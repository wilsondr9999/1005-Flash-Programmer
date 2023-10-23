using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Reflection;

namespace PICFlash
{
    internal class FlashProgrammer
    {
        private static CommandParser myCommandParser;
        private static ProgrammerDriver driver;
        private static FileStream fs = null;

        static void Main(string[] args)
        {
            int i;
            byte[] _randomDataBuffer;
            int _randomDataBufPtr;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

            Console.WriteLine(String.Empty);
            Console.WriteLine("Flash Programmer v" + Assembly.GetEntryAssembly().GetName().Version.ToString());
            Console.WriteLine("Copyright © 2021 Dan Wilson");
            Console.WriteLine("===========================");
            Console.WriteLine(String.Empty);

            // Create a new CommandParser and use it to process all arguments
            myCommandParser = new CommandParser(args);

            // If there were any errors in command parsing, print them now and exit
            if (myCommandParser.Error)
            {
                Console.WriteLine("ERROR: " + myCommandParser.ErrorText);
                return;
            }

            // Print command summary
            Console.WriteLine(String.Format("   COM port:          {0}", myCommandParser.ComPortSpec));
            Console.WriteLine(              "   Command:           " + myCommandParser.CommandText);
            Console.WriteLine(String.Format("   Starting Address:  {0:x6}", myCommandParser.StartingAddress));
            Console.WriteLine(String.Format("   Data Length:       {0:x6}", myCommandParser.DataLength));
            Console.WriteLine(String.Format("   I/O:               {0}", (myCommandParser.DisplayToConsole ? "Console" : myCommandParser.FileName)));
            Console.WriteLine(String.Empty);

            // Create a new ProgrammerDriver object
            driver = new ProgrammerDriver(myCommandParser.BaudRate, myCommandParser.ComPortSpec);

            // Reset the device
            Console.Write("Resetting Flash programmer ... ");
            if (driver.ResetDevice())
            {
                Console.WriteLine("Success");
            } else
            {
                Console.WriteLine("ERROR: Failed to reset device.");
                return;
            }

            // Get the chip identifier
            Console.WriteLine("Chip Identification: " + driver.GetChipIdentifier);
            if (driver.GetChipType==ProgrammerDriver.ChipType._UnknownChip)
            {
                return;
            }

            // Make sure the address given on the command line is within this chip's size
            if (myCommandParser.StartingAddress>=driver.GetChipSize)
            {
                Console.WriteLine("ERROR: Address is invalid for this chip type.");
                return;
            }

            // Set the DataLength default if it's not already set
            if (myCommandParser.DataLength == 0x0) myCommandParser.DataLength = driver.GetChipSize;

            // Make sure the DataLength give on the command line is within this chip's size
            if (myCommandParser.StartingAddress+myCommandParser.DataLength > driver.GetChipSize)
            {
                Console.WriteLine("ERROR: Data length is too large for this chip type.");
                return;
            }

            // Form the commands to send to the PIC
            switch (myCommandParser.Command)
            {
                case CommandParser.CommandType.ReadFlash:
                    // If output is to file, open it for writing
                    if (!myCommandParser.DisplayToConsole) fs = new FileStream(myCommandParser.FileName, FileMode.Create, FileAccess.Write);

                    // Set up subscription to PageReadCompleted event from driver
                    driver.PageReadCompleted += driver_PageReadCompleted;

                    // Read from Flash
                    if (driver.ReadFlash(myCommandParser.StartingAddress,myCommandParser.DataLength))
                    {
                        // Close output file
                        if (!myCommandParser.DisplayToConsole) fs.Close();

                        Console.WriteLine("Flash read completed successfully.");
                    } else
                    {
                        Console.WriteLine("ERROR: Error during flash read.");
                    }

                    // Remove subscription to PageReadCompleted event
                    driver.PageReadCompleted -= driver_PageReadCompleted;
                    break;
                case CommandParser.CommandType.WriteFlash:
                    // Open input file for reading
                    fs = new FileStream(myCommandParser.FileName, FileMode.Open, FileAccess.Read);

                    // Set up subscription to NeedBytesForPageWrite event from driver
                    driver.NeedBytesForPageWrite += driver_NeedBytesForPageWrite;

                    // Write to Flash
                    if (driver.WriteFlash(myCommandParser.StartingAddress, myCommandParser.DataLength))
                    {
                        // Close input file
                        fs.Close();

                        Console.WriteLine("Flash write completed successfully.");
                    } else
                    {
                        Console.WriteLine("ERROR: Error during flash write.");
                    }

                    // Remove subscription to NeedBytesForPageWrite event
                    driver.NeedBytesForPageWrite -= driver_NeedBytesForPageWrite;
                    break;
                case CommandParser.CommandType.GenerateRandomFile:
                    // Generate random file command
                    _randomDataBuffer = new byte[0x100];
                    try
                    {
                        var rand = new Random();
                        using (fs = new FileStream(myCommandParser.FileName, FileMode.Create, FileAccess.Write))
                        {
                            i = 0;              // Number of bytes written to file
                            _randomDataBufPtr = 0;
                            while (i < myCommandParser.DataLength)
                            {
                                if (_randomDataBufPtr < 0x100)
                                {
                                    // Buffer is not full yet
                                    _randomDataBuffer[_randomDataBufPtr++] = (byte)rand.Next(0x100);
                                    i++;
                                }
                                else
                                {
                                    // Buffer is full, write it to file
                                    fs.Write(_randomDataBuffer, 0, 0x100);
                                    _randomDataBufPtr = 0;
                                }
                            }
                            // Flush last buffer to file if there's data in it
                            if (_randomDataBufPtr > 0)
                            {
                                fs.Write(_randomDataBuffer, 0, _randomDataBufPtr);
                            }
                            fs.Close();
                            Console.WriteLine("Successfully wrote file: " + myCommandParser.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: Exception while writing file: " + ex.Message);
                    }
                    break;
                case CommandParser.CommandType.GetChipIdentification:
                    // Get Flash chip ID command
                    // The driver already got it for us, and it's already displayed, so just exit
                    break;
                case CommandParser.CommandType.FormatFlash:
                    // Erase entire chip (format)

                    Console.Write("Formatting Flash ... ");
                    
                    // Format flash
                    if (driver.FormatFlash())
                    {
                        Console.WriteLine("Successful");
                    } else
                    {
                        Console.WriteLine();
                        Console.WriteLine("ERROR: Error during flash format.");
                    }

                    break;
                /*case CommandParser.CommandType.EraseFlashSectors:
                    // Erase one sector of chip
                    // Assemble command
                    _bufPtr = 0;
                    _buffer[_bufPtr++] = 0x82;                          // Command 0x82 = erase one 4K sector
                    _buffer[_bufPtr++] = (byte)((myCommandParser.StartingAddress >> 16) & 0xff);   // MSByte of address
                    _buffer[_bufPtr++] = (byte)((myCommandParser.StartingAddress >> 8) & 0xff);    // Next byte of address
                    _buffer[_bufPtr++] = (byte)((myCommandParser.StartingAddress) & 0xff);         // LSByte of address
                    _buffer[_bufPtr++] = 0x00;                          // Placeholder

                    // Print command to send
                    ConditionalConsoleWriteCmd("> ");
                    ConditionalConsoleWriteCmd(PrintBytesFromBuf(0, _bufPtr, true));
                    ConditionalConsoleWriteLineCmd(String.Empty);

                    // Send the command
                    SendBytesToSerial((byte)(_bufPtr));                 // Send the command to the PIC

                    // Now wait for acknowledgement
                    ConditionalConsoleWriteCmd("< ");
                    ReadBytesFromSerial(1);                             // Wait for acknowledgement byte
                    ConditionalConsoleWriteCmd(PrintBytesFromBuf(0, 1, true));

                    // Check to see what the acknowledgement byte is, that tells us whether more bytes are coming
                    if (_buffer[0] == 0x80)
                    {
                        // No additional bytes to receive, do nothing
                        ConditionalConsoleWriteLineCmd(String.Empty);
                        Console.WriteLine("Sector Erase completed successfully.");
                    }
                    else
                    {
                        // Error, inform user
                        ConditionalConsoleWriteLineCmd(String.Empty);
                        Console.WriteLine("ERR: Sector erase command failed.");
                    }
                    break;*/
                default:
                    break;
            }

            return;
        }

        internal static void driver_PageReadCompleted(byte[] _buffer,int currentAddress,int length)
        {
            // A page read was completed, send the page of data either to the console or the file
            if (myCommandParser.DisplayToConsole)
            {
                // Display to console
                PrettyPrintHeader();
                PrettyPrintBytes(_buffer, currentAddress, length);
            } else
            {
                // Writing to file
                Console.Write(String.Format("Reading Flash ... {0:P1}", ((double)currentAddress / (double)myCommandParser.DataLength)) + "\r");
                fs.Write(_buffer, 0, length);
            }
            return;
        }

        internal static void driver_NeedBytesForPageWrite(Buffers _buffer,int currentAddress,int length)
        {
            // A parge write is requested, we need to send a page of data from the file
            int i;
            int data;

            Console.Write(String.Format("Writing Flash ... {0:P1}", ((double)currentAddress / (double)myCommandParser.DataLength)) + "\r");
            _buffer.ClearBuf();
            for (i=0; i<length; i++)
            {
                data = fs.ReadByte();
                if (data!=-1)
                {
                    _buffer.SetNextBufByte((byte)data);
                } else
                {
                    Console.WriteLine("ERROR: End of input file.");
                }
            }
        }

        public static void PrettyPrintHeader()
        {
            Console.WriteLine("Address   0  1  2  3  4  5  6  7    8  9  a  b  c  d  e  f    ASCII");
        }

        public static void PrettyPrintBytes(byte[] _buffer, int address, int length)
        {
            // Prints the data from the buffer in a human-readable hexadecimal format
            int lineAddr, offset, lineNum;
            lineAddr = address;                         // This routine assumes the address is at a 16-byte boundary
            lineNum = 0;
            char testChar;
            while (lineAddr < (address+length))
            {
                Console.Write(String.Format("{0:x6}:  ", lineAddr));
                for (offset = 0; offset < 8; offset++)
                {
                    Console.Write(String.Format("{0:x2} ", _buffer[(lineNum * 16) + offset]));
                }
                Console.Write("  ");
                for (offset = 8; offset < 16; offset++)
                {
                    Console.Write(String.Format("{0:x2} ", _buffer[(lineNum * 16) + offset]));
                }
                Console.Write("   ");
                for (offset=0;offset < 16; offset++)
                {
                    testChar = (char)_buffer[(lineNum * 16) + offset];
                    if (!Char.IsControl(testChar))
                    {
                        // Character is printable
                        Console.Write(testChar);
                    } else
                    {
                        Console.Write('.');
                    }
                }
                lineNum++;
                lineAddr += 16;
                Console.WriteLine(String.Empty);
            }
        }
        /*
        public static void ClearBuffer()
        {
            // Clears the buffer to 00s
            for (int i = 0; i < 0x100; i++) _buffer[i] = 0x00;
        }

        public static void ConditionalConsoleWriteLineCmd(string output)
        {
            // Conditionally writes the command based on the printCmds variable
            if (printCmds) Console.WriteLine(output);
        }
        public static void ConditionalConsoleWriteCmd(string output)
        {
            // Conditionally writes the command based on the printCmds variable
            if (printCmds) Console.Write(output);
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* This class encapsulates the functionality to drive the programmer via the serial port. */


namespace PICFlash
{
    internal class ProgrammerDriver
    {
        // Typedefs
        internal enum CommandIndexes
        {
            Read=0,
            Write=1,
            SectorErase=2,
            ChipID=3,
            DeviceReset=4,
            ChipErase=5
        }
        internal enum ChipType
        {
            _1Mbit=0,
            _2Mbit=1,
            _4Mbit=2,
            _UnknownChip=3
        }

        byte[] Commands = { 0x80, 0x81, 0x82, 0x83, 0xbf, 0x84 };

        // Constants
        const int ATRlen = 12;
        byte[] ATR = new byte[] { 0x3b, 0x38, 0x45, 0x00, (byte)'F', (byte)'l', (byte)'s', (byte)'h', (byte)'P', (byte)'r', (byte)'g', 0x12 };
        string[] ChipIdentifiers = new string[] { "SST39SF010A (1Mbit, 128KB)", "SST39SF020A (2Mbit, 256KB)", "SST39SF040A (4Mbit, 512KB)", "Unknown Chip Type" };
        int[] ChipSize = new int[] { 0x20000, 0x40000, 0x80000, 0x0 };

        // Private variables
        private Serial _mySerialPort;
        private Buffers _cmdBuf;
        private Buffers _pageBuf;
        private Buffers _sectorBuf;
        private Buffers _ackBuf;
        private ChipType _chipType;

        // Events
        internal delegate void NotifyReadCompleted(byte[] _buffer, int startingAddress, int length);
        internal delegate void NotifyWriteCompleted(int startingAddress, int length);
        internal delegate void NotifyNeedBytesForPageWrite(Buffers _buffer, int startingAddress, int length);
        internal event NotifyReadCompleted PageReadCompleted;
        internal event NotifyWriteCompleted PageWriteCompleted;
        internal event NotifyNeedBytesForPageWrite NeedBytesForPageWrite;

        // Internal Properties
        internal ChipType GetChipType
        {
            get { return _chipType; }
        }
        internal string GetChipIdentifier
        {
            get { return ChipIdentifiers[(int)_chipType]; }
        }
        internal int GetChipSize
        {
            get { return ChipSize[(int)_chipType]; }
        }
        internal Buffers GetSectorBufferObject
        {
            get { return _sectorBuf; }
        }

        // Internal Methods
        internal ProgrammerDriver(int baudRate, string comPortSpec)
        {
            _mySerialPort = new Serial(baudRate, comPortSpec);
            _cmdBuf=new Buffers(5);
            _pageBuf=new Buffers(256);
            _sectorBuf=new Buffers(4096);
            _ackBuf = new Buffers(12);
        }

        internal bool ResetDevice()
        {
            // Resets the device and verifies the ATR
            // Returns true if successful

            int i;

            // Set up the command buffer and send it to the device
            _cmdBuf.ClearBuf();
            _cmdBuf.SetNextBufByte(Commands[(int)CommandIndexes.DeviceReset]);
            for (i = 1;i<_cmdBuf.BufSize;i++) { _cmdBuf.SetNextBufByte(0); }
            _mySerialPort.SendBytesToSerial(_cmdBuf);

            // Receive the ATR
            _ackBuf.ClearBuf();
            _mySerialPort.ReadBytesFromSerial(_ackBuf, ATRlen);

            // Make sure the ATR is correct
            for (i=0;i<ATRlen;i++)
            {
                if (ATR[i] != _ackBuf.GetNextBufByte()) return false;
            }

            // ATR was correct, get chip type
            GetFlashIdentifier();

            return true;
        }
        private bool GetFlashIdentifier()
        {
            // Gets the identifier bytes from the Flash chip
            // Returns true if successful

            int i;
            int length;
            byte mfrID, chipID;

            // Set up the command buffer and send it to the device
            _cmdBuf.ClearBuf();
            _cmdBuf.SetNextBufByte(Commands[(int)CommandIndexes.ChipID]);
            for (i = 1; i < _cmdBuf.BufSize; i++) { _cmdBuf.SetNextBufByte(0); }
            _mySerialPort.SendBytesToSerial(_cmdBuf);

            // Receive the acknowledgement byte
            _ackBuf.ClearBuf();
            _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);

            // See if we have more bytes coming
            _ackBuf.ResetBuf();
            if (_ackBuf.GetNextBufByte() == 0x81)
            {
                // We have more bytes coming, get the length
                _ackBuf.ClearBuf();
                _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);
                _ackBuf.ResetBuf();
                length = _ackBuf.GetNextBufByte();
                if (length == 0) length = 0x100;

                // Now read all the returned bytes into the ack buffer
                _ackBuf.ClearBuf();
                _mySerialPort.ReadBytesFromSerial(_ackBuf, length);

                // Get the manufacturer ID and chip ID
                mfrID = _ackBuf.GetNextBufByte();
                chipID = _ackBuf.GetNextBufByte();

                // Store the chip identifier
                if (mfrID == 0xbf)
                {
                    switch (chipID)
                    {
                        case 0xb5:
                            _chipType = ChipType._1Mbit;
                            break;
                        case 0xb6:
                            _chipType = ChipType._2Mbit;
                            break;
                        case 0xb7:
                            _chipType = ChipType._4Mbit;
                            break;
                        default:
                            _chipType = ChipType._UnknownChip;
                            break;
                    }
                }
                else _chipType = ChipType._UnknownChip;
            }
            else return false;

            if (_chipType != ChipType._UnknownChip) return true; else return false;
        }
        internal bool FormatFlash()
        {
            // Format entire flash chip
            // Returns true if successful

            int i;

            // Set up the command buffer and send it to the device
            _cmdBuf.ClearBuf();
            _cmdBuf.SetNextBufByte(Commands[(int)CommandIndexes.ChipErase]);
            for (i = 1; i < _cmdBuf.BufSize; i++) { _cmdBuf.SetNextBufByte(0); }
            _mySerialPort.SendBytesToSerial(_cmdBuf);

            // Receive the acknowledgement byte
            _ackBuf.ClearBuf();
            _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);

            // Make sure we get proper acknowledgement
            _ackBuf.ResetBuf();
            if (_ackBuf.GetNextBufByte() == 0x80) return true; else return false;
        }
        internal bool ReadSector(int startingAddress, int dataLength)
        {
            // Reads one sector (4KB) into the sector buffer
            // Raises an event "PageReady" when each page has completed the read
            // The main program can then get the data and either display it or write it to a file

            // DataLength might be a large value, but we can only read 256 bytes at a time
            // So keep track of the length

            int address;
            int length;
            int j;

            // Make sure the data length is not over 1 sector
            if (dataLength >= _sectorBuf.BufSize) return false;

            // Make sure the address falls on a sector boundary
            if (startingAddress != (startingAddress & 0xfff000)) return false;

            // Clear the sector buffer
            _sectorBuf.ClearBuf();

            // Begin a loop to read the sector 1 page at a time
            for (address = startingAddress; address < dataLength; address += 0x100)
            {
                // Set up the command buffer and send it to the device
                _cmdBuf.ClearBuf();
                _cmdBuf.SetNextBufByte(Commands[(int)CommandIndexes.Read]);
                _cmdBuf.SetNextBufByte((byte)(address >> 16));
                _cmdBuf.SetNextBufByte((byte)(address >> 8));
                _cmdBuf.SetNextBufByte((byte)(address));
                if ((dataLength - address) >= 0x100)
                {
                    length = 0;
                }
                else
                {
                    length = (dataLength - address);
                }
                _cmdBuf.SetNextBufByte((byte)(length));
                _mySerialPort.SendBytesToSerial(_cmdBuf);

                // Receive acknowledgement
                _ackBuf.ClearBuf();
                _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);

                // See if we have more bytes coming
                _ackBuf.ResetBuf();
                if (_ackBuf.GetNextBufByte() == 0x81)
                {
                    // We have more bytes coming, get the length
                    _ackBuf.ClearBuf();
                    _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);
                    _ackBuf.ResetBuf();
                    length = _ackBuf.GetNextBufByte();
                    if (length == 0) length = 0x100;

                    // Now read all the returned bytes into the page buffer
                    _pageBuf.ClearBuf();
                    _mySerialPort.ReadBytesFromSerial(_pageBuf, length);

                    // Now raise the event to notify subscribers that the page read finished
                    _pageBuf.ResetBuf();
                    OnPageReadCompleted(_pageBuf.Buffer, address, (length == 0 ? 0x100 : length));

                    // Copy the page buffer into the sector buffer
                    _pageBuf.ResetBuf();
                    for (j=0;j<length; j++)
                    {
                        _sectorBuf.SetNextBufByte(_pageBuf.GetNextBufByte());
                    }
                }
                else
                {
                    // No additional bytes coming, we're done
                }
            }

            return true;
        }
        internal bool WriteSector(int startingAddress)
        {
            // Writes one sector (4KB) from the sector buffer to the starting address
            // Raises an event "PageReady" when each page has completed the read
            // The main program can then get the data and either display it or write it to a file

            // DataLength might be a large value, but we can only read 256 bytes at a time
            // So keep track of the length

            int address;
            int length,dataLength;
            int j;

            // Length will be 1 sector
            dataLength = 0x1000;

            // Make sure the address falls on a sector boundary
            if (startingAddress != (startingAddress & 0xfff000)) return false;

            // Reset the sector buffer to the beginning
            _sectorBuf.ResetBuf();

            // Begin a loop to write the sector 1 page at a time
            for (address = startingAddress; address < dataLength; address += 0x100)
            {
                // Set up the command buffer and send it to the device
                _cmdBuf.ClearBuf();
                _cmdBuf.SetNextBufByte(Commands[(int)CommandIndexes.Write]);
                _cmdBuf.SetNextBufByte((byte)(address >> 16));
                _cmdBuf.SetNextBufByte((byte)(address >> 8));
                _cmdBuf.SetNextBufByte((byte)(address));
                if ((dataLength - address) >= 0x100)
                {
                    length = 0;
                }
                else
                {
                    length = (dataLength - address);
                }
                _cmdBuf.SetNextBufByte((byte)(length));
                _mySerialPort.SendBytesToSerial(_cmdBuf);

                // Receive acknowledgement
                _ackBuf.ClearBuf();
                _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);

                // See if we're clear to send bytes
                _ackBuf.ResetBuf();
                if (_ackBuf.GetNextBufByte() == 0x91)
                {
                    // We can send more bytes, get the length
                    _ackBuf.ClearBuf();
                    _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);
                    _ackBuf.ResetBuf();
                    length = _ackBuf.GetNextBufByte();
                    if (length == 0) length = 0x100;

                    // Copy the next page of the sector buffer into the page buffer
                    _pageBuf.ClearBuf();
                    for (j=0; j<length; j++)
                    {
                        _pageBuf.SetNextBufByte(_sectorBuf.GetNextBufByte());
                    }

                    // Now read all the returned bytes into the page buffer
                    _pageBuf.ResetBuf();
                    _mySerialPort.SendBytesToSerial(_pageBuf);

                    // Now raise the event to notify subscribers that the page read finished
                    _pageBuf.ResetBuf();
                    OnPageWriteCompleted(address, (length == 0 ? 0x100 : length));
                }
                else
                {
                    // Did not request we send bytes, that's an error
                    return false;
                }
            }

            return true;
        }
        internal bool ReadFlash(int startingAddress, int dataLength)
        {
            // Reads Flash memory
            // Raises an event "PageReadCompleted" when each page has completed the read
            // The main program can then get the data and either display it or write it to a file

            // DataLength might be a large value, but we can only read 256 bytes at a time
            // So keep track of the length

            int address;
            int length;

            // Set up the command buffer and send it to the device
            for (address=startingAddress;address<dataLength;address+=0x100)
            {
                _cmdBuf.ClearBuf();
                _cmdBuf.SetNextBufByte(Commands[(int)CommandIndexes.Read]);
                _cmdBuf.SetNextBufByte((byte)(address >> 16));
                _cmdBuf.SetNextBufByte((byte)(address >> 8));
                _cmdBuf.SetNextBufByte((byte)(address));
                if ((dataLength-address)>=0x100)
                { 
                    length = 0;
                } else
                {
                    length = (dataLength-address);
                }
                _cmdBuf.SetNextBufByte((byte)(length));
                _mySerialPort.SendBytesToSerial(_cmdBuf);

                // Receive acknowledgement
                _ackBuf.ClearBuf();
                _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);

                // See if we have more bytes coming
                _ackBuf.ResetBuf();
                if (_ackBuf.GetNextBufByte() ==0x81)
                {
                    // We have more bytes coming, get the length
                    _ackBuf.ClearBuf();
                    _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);
                    _ackBuf.ResetBuf();
                    length=_ackBuf.GetNextBufByte();
                    if (length == 0) length = 0x100;

                    // Now read all the returned bytes into the page buffer
                    _pageBuf.ClearBuf();
                    _mySerialPort.ReadBytesFromSerial(_pageBuf,length);

                    // Now raise the event to notify subscribers that the page read finished
                    _pageBuf.ResetBuf();
                    OnPageReadCompleted(_pageBuf.Buffer, address, (length == 0 ? 0x100 : length));

                } else
                {
                    // No additional bytes coming, we're done
                }
            }

            return true;
        }

        internal bool WriteFlash(int startingAddress, int dataLength)
        {
            // Writes Flash memory
            // Raises an event "NeedBytesForPageWrite" when this routine needs a page of data to write
            // The main program needs to fill the page with data

            // DataLength might be a large value, but we can only read 256 bytes at a time
            // So keep track of the length

            int address;
            int length;

            // Set up the command buffer and send it to the device
            for (address = startingAddress; address < dataLength; address += 0x100)
            {
                _cmdBuf.ClearBuf();
                _cmdBuf.SetNextBufByte(Commands[(int)CommandIndexes.Write]);
                _cmdBuf.SetNextBufByte((byte)(address >> 16));
                _cmdBuf.SetNextBufByte((byte)(address >> 8));
                _cmdBuf.SetNextBufByte((byte)(address));
                if ((dataLength - address) >= 0x100)
                {
                    length = 0;
                }
                else
                {
                    length = (dataLength - address);
                }
                _cmdBuf.SetNextBufByte((byte)(length));
                _mySerialPort.SendBytesToSerial(_cmdBuf);

                // Receive acknowledgement
                _ackBuf.ClearBuf();
                _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);

                // See if we need to send bytes
                _ackBuf.ResetBuf();
                if (_ackBuf.GetNextBufByte() == 0x91)
                {
                    // We have been requested to send bytes, get the length
                    _ackBuf.ClearBuf();
                    _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);
                    _ackBuf.ResetBuf();
                    length = _ackBuf.GetNextBufByte();
                    if (length == 0) length = 0x100;

                    // Raise event to notify subscriber that we need bytes
                    _pageBuf.ClearBuf();
                    OnNeedBytesForPageWrite(_pageBuf, address, (length == 0 ? 0x100 : length));

                    // Send the bytes to the programmer
                    _pageBuf.ResetBuf();
                    _mySerialPort.SendBytesToSerial(_pageBuf);

                    // Get the ack byte to make sure page write was successful
                    _ackBuf.ClearBuf();
                    _mySerialPort.ReadBytesFromSerial(_ackBuf, 1);
                    _ackBuf.ResetBuf();
                    if (_ackBuf.GetNextBufByte()!=0x80)
                    {
                        // Error during write, return
                        return false;
                    }
                }
                else
                {
                    // No bytes requested to send, we're done
                }
            }

            return true;
        }

        protected virtual void OnPageReadCompleted(byte[] _buffer, int currentAddress, int length)
        {
            PageReadCompleted?.Invoke(_buffer,currentAddress,length);
        }
        protected virtual void OnPageWriteCompleted(int currentAddress, int length)
        {
            PageWriteCompleted?.Invoke(currentAddress, length);
        }
        protected virtual void OnNeedBytesForPageWrite(Buffers _buffer, int currentAddress, int length)
        {
            NeedBytesForPageWrite?.Invoke(_buffer, currentAddress, length);
        }
    }
}

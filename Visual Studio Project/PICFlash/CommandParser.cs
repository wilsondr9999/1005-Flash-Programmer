using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


/* CommandParser.cs
 * 
 * This class encapsulates all code necessary to parse the command line arguments and make them available to the Main
 * program through properties
 */


namespace PICFlash
{
    internal class CommandParser
    {
        // Internal types
        internal enum CommandType
        {
            NoCommand=0,
            ReadFlash=1,
            WriteFlash=2,
            FormatFlash=3,
            EraseFlashSectors=4,
            GetChipIdentification=5,
            GenerateRandomFile=6
        };

        // Private variables
        // Serial port
        private int _baudRate;
        private string _comPortSpec;

        // Legal baud rates
        private readonly int[] allowedBaudRates = { 300, 1200, 2400, 9600, 14400, 19200, 28800, 38400, 57600, 115200, 230400 };

        // Commands
        private CommandType _command;
        private bool _displayToConsole;

        // Addressing
        private int _startingAddress;
        private int _dataLength;
        private int _endingAddress;
        private int _fileOffset;

        // File
        private string _fileName;

        // Error
        private bool _error;
        private string _errorText;


        // Properties
        internal int BaudRate
        {
            get { return _baudRate; }
        }
        internal string ComPortSpec
        {
            get { return _comPortSpec; }
        }
        internal CommandType Command
        {
            get { return _command; }
        }
        internal string CommandText
        {
            get
            {
                switch (_command)
                {
                    case CommandType.NoCommand: return "No Command";
                    case CommandType.ReadFlash: return "Read Flash";
                    case CommandType.WriteFlash: return "Write Flash";
                    case CommandType.FormatFlash: return "Format Flash";
                    case CommandType.EraseFlashSectors: return "Erase Flash Sectors";
                    case CommandType.GetChipIdentification: return "Get Chip Identification";
                    case CommandType.GenerateRandomFile: return "Generate Random File";
                    default: return "Unknown Command";
                }
            }
        }
        internal bool DisplayToConsole
        {
            get { return _displayToConsole; }
        }
        internal int StartingAddress
        {
            get { return _startingAddress; }
        }
        internal int DataLength
        {
            get { return _dataLength; }
            set { _dataLength = value; }
        }
        internal int EndingAddress
        {
            get {  return (_startingAddress + _dataLength -1); }
        }
        internal int FileOffset
        {
            get { return _fileOffset; }
        }
        internal string FileName
        {
            get { return _fileName; }
        }
        internal bool Error
        {
            get { return _error; }
        }
        internal string ErrorText
        {
            get { return _errorText; }
        }


        // Methods
        internal CommandParser(string[] args)
        {
            // Constructor for the CommandParser class
            // Take all the args and parse them, put them in variables
            int i,j;

            // First, set all defaults
            _baudRate = 230400;
            _comPortSpec = "COM1";
            _command = CommandType.ReadFlash;
            _displayToConsole= false; 
            _startingAddress = 0;
            _dataLength = 0;
            _fileOffset = 0;
            _fileName=String.Empty;
            _error = false;
            _errorText = String.Empty;

            // In a loop, process all arguments
            i = 0;
            while (i < args.Length)
            {
                switch (args[i].ToLower()) {
                    case "-b":
                        // Setting baud rate, make sure it's an integer
                        if (!int.TryParse(args[i+1],out _baudRate)) {
                            _error = true;
                            _errorText = "Unable to parse baud rate: " + args[i + 1];
                        }
                        // Make sure baud rate is a legal baud rate
                        _error = true;
                        _errorText = "Baud rate is not allowed: " + args[i + 1];
                        for (j=0;j<allowedBaudRates.Length;j++)
                        {
                            if (_baudRate==allowedBaudRates[j])
                            {
                                _error = false;
                                _errorText = String.Empty;
                            }
                        }
                        i += 2;
                        break;
                    case "-c":
                        // Set comport spec
                        _comPortSpec = args[i + 1];
                        // Check to see if comport spec is valid
                        if ((_comPortSpec.ToLower().Substring(0,3) != "com") || (_comPortSpec.Length>4) ||
                            (_comPortSpec[3]<'0') || (_comPortSpec[3]>'9'))
                        {
                            _error=true;
                            _errorText = "Invalid Com Port Specification: " + _comPortSpec;
                        }
                        i += 2;
                        break;
                    case "-w":
                        // Writing to flash
                        if (_command==CommandType.ReadFlash)
                        {
                            _command = CommandType.WriteFlash;
                        } else
                        {
                            _error = true;
                            _errorText = "Can only have one command present.";
                        }
                        i++;
                        break;
                    case "-f":
                        // Format flash
                        if (_command == CommandType.ReadFlash)
                        {
                            _command = CommandType.FormatFlash;
                        }
                        else
                        {
                            _error = true;
                            _errorText = "Can only have one command present.";
                        }
                        i++;
                        break;
                    case "-e":
                        // Erase one or more flash sectors
                        if (_command == CommandType.ReadFlash)
                        {
                            _command = CommandType.EraseFlashSectors;
                        }
                        else
                        {
                            _error = true;
                            _errorText = "Can only have one command present.";
                        }
                        i++;
                        break;
                    case "-i":
                        // Get chip identification
                        if (_command==CommandType.ReadFlash)
                        {
                            _command = CommandType.GetChipIdentification;
                        } else
                        {
                            _error= true;
                            _errorText = "Can only have one command present.";
                        }
                        i++;
                        break;
                    case "-g":
                        // Generate random file
                        if (_command==CommandType.ReadFlash)
                        {
                            _command = CommandType.GenerateRandomFile;
                        }
                        else
                        {
                            _error=true;
                            _errorText = "Can only have one command present.";
                        }
                        i++;
                        break;
                    case "-d":
                        // Display results to console
                        // Only valid with read command
                        if (_command==CommandType.ReadFlash)
                        {
                            _displayToConsole = true;
                        }
                        else
                        {
                            _error= true;
                            _errorText = "Display to console is only valid for reading flash.";
                        }
                        i++;
                        break;
                    case "-s":
                        // Starting address
                        try
                        {
                            _startingAddress = Convert.ToInt32(args[i + 1], 16);
                        } catch (Exception ex)
                        {
                            _error = true;
                            _errorText = "Starting address out of range or invalid: " + args[i + 1];
                        }
                        // Make sure address is in range
                        if (_startingAddress < 0 || _startingAddress > 0x07ffff)
                        {
                            _error = true;
                            _errorText = "Starting address out of range or invalid: " + args[i + 1];
                        }
                        i += 2;
                        break;
                    case "-l":
                        // Data length
                        try
                        {
                            _dataLength = Convert.ToInt32(args[i + 1], 16);
                        }
                        catch (Exception ex)
                        {
                            _error = true;
                            _errorText = "Data length out of range or invalid: " + args[i + 1];
                        }
                        // Make sure length is in range
                        if (_dataLength<0 || _dataLength>0x07ffff)
                        {
                            _error=true;
                            _errorText = "Data length out of range or invalid: " + args[i + 1];
                        }
                        i += 2;
                        break;
                    case "-n":
                        // Ending address
                        try
                        {
                            _endingAddress = Convert.ToInt32(args[i + 1], 16);
                        }
                        catch (Exception ex)
                        {
                            _error = true;
                            _errorText = "Ending address out of range or invalid: " + args[i + 1];
                        }
                        // Make sure ending address is in range
                        if (_endingAddress<0 || _endingAddress>0x07ffff)
                        {
                            _error = true;
                            _errorText = "Ending address out of range or invalid: " + args[i + 1];
                        }
                        i += 2;
                        _dataLength = _endingAddress - _startingAddress + 1;
                        break;
                    case "-o":
                        // Offset in file
                        try
                        {
                            _fileOffset = Convert.ToInt32(args[i + 1], 16);
                        }
                        catch (Exception ex)
                        {
                            _error = true;
                            _errorText = "File offset out of range or invalid: " + args[i + 1];
                        }
                        // Make sure file offset is in range
                        if (_fileOffset<0 || _fileOffset>0x07ffff)
                        {
                            _error = true;
                            _errorText = "File offset out of range or invalid: " + args[i + 1];
                        }
                        i += 2;
                        break;
                    default:
                        // Filename to write data into (reading Flash) or read from (write to Flash)
                        _fileName= args[i];
                        // If writing to Flash, file must exist
                        if (_command==CommandType.WriteFlash && !File.Exists(_fileName))
                        {
                            _error = true;
                            _errorText = "File does not exist: " + args[i];
                        } else if (_command==CommandType.ReadFlash & File.Exists(_fileName))
                        {
                            // If reading from Flash, file should not already exist
                            _error = true;
                            _errorText = "File already exists: " + args[i];
                        }
                        i++;
                        break;
                }
                if (_error) i = args.Length;
            }

            // Don't perform additional checks if we already have an error
            if (_error) return;

            // Make sure we have a valid output selected and specified for each command
            if (_command==CommandType.ReadFlash)
            {
                if (_fileName==String.Empty && !_displayToConsole)
                {
                    _error= true;
                    _errorText = "Flash read command must either display to console (-d) or must have a filename to write data to.";
                }
            } else if (_command==CommandType.GenerateRandomFile)
            {
                if (_fileName==String.Empty)
                {
                    _error = true;
                    _errorText = "Generate random file command must have a filename to write data to.";
                }
            } else if (_command==CommandType.WriteFlash)
            {
                if (_fileName == String.Empty)
                {
                    _error = true;
                    _errorText = "Flash write file command must have a filename to read data from.";
                }
            }
        }
    }
}

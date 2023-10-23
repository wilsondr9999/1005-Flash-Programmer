using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;


/* Serial.cs
 * 
 * This class encapsulates all functions necessary to send and receive data to/from the serial port
 */

namespace PICFlash
{
    internal class Serial
    {
        // Private members
        private SerialPort _serialPort;

        private int _baudRate;
        private string _comPortSpec;


        // Internal Properties
        internal int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }
        internal string ComPortSpec
        {
            get { return _comPortSpec; }
            set { _comPortSpec = value; }
        }


        // Internal Methods
        internal Serial(int baudRate,string comPortSpec)
        {
            _baudRate = baudRate;
            _comPortSpec = comPortSpec;

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = _comPortSpec;
            _serialPort.BaudRate = _baudRate;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            // Open the port
            _serialPort.Open();
        }

        ~Serial()
        {
            _serialPort.Close();
        }

        internal void SendBytesToSerial(Buffers _buffer)
        {
            // Sends bytes from _buffer to the serial port
            _serialPort.Write(_buffer.Buffer, 0, _buffer.BufDataLength);
        }

        internal void ReadBytesFromSerial(Buffers _buffer, int length)
        {
            // Reads bytes from the serial port and puts them into buffer
            bool gotByte;
            bool done = false;
            int j = 0;
            while (!done)
            {
                gotByte = false;
                while (!gotByte)
                {
                    gotByte = true;
                    try
                    {
                        // Try to read a byte
                        j = _serialPort.ReadByte();
                    }
                    catch (TimeoutException)
                    {
                        gotByte = false;
                    }
                }

                _buffer.SetNextBufByte((byte)j);
                if (_buffer.BufDataLength == length) done = true;
            }
            // If we exit the while loop, we've received length bytes from the serial port into _buffer, so exit
        }
    }
}

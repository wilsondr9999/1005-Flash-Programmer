using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* This class encapsulates all code necessary to maintain buffers used by the main program */


namespace PICFlash
{
    internal class Buffers
    {
        // Private members
        private byte[] _buffer;
        private int _bufPtr;
        private int _bufDataLength;

        // Internal Properties
        internal int BufSize
        {
            get { return _buffer.Length; }
        }
        internal int BufDataLength
        {
            get { return _bufDataLength; }
        }
        internal byte[] Buffer
        {
            get { return _buffer; }
        }

        // Internal Methods
        internal Buffers(int size)
        {
            _buffer = new byte[size];
            _bufPtr = 0;
            _bufDataLength = 0;
        }

        internal void ResetBuf()
        {
            _bufPtr=0;
        }
        internal void ClearBuf()
        {
            _bufPtr = 0;
            _bufDataLength = 0;
        }
        internal byte GetNextBufByte() { return _buffer[_bufPtr++]; }
        internal void SetNextBufByte(byte newByte) { _buffer[_bufDataLength++] = newByte; }
    }
}

using System;

using System.IO;

namespace ALLBOT
{
    class WavStream : IDisposable
    {
		private MemoryStream stream = new MemoryStream();

		private int dataLen = 0;

		public int Length {
			get;
			private set;
		}

		public MemoryStream Stream
        {
            get
            {
                return stream;
            }
            private set
            {
                stream = value;
            }
        }
        /// <summary>
        /// Write an array to the stream with a given 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        public void writeByteArray(byte[] data, int length)
        {
            for (int i = 0; i < length;i++)
            {
                writeByte(data[i]);
            }
                
        }

        /// <summary>
        /// Write a byte to the stream
        /// </summary>
        /// <param name="data"></param>
        public void writeByte(byte data)
        {
			byte[] dat = new byte[1];
			dat[0] = data;
			stream.Write (dat, 0, 1);
        }

        /// <summary>
        /// write 8 bit to the stream
        /// </summary>
        /// <param name="data"></param>
        void write8bit(byte data)
        {
            writeByte(data);
        }

        /// <summary>
        /// Write 16bits to the stream
        /// </summary>
        /// <param name="data"></param>
        public void writeInt16(ushort data)
        {
            byte[] buf = new byte[2];
            buf[0] = (byte)(data);
            buf[1] = (byte)(data >> 8);
            writeByteArray(buf,2);
        }

		public void writeInt32(Int32 data)
		{
			byte[] buf = new byte[4];
			buf[0] = (byte)(data);
			buf[1] = (byte)(data >> 8);
			buf[2] = (byte)(data >> 16);
			buf[3] = (byte)(data >> 24);
			writeByteArray(buf,4);
		}

		public void writeTag(byte[] tag)
		{
			writeByte (tag[0]);
			writeByte (tag[1]);
			writeByte (tag[2]);
			writeByte (tag[3]);
		}

        public void writeSample(uint sample,WavConfig config)
        {
            if(config.bitsPerSample == WavConfig.BPS_16BIT)
            {
                writeInt16((ushort)sample);
				dataLen += 2;
            }
            else if(config.bitsPerSample == WavConfig.BPS_8BIT)
            {
                write8bit((byte)sample);
				dataLen += 1;
            }
        }

		public void ResetStream()
		{
			stream.Dispose ();
			stream = new MemoryStream ();
			dataLen = 0;
		}
        
		#region IDisposable implementation
		public void Dispose ()
		{
			stream.Dispose ();
		}
		#endregion
    }
}
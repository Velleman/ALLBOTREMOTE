using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALLBOT
{
    class Uart2Sound
    {
        WavStream stream;
        WavConfig config;
		int dataOffset;
        public Uart2Sound(WavConfig config, int baud)
        {
            this.stream = new WavStream();
            this.config = config;
            config.samplesPerBit = (this.config.sampleRate / 1000.0) * (1000.0 / baud);
        }

		public void WriteHeader(WavConfig config)
		{
			var blockAlign = config.channels * (config.bitsPerSample / 8);
			stream.writeTag (System.Text.Encoding.ASCII.GetBytes (WavConfig.tagRiff));
			stream.writeInt32 (0);
			stream.writeTag(System.Text.Encoding.ASCII.GetBytes (WavConfig.tagWave));
			stream.writeTag(System.Text.Encoding.ASCII.GetBytes (WavConfig.tagFmt));
			stream.writeInt32 (16);
			stream.writeInt16(WavConfig.LIBWAV_FORMAT_PCM);
			stream.writeInt16 (config.channels);
			stream.writeInt32 ((int)config.sampleRate);
			stream.writeInt32 ((int)(config.sampleRate * blockAlign));
			stream.writeInt16 ((ushort)blockAlign);
			stream.writeInt16 (config.bitsPerSample);
			stream.writeTag(System.Text.Encoding.ASCII.GetBytes(WavConfig.tagData));
			dataOffset = (int)stream.Stream.Position;
			stream.writeInt32 (0);
		}

        public void Fill(int ms, uint value)
        {
            uint numberOfSamples, i, j;
            numberOfSamples = (uint)((config.sampleRate / 1000.0) * ms);

            for (i = 0; i < numberOfSamples; i++)
            {
                for (j = 0; j < config.channels; j++)
                {
                    stream.writeSample(value, config); // silence
                }
            }
        }

        public void WriteBit(byte value)
        {
            double i = config.samplesPerBit;

            i--;
            for (int j = 0; j < this.config.channels; j++)
            {
                stream.writeSample((uint)(Convert.ToBoolean(value) ? 0 : 30000), config);
            }
            

            while (i-- > 0)
            {
                for (int j = 0; j < this.config.channels; j++)
                {
                    stream.writeSample(0, config);
                }
            }
        }


        public byte[] GetData()
        {
			stream.Stream.Position = dataOffset;
			stream.writeInt32 ((int)stream.Stream.Length);
			return stream.Stream.ToArray();
        }

        public void Reset()
		{
			stream.ResetStream ();
			dataOffset = 0;
        }
    }
}
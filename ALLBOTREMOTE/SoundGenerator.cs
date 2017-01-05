using System;

namespace ALLBOT
{
	public class SoundGenerator
	{
		WavConfig config;
		Uart2Sound soundGenerator;

		public SoundGenerator ()
		{
			this.config = new WavConfig ();
			this.config.sampleRate = WavConfig.SR_44100;
			this.config.channels = WavConfig.STEREO;
			this.config.bitsPerSample = WavConfig.BPS_16BIT;
			soundGenerator = new Uart2Sound (config, 2400);
		}

		public byte[] Generate(string command)
		{
			soundGenerator.Reset ();
			//soundGenerator.WriteHeader (this.config);
			soundGenerator.Fill (100,0);
			char[] dataArray = command.ToCharArray ();
			int iterator = 0;
			byte c;
			while (iterator < command.Length) {
				soundGenerator.WriteBit (0);
				c = (byte)dataArray [iterator];
				for (int i = 0; i < 8; i++) {
					soundGenerator.WriteBit((byte)(c & 0x01));
					c >>=1;
				}
				soundGenerator.WriteBit (1);
				iterator++;

			}
			//soundGenerator.Fill(100,0);
			byte[] sound = soundGenerator.GetData();
			return sound;
		}
	}
}


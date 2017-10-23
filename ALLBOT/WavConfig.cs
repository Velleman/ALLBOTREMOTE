using System;

namespace ALLBOT
{
    class WavConfig
    {
        public const int SR_128000 = 128000; // /**< 128kHz */
        public const int SR_88200 = 88200; // /**< 88.2kHz */
        public const int SR_44100 = 44100; // /**< 44.1kHz */
        public const int SR_16000 = 16000; // /**< 16kHz */
        public const int SR_8000 = 8000; // /**< 8kHz 

        public const int LIBWAV_FORMAT_PCM = 1;

        public const int MONO = 1; /**< 1 channel (mono) */
        public const int STEREO = 2; /**< 2 channels (stereo) */

        public const int BPS_8BIT = 8; /**<  8 bits per sample */
        public const int BPS_16BIT = 16; /**< 16 bits per sample */

        public UInt16 channels;
        public UInt32 sampleRate;
        public UInt16 bitsPerSample;
        public double samplesPerBit;

		public const string tagRiff = "RIFF";
		public const string tagWave = "WAVE";
		public const string tagFmt = "fmt ";
		public const string tagData = "data";
    }
}
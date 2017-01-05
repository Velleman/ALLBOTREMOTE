using Android.Media;

namespace ALLBOT
{
	public class SoundPlayer
	{
		public SoundPlayer ()
		{
            
		}

		public static void Play(byte[] data)
		{
            AudioTrack audioTrack = new AudioTrack(Stream.Music, 44100, ChannelConfiguration.Stereo, Android.Media.Encoding.Pcm16bit, data.Length, AudioTrackMode.Static);
            audioTrack.Write(data, 0, data.Length);
            audioTrack.Play();
		}


	}

}


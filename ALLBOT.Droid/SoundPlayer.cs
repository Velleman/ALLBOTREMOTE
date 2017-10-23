using Android.Media;

namespace ALLBOT.Droid
{
	public class SoundPlayer : ISoundPlayer
	{
		public SoundPlayer ()
		{
            
		}

		public void Play(byte[] data)
		{
            AudioTrack audioTrack = new AudioTrack(Stream.Music, 44100, ChannelOut.Stereo, Android.Media.Encoding.Pcm16bit, data.Length, AudioTrackMode.Static);
            audioTrack.Write(data, 0, data.Length);
            while (audioTrack.State != AudioTrackState.Initialized);
            audioTrack.SetNotificationMarkerPosition(audioTrack.BufferSizeInFrames);
            audioTrack.MarkerReached += audioTrack_MarkerReached;            
            audioTrack.Play();          
            
		}

        void audioTrack_MarkerReached(object sender, AudioTrack.MarkerReachedEventArgs e)
        {
            e.Track.Release();
        }
    }

}


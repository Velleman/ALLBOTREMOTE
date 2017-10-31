using System;
using Android.Content;
using Android.Media;
using Android.OS;

namespace ALLBOT.Droid
{
    public class SoundPlayer : ISoundPlayer
    {
        private AudioTrack _audioTrack;
        private int _minBufferSize;
        public SoundPlayer()
        {
            _minBufferSize = AudioTrack.GetMinBufferSize(44100, ChannelOut.Stereo, Encoding.Pcm16bit);
            _audioTrack = new AudioTrack(Stream.Music, 44100, ChannelOut.Stereo, Android.Media.Encoding.Pcm16bit, _minBufferSize, AudioTrackMode.Stream);
            while (_audioTrack.State != AudioTrackState.Initialized) ;
            _audioTrack.Play();
        }

        public void Play(byte[] data)
        {

            var chuncks = Math.Round((decimal)(data.Length / _minBufferSize), MidpointRounding.AwayFromZero);
            for (int i = 0; i <= chuncks; i++)
            {
                var newData = CreateStreamBuffer(_minBufferSize, data, _minBufferSize * (int)chuncks);
                _audioTrack.Write(newData, 0, newData.Length);
            }

        }

        private byte[] CreateStreamBuffer(int size, byte[] data, int index)
        {
            byte[] newData = new byte[size];
            data.CopyTo(newData, index);
            return newData;
        }

        /*void audioTrack_MarkerReached(object sender, AudioTrack.MarkerReachedEventArgs e)
        {
            e.Track.Release();
        }*/
    }

}


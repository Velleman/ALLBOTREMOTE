using System;
using AVFoundation;
using AudioToolbox;
using Foundation;
using System.Runtime.InteropServices;
using ALLBOT;

namespace ALLBOTREMOTE
{
	public class SoundPlayer : ISoundPlayer
	{

		public SoundPlayer ()
		{
			
			//SetupAudio ();
		}

		public void Play(byte[] data)
		{
			AVAudioPlayer player = AVAudioPlayer.FromData (NSData.FromArray (data));
			player.PrepareToPlay ();
			player.Play ();
		}


	}

}


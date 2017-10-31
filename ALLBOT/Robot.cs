using System;
using System.Text;

namespace ALLBOT
{
    public class Robot
    {
        SoundGenerator generator;
        ISoundPlayer player;
        public int MoveSpeed { get; set; }
        public int Speed { get; set; }
        private object thisLock = new object();

        public Robot(ISoundPlayer sp)
        {
            generator = new SoundGenerator();
            player = sp;
        }

        public void SetAmplitude(uint amp)
        {
            generator.SetAmplitude(amp);
        }

        private void SendCommand(string command)
        {
            lock(thisLock)
            {
                var data = generator.Generate(command);
#if DEBUG
                //var text =ByteArrayToString(data);
                //initiate self-destruct sequence
                //Console.WriteLine(text);
#endif
                player.Play(data);
            }
        }
#if DEBUG
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
#endif

        public void WalkForward(int repeat)
        {
            SendCommand("<WF " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void WalkBackward(int repeat)
        {
            SendCommand("<WB " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void WalkLeft(int repeat)
        {
            SendCommand("<WL " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void WalkRight(int repeat)
        {
            SendCommand("<WR " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void TurnLeft(int repeat)
        {
            SendCommand("<TL " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void TurnRight(int repeat)
        {
            SendCommand("<TR " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void LeanLeft(int repeat)
        {
            SendCommand("<LL " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void LeanRight(int repeat)
        {
            SendCommand("<LR " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void LeanForward(int repeat)
        {
            SendCommand("<LF " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void LeanBackwards(int repeat)
        {
            SendCommand("<LB " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void WaveFrontLeft(int repeat)
        {
            SendCommand("<FL " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void WaveFrontRight(int repeat)
        {
            SendCommand("<FR " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void WaveBackLeft(int repeat)
        {
            SendCommand("<RL " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void WaveBackRight(int repeat)
        {
            SendCommand("<RR " + repeat + " " + MoveSpeed + ">\r\n");
        }

        public void Chirp(int repeat)
        {
            SendCommand("<CH " + repeat + " " + Speed  + ">\r\n");
        }
    }
}

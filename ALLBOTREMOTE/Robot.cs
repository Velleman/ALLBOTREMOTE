using System;

namespace ALLBOT
{
    public class Robot
    {
        SoundGenerator generator;
        SoundPlayer player;
        public int MoveSpeed { get; set; }
        public int Speed { get; set; }

        public Robot()
        {
            generator = new SoundGenerator();
            player = new SoundPlayer();
        }

        private void SendCommand(string command)
        {
            var data = generator.Generate(command);
            SoundPlayer.Play(data);
        }

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

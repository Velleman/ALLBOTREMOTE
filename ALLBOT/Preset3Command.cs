namespace ALLBOT
{
    public class Preset3Command : IDPadCommand
	{
		Robot robot;
		public int Repeat{ get; set;}
		public bool DPadRotated{ get; set;}
		public Preset3Command (Robot robot)
		{
			this.robot = robot;
			Repeat = 1;
			DPadRotated = false;
		}

		#region IDPadCommand implementation
		public void UpAction ()
		{
			robot.WaveFrontRight (Repeat);
		}
		public void LeftAction ()
		{
			robot.WaveFrontLeft (Repeat);
		}
		public void DownAction ()
		{
			robot.WaveBackLeft (Repeat);
		}
		public void RightAction ()
		{
			robot.WaveBackRight (Repeat);
		}
		public void MiddleAction ()
		{
			robot.Chirp (Repeat);
		}
		#endregion


	}
}


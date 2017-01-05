using System;

namespace ALLBOT
{
	public class Preset6Command : IDPadCommand
	{
		Robot robot;
		public int Repeat{ get; set;}
		public bool DPadRotated { get; private set;}
		public Preset6Command (Robot _robot)
		{
			robot = _robot;
			this.Repeat = 5;
			DPadRotated = true;
		}

		#region IDPadCommand implementation
		public void UpAction ()
		{
			robot.WalkForward (Repeat);
		}
		public void LeftAction ()
		{
			robot.WalkLeft (Repeat);
		}
		public void DownAction ()
		{
			robot.WalkBackward (Repeat);
		}
		public void RightAction ()
		{
			robot.WalkRight (Repeat);
		}
		public void MiddleAction ()
		{
			robot.Chirp (Repeat);
		}
		#endregion


	}
}


using System;

namespace ALLBOT
{
	public class Preset1Command : IDPadCommand
	{
		Robot robot;
		public int Repeat{ get; set;}
		public bool DPadRotated{get; private set;}
		public Preset1Command (Robot _robot)
		{
			this.robot = _robot;
			this.Repeat = 1;
			DPadRotated = true;
		}
			
		#region IDPadCommand implementation
		public void UpAction ()
		{
			robot.WalkForward (Repeat);
		}
		public void LeftAction ()
		{
			robot.TurnLeft (Repeat);
		}
		public void DownAction ()
		{
			robot.WalkBackward (Repeat);
		}
		public void RightAction ()
		{
			robot.TurnRight (Repeat);
		}
		public void MiddleAction ()
		{
			robot.Chirp (Repeat);
		}
		#endregion
	}
}


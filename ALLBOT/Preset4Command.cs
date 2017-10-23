namespace ALLBOT
{
    public class Preset4Command : IDPadCommand
	{
		Robot robot;
		public int Repeat{ get; set;}
		public bool DPadRotated{ get; set;}
		public Preset4Command (Robot _robot)
		{
			robot = _robot;
			Repeat = 1;
			DPadRotated = true;
		}

		#region IDPadCommand implementation
		public void UpAction ()
		{
			robot.LeanForward (Repeat);
		}
		public void LeftAction ()
		{
			robot.LeanLeft (Repeat);
		}
		public void DownAction ()
		{
			robot.LeanBackwards (Repeat);
		}
		public void RightAction ()
		{
			robot.LeanRight (Repeat);
		}
		public void MiddleAction ()
		{
			robot.Chirp (Repeat);
		}
		#endregion


	}
}


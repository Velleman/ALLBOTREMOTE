using System;

namespace ALLBOT
{
	public interface IDPadCommand 
	{
		bool DPadRotated{get;}
		void UpAction();
		void LeftAction();
		void DownAction();
		void RightAction();
		void MiddleAction();
	}
}


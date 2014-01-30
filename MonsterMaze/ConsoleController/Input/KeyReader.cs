using System;

namespace MonsterMaze.ConsoleController.Input
{
	public static class KeyReader
	{
		static readonly Func<string, ControllerMessage> BuildPlayerMessage = s => new ControllerMessage { Header = "player", Body = s };

		public static ControllerMessage Read(ConsoleKeyInfo keyInfo)
		{
			switch (keyInfo.Key)
			{
				case(ConsoleKey.W):
					return BuildPlayerMessage("up");

				case (ConsoleKey.S):
					return BuildPlayerMessage("down");

				case (ConsoleKey.A):
					return BuildPlayerMessage("left");

				case (ConsoleKey.D):
					return BuildPlayerMessage("right");

				case (ConsoleKey.Spacebar):
					return BuildPlayerMessage("wait");

				case (ConsoleKey.R):
					return new ControllerMessage { Header = "game", Body = "rewind" };
				
				case ConsoleKey.Q:
					return new ControllerMessage {Header = "game", Body = "quit"};
			}
			return new ControllerMessage { Header = "error", Body = "bad input"};
		}
	}
}
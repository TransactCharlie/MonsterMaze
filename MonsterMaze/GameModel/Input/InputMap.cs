namespace MonsterMaze.GameModel.Input
{
	public static class InputMap
	{
		public static Point Transform(string direction, Point location, int speed)
		{
			switch (direction)
			{
				case (GameConstants.UP):
					return location.Up();
				case (GameConstants.DOWN):
					return location.Down();
				case (GameConstants.LEFT):
					return location.Left();
				case (GameConstants.RIGHT):
					return location.Right();
				case (GameConstants.WAIT):
					return location;
				default:
					return location;
			}
		}
	}
}
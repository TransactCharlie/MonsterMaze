using MonsterMaze.GameModel.Tiles;

namespace MonsterMaze.GameModel
{
	public class Message
	{
		public string Name;
		public string Type;
		public string From;
		public string To;
		public MessageTarget MessageTarget;

		public Message Invert()
		{
			return new Message
			{
				MessageTarget = MessageTarget,
				Name = Name,
				Type = Type,
				From = To,
				To = From
			};
		}
	}
}
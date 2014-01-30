using System.Linq;
using MonsterMaze.GameModel.Tiles;
using System.Collections.Generic;

namespace MonsterMaze.GameModel
{
	public class CollisionInfo
	{
		public bool Collision;
		public Tile Tile;
		public Tile Object;
		public Point Vector;
		public List<Message> Messages = new List<Message>();

		public bool Involves(params string[] names)
		{
			if(Tile != null && Object != null)
				return names.Any(n => n == Tile.Type) && names.Any(n => n == Object.Type);
			return false;
		}
		
		// This is used by the functions that determine who wins in a collision.
		public bool InitiatorReceiverCheck(string initiator, string receiver)
		{
			if (Tile != null && Object != null)
			{
				return (initiator == Tile.Type && receiver == Object.Type);
			}
			return false;
		}

		public bool WasReciever(Tile tile)
		{
			return Object.Name == tile.Name;
		}
	}
}
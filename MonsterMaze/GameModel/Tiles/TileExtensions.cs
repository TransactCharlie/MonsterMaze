using System;
using System.Collections.Generic;
using System.Linq;

namespace MonsterMaze.GameModel.Tiles
{
	public static class TileExtensions
	{
		public static Tile WithName(this TileSet tileSet, string name)
		{
			return tileSet.Tiles.FirstOrDefault(m => m.Name == name);
		}

		public static IEnumerable<Tile> WithType(this TileSet tileSet, string type)
		{
			return tileSet.Tiles.Where(m => m.Type == type);
		}

		public static void AddCollisionRules(this IEnumerable<Tile> tiles, Func<Tile,Func<CollisionInfo,Message>> rule)
		{
			tiles
				.ToList()
				.ForEach(t=> t.OnCollision.Add(rule(t)));
		}
		public static void AddUpdateBehaviour(this IEnumerable<Tile> tiles,  Func<Tile, Message> behaviour)
		{
			tiles
				.ToList()
				.ForEach(t => t.OnUpdate = behaviour);
		}

		public static IEnumerable<Tile> GetActiveTiles(this TileSet tileSet)
		{
			return tileSet.Tiles.Where(m => m.IsActive);
		}

		public static IEnumerable<Message> GetUpdates(this IEnumerable<Tile> tiles)
		{
			return tiles.Select(t => t.Update()).Where(m => m != null);
		}

		public static IEnumerable<Message> GetCollisionResponse(this Tile tile, CollisionInfo collisionInfo)
		{
			return tile.OnCollision.Select(_ => _(collisionInfo))
				.Where(m => m != null);
		}

		public static Message BuildMoveMessage(this Tile tile, Point to)
		{
			return new Message{
						Type = GameConstants.MOVEMENT,
						From = tile.Location.ToString(),
						To = to.ToString(),
						Name =  tile.Name
			       	};
		}
		public static Message BuildActiveMessage(this Tile tile, bool to)
		{
			return new Message
			{
				Type = GameConstants.ACTIVE,
				From = tile.IsActive.ToString(),
				To = to.ToString(),
				Name = tile.Name
			};
		}
		public static Message BuildSplatMessage(this Tile tile)
		{
			return new Message
			{
				Type = GameConstants.SPLAT,
				MessageTarget = MessageTarget.Static,
				From = tile.Location.ToString(),
				To = tile.Location.ToString(),
				Name = tile.Name
			};
		}

		public static Message BuildCrashMessage(this Tile tile)
		{
			return new Message
			{
				Type = GameConstants.CRASH,
				MessageTarget = MessageTarget.Static,
				From = tile.Location.ToString(),
				To = tile.Location.ToString(),
				Name = tile.Name
			};
		}
	}
}
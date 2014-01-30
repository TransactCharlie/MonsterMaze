using MonsterMaze.GameModel.Tiles;

namespace MonsterMaze.GameModel
{
	public static class CollisionInfoExtensions
	{
		public static bool WasCrushedByBolder(this CollisionInfo info,Tile tile)
		{
			return info.InitiatorReceiverCheck(GameConstants.BOULDER, tile.Type);
		}
		public static bool WasEatenByAMonster(this CollisionInfo info, Tile tile)
		{
			return info.Involves(GameConstants.STRONG_MONSTER, tile.Type);
		}
		public static bool FellInHole(this CollisionInfo info, Tile tile)
		{
			return info.Involves(GameConstants.HOLE, tile.Type);
		}
	}
}

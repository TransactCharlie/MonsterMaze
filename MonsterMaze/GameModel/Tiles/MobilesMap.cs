using System;
using System.Collections.Generic;

namespace MonsterMaze.GameModel.Tiles
{
	public static class MobilesMap
	{
		public static readonly Dictionary<char, Func<Point, Tile>> TileBuilder = new Dictionary<char, Func<Point, Tile>>{
			{'@', p => new Tile{Location = p, Token = '@', Name = GameConstants.PLAYER,Type = GameConstants.PLAYER, CanBeKilled = true, CanPush = true, CanFall = true}},
			{'m', p => new Tile{Location = p, Token = 'm', Name = GenerateName(GameConstants.WEAK_MONSTER), Type = GameConstants.WEAK_MONSTER, CanFall = true, CanBeKilled = true}},
			{'M', p => new Tile{Location = p, Token = 'M', Name = GenerateName(GameConstants.STRONG_MONSTER), Type = GameConstants.STRONG_MONSTER, CanFall = true, CanBeKilled = true, CanKill = true, CanPush = true}},
			{'0', p => new Tile{Location = p, Token = '0', Name = GenerateName(GameConstants.BOULDER), Type = GameConstants.BOULDER, CanBePushed = true, CanKill = true, CanFall = true}},
			{'H', p => new Tile{Location = p, Token = 'H', Name = GenerateName(GameConstants.HOLE), Type = GameConstants.HOLE, CanBeFellInto = true, CanKill = true, CanBeKilled = true}},
			{'.', p => null},
            {'#', p => null},
        };

		private static Dictionary<string, int> _nameCounter = new Dictionary<string, int>();
		private static string GenerateName(string name)
		{
			if (_nameCounter.ContainsKey(name))
				_nameCounter[name]++;
			else
				_nameCounter[name] = 1;

			return String.Format("{0}_{1}", name, _nameCounter[name]);
		}
	}
}
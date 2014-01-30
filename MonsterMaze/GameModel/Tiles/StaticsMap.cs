using System;
using System.Collections.Generic;

namespace MonsterMaze.GameModel.Tiles
{
	public static class StaticsMap
	{
		public static readonly Dictionary<char, Func<Point, Tile>> TileBuilder = new Dictionary<char, Func<Point, Tile>>{
			{'#', p => new Tile{Location = p, Token = '#', IsTraversible = false}},
			{'.', p => new Tile{Location = p, Token = '.', IsTraversible = true}},
			{'b', p => new Tile{Location = p, Token = 'b', IsTraversible = true}}
		};
	}
}
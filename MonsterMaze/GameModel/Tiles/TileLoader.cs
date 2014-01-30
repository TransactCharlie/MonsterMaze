using System;
using System.Collections.Generic;
using System.Linq;

namespace MonsterMaze.GameModel.Tiles
{
    public class TileLoader
    {
        public TileSet Load(char[,] level, Dictionary<char,Func<Point, Tile>> map)
        {
            var height = level.GetHeight() + 1;
            var width = level.GetWidth() + 1;

            var tileSet = from y in Enumerable.Range(0, height)
                          from x in Enumerable.Range(0, width)
                          let tiles = map[level[x, y]](new Point(x, y))
                          where tiles != null
                          select tiles;
            
            return new TileSet(tileSet);
        }
    }
}
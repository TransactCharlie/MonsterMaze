using MonsterMaze;
using MonsterMaze.GameModel.Tiles;
using NUnit.Framework;

namespace UnitTests.ModelTests
{
	[TestFixture]
	public class TileSetTests
	{
		readonly char[,] _level = new[,] {
								{'#', '#', '#', '#'},
								{'#', '.', '.', '#'},
								{'#', '.', '.', '#'},
								{'#', '#', '#', '#'}
							};

		[Test]
		public void CanConstruct()
		{
			var loader = new TileLoader();
			var tileSet = loader.Load(_level, StaticsMap.TileBuilder);
			Assert.IsNotNull(tileSet.Tiles);
		}

		[Test]
		public void CanIndex()
		{
			var loader = new TileLoader();
			var tileSet = loader.Load(_level, StaticsMap.TileBuilder);
			Assert.AreEqual(tileSet[1,1].Token, '.');
		}

		[Test]
		public void CanUpdate()
		{
			var loader = new TileLoader();
			var tileSet = loader.Load(_level, StaticsMap.TileBuilder);
			tileSet[1, 1].Location = new Point(0,0);
			tileSet[0, 0].Location = new Point(1, 1);
			tileSet.Update();
			Assert.AreEqual(tileSet[1, 1].Token, '#');
		}
	}
}

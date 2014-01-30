using MonsterMaze.GameModel.Tiles;
using NUnit.Framework;

namespace UnitTests.ModelTests
{
    [TestFixture]
    public class LoaderTests
    {
        readonly char[,] _level = new[,] {
                                {'#', '#', '#', '#'},
                                {'#', '.', '.', '#'},
                                {'#', '.', '.', '#'},
                                {'#', '#', '#', '#'}};

        [Test]
        public void CanLoad()
        {
            var staticLoader = new TileLoader();
            var statics = staticLoader.Load(_level, StaticsMap.TileBuilder);
            Assert.That(statics[0,0].Token == '#');
            Assert.That(statics[1,1].Token == '.');
        }
    }
}
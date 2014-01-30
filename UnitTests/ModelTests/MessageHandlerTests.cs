using MonsterMaze;
using MonsterMaze.GameModel;
using MonsterMaze.GameModel.Tiles;
using NUnit.Framework;

namespace UnitTests.ModelTests
{
	[TestFixture]
	public class MessageHandlerTests
	{

		readonly char[,] _level = new[,] {
								{'.', '.', '.', '.', '.'},
								{'.', '.', '.', 'M', '.'},
								{'.', '.', '.', '.', '.'},
								{'.', '.', '@', '.', '.'},
								{'.', '.', '.', '.', '.'}
							};

		[Test]
		public void CanResolveMessage()
		{
			var loader = new TileLoader();
			var mobiles = loader.Load(_level, MobilesMap.TileBuilder);
			var messageHandler = new MessageHandler(mobiles, null);

			var player = mobiles.WithName("player");
			
			var previousPlayerLocation = player.Location;

			var message = new Message{
				Name = "player",
				Type = "movement",
				From = player.Location.ToString(),
				To = new Point(player.Location.X - 1, player.Location.Y).ToString()
			};

			messageHandler.Resolve(message);

			Assert.AreNotEqual(previousPlayerLocation,player.Location);
			Assert.AreEqual(previousPlayerLocation.X - 1, player.Location.X);

			messageHandler.Resolve(message.Invert());
			Assert.AreEqual(previousPlayerLocation, player.Location);
			
		}
	}
}

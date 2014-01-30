using System.Linq;
using MonsterMaze;
using MonsterMaze.GameModel;
using MonsterMaze.GameModel.Behaviour;
using MonsterMaze.GameModel.Tiles;
using NUnit.Framework;

namespace UnitTests.ModelTests
{
	[TestFixture]
	public class ModelTests
	{
		readonly IModelBehaviour behaviourModel = new BehaviourModel();
		// Data
		readonly char[,] _level = new[,] {
								{'#', '#', '#', '#'},
								{'#', '.', '.', '#'},
								{'#', '.', '.', '#'},
								{'#', '#', '#', '#'}
							};

		readonly char[,] _mobs = new[,] {
								{'.', '.', '.', '.'},
								{'.', '.', '.', '.'},
								{'.', '@', '.', '.'},
								{'.', '.', '.', '.'}
							};
		[Test]
		public void CanInjectControllerWithLevel()
		{

			var model = new Model(_level, _mobs, behaviourModel);
			model.InputBuffer.Send(new ControllerMessage { Header = "player", Body = "up" });

			// Check that the arrays are the same length
			Assert.That(model.Statics.Tiles.Count() == _level.Length);

			// Check that we get back what we put in
			for (var i = 0; i < _level.GetWidth(); i++)
			{
				for (var j = 0; j < _level.GetHeight(); j++)
				{
					var token = _level[i, j];
					if(token == '@')
						Assert.That(token == model.Mobiles[i,j].Token);
					else
						Assert.That(token == model.Statics[i,j].Token);

				}
			}
		}

		[Test]
		public void CanUpdatePlayerLocation()
		{
			var model = new Model(_level, _mobs, behaviourModel);
			var originalPlayerLocation = model.Mobiles.WithName("player").Location;
			var message = new ControllerMessage{Body = "down", Header = "player"};
			model.InputBuffer.Send(message);
			var newPlayerLocation = model.Mobiles.Tiles.FirstOrDefault(p => p.Name == "player").Location;
			Assert.AreNotEqual(originalPlayerLocation, newPlayerLocation);
			Assert.AreEqual(originalPlayerLocation.Y + 1, newPlayerLocation.Y);
		}
	}
}
using MonsterMaze;
using MonsterMaze.GameModel;
using NUnit.Framework;

namespace UnitTests.ModelTests
{
	[TestFixture]
	public class ControllerMessageMediatorTests
	{
		[Test]
		public void CanListenForMessages()
		{
			var listener = new MessageListener<ControllerMessage>();
			var mediator = new ControllerMessageMediator();
			mediator.Subscribe("test", listener);
			var testMessage = new ControllerMessage { Header = "test", Body = "testing123" };
			var uselessMessage = new ControllerMessage { Header = "poop", Body = "xxxx" };
			var callCount = 0;
			listener.MessageRecieved += m => callCount++;
			mediator.Send(testMessage);
			mediator.Send(uselessMessage);
			Assert.AreEqual(1, callCount);
		}
	}
}

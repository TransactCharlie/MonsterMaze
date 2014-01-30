using System.Collections.Generic;

namespace MonsterMaze.GameModel
{
	public class ControllerMessageMediator
	{
		Dictionary<string, IQueueListener<ControllerMessage>> listners = new Dictionary<string, IQueueListener<ControllerMessage>>();

		public void Subscribe(string listenFor, IQueueListener<ControllerMessage> listener)
		{
			listners.Add(listenFor, listener);
		}

		public void Send(ControllerMessage message)
		{
			if(listners.ContainsKey(message.Header))
				listners[message.Header].Send(message);
		}
	}
}
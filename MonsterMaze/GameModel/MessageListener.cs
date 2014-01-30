namespace MonsterMaze.GameModel
{
	public class MessageListener<T> : IQueueListener<T>
	{
		public delegate void OnMessageRecieved(T message);
		public event OnMessageRecieved MessageRecieved;
	
		public void Send(T message)
		{
			if (MessageRecieved != null)
				MessageRecieved(message);
		}
	}
}
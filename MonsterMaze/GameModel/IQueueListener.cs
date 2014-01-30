using System.Collections.Generic;

namespace MonsterMaze.GameModel
{
	public interface IQueueListener<T>
	{
		void Send(T message);
		event MessageListener<T>.OnMessageRecieved MessageRecieved;
	}
}
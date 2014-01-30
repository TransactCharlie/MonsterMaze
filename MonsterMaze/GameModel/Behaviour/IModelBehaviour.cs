using System;
using System.Collections.Generic;
using MonsterMaze.GameModel.Tiles;

namespace MonsterMaze.GameModel.Behaviour
{
	public interface IModelBehaviour
	{
		void SetupBehaviours(TileSet mobiles, TileSet statics, List<Func<Message, Message>> gameMessageReactor);
	}
}
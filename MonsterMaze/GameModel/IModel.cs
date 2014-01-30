using System.Collections.Generic;
using MonsterMaze.GameModel.Tiles;

namespace MonsterMaze.GameModel
{
	public interface IModel
	{
		TileSet Statics { get; }
		TileSet Mobiles { get; }
		int Width { get;}
		int Height { get; }
		ControllerMessageMediator InputBuffer { get; set; }
		Stack<Message> CurrentTurnMessages {get;}
		event Model.OnTurnEnd TurnEnd;
	}
}
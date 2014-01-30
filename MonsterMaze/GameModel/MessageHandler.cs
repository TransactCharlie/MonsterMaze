using System;
using MonsterMaze.GameModel.Tiles;

namespace MonsterMaze.GameModel
{
	public class MessageHandler
	{        
		private readonly TileSet _mobiles;
		private readonly TileSet _statics;

		public MessageHandler(TileSet mobiles, TileSet statics)
		{
			_mobiles = mobiles;
			_statics = statics;
		}

		public void Resolve(Message message)
		{
			switch (message.MessageTarget)
			{
				case MessageTarget.Mobile:
					{
						var target = _mobiles.WithName(message.Name);
						if (target != null)
							switch (message.Type)
							{
								case (GameConstants.MOVEMENT):
									target.MoveTo(message.To.ToPoint());
									break;
								case (GameConstants.ACTIVE):
									target.IsActive = Convert.ToBoolean(message.To);
									break;
							}
					}
					break;
				case MessageTarget.Static:
					{
						switch (message.Type)
						{
							case (GameConstants.SPLAT):
								{
									var tile = _statics[message.From.ToPoint()];
									tile.Token = (tile.Token == '.' ? 'b' : '.');
								}
								break;
							case (GameConstants.CRASH):
								{
									var tile = _statics[message.From.ToPoint()];
									tile.Token = (tile.Token == '.' ? 'h' : '.');
								}
								break;
						}
					}
					break;

				case MessageTarget.Game:
					{
						switch (message.Type)
						{
							case(GameConstants.NEXT_LEVEL):
								//Load next level;
								break;
						}
						switch (message.Type)
						{
							case (GameConstants.LOAD_LEVEL):
								//Load level X;
								break;
						}
					}
					break;
			}
		}
	}
}
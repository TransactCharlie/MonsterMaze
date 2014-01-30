using System;
using System.Linq;
using System.Collections.Generic;
using MonsterMaze.GameModel.Input;
using MonsterMaze.GameModel.Tiles;
using MonsterMaze.GameModel.Behaviour;

namespace MonsterMaze.GameModel
{
	public class Model : IModel
	{
		public TileSet Mobiles { get; private set; }
		public TileSet Statics { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public ControllerMessageMediator InputBuffer { get; set; }
		
		public delegate void OnTurnEnd();
		public event OnTurnEnd TurnEnd;

		private readonly Stack<Stack<Message>> _turns = new Stack<Stack<Message>>();
		public Stack<Message> CurrentTurnMessages { get { return _turns.Any() ? _turns.Peek() : null; } }
		
		private readonly MessageHandler _messageHandler;
		private readonly MessageListener<ControllerMessage> _playerInputQueue = new MessageListener<ControllerMessage>();
		private readonly MessageListener<ControllerMessage> _gameInputQueue = new MessageListener<ControllerMessage>();
		
		private readonly List<Func<Message,Message>> _gameMessageReactor = new List<Func<Message, Message>>();
		 

		public Model(char[,] statics, char[,] mobiles, IModelBehaviour behaviourModel)
		{
			InputBuffer = new ControllerMessageMediator();
			var loader = new TileLoader();

			Statics = loader.Load(statics, StaticsMap.TileBuilder);

			Statics.AddCollisionRules(
				(mobile, tile) => !tile.IsTraversible);

			Mobiles = loader.Load(mobiles, MobilesMap.TileBuilder);
			
			Mobiles.AddCollisionRules(
				(mobile, tile) => !(mobile.CanPush && tile.CanBePushed),
				(mobile, tile) => !(mobile.CanKill && tile.CanBeKilled),
				(mobile, tile) => !(mobile.CanFall && tile.CanBeFellInto)
				);

			Width = statics.GetWidth();
			Height = statics.GetHeight();

			behaviourModel.SetupBehaviours(Mobiles, Statics ,_gameMessageReactor);
			
			_messageHandler = new MessageHandler(Mobiles, Statics);
			
			InputBuffer.Subscribe(GameConstants.PLAYER, _playerInputQueue);
			InputBuffer.Subscribe(GameConstants.GAME, _gameInputQueue);
			
			_playerInputQueue.MessageRecieved += ResolvePlayerInput;
			_gameInputQueue.MessageRecieved += ResolveGameMessage;
		}

		private void ResolvePlayerInput(ControllerMessage inputMessage)
		{
			var player = Mobiles.WithName(GameConstants.PLAYER);
			player.OnUpdate = p =>  MovePlayer(p,inputMessage);

			var turn = new Stack<Message>();

			foreach (var message in Mobiles.GetActiveTiles().GetUpdates())
			{
				var action = ResolveAction(message);
				if (!action.IsValid) continue;
				foreach (var data in action.FrameData)
				{
					_messageHandler.Resolve(data);
					turn.Push(data);
				}
				EndTurn();
			}
			_turns.Push(turn);
		}

		private void ResolveGameMessage(ControllerMessage inputMessage)
		{
			switch (inputMessage.Body) 
			{
				case (GameConstants.REWIND):
					if (_turns.Any())
					{
						var turn = _turns.Pop();
						while(turn.Any())
							_messageHandler.Resolve(turn.Pop().Invert());
						EndTurn();
					}
				break;
			}			
		}

		private void EndTurn()
		{
			Mobiles.Update();
			if (TurnEnd != null)
				TurnEnd();
		}

		private Frame ResolveAction(Message message, Frame frame = null)
		{
			if (frame == null)
				frame = new Frame();

			if (frame.IsValid && message != null)
			{
				switch (message.Type)
				{
					case (GameConstants.MOVEMENT):
						{
							var target = Mobiles.WithName(message.Name);
							var targetLocation = message.To.ToPoint();
							var mobileCollisionInfo = Mobiles.CollisionDetection(target, targetLocation);
							var staticCollisionInfo = Statics.CollisionDetection(target, targetLocation);

							frame.IsValid = !(mobileCollisionInfo.Collision || staticCollisionInfo.Collision);
							if (!frame.IsValid)
								return frame;

							mobileCollisionInfo.Messages.ForEach(m => ResolveAction(m, frame));
						}
						break;					
				}

				frame.FrameData.Add(message);
				frame.FrameData.AddRange(_gameMessageReactor.Select(r=> r(message)).Where(m=> m!= null));
				return frame;
			}
			return frame;
		}
		
		private Message MovePlayer(Tile player, ControllerMessage message)
		{
			if (player.IsActive)
			{
				var destination = InputMap.Transform(message.Body, player.Location, 1);
				return player.BuildMoveMessage(destination);
			}

			return null;
		} 
	}
}
using System;
using System.Collections.Generic;
using MonsterMaze.GameModel.Tiles;
using MonsterMaze.GameModel.Input;

namespace MonsterMaze.GameModel.Behaviour
{
	public class BehaviourModel : IModelBehaviour
	{

		static readonly Func<Tile, Func<Tile, bool>, Message> KillIf = (t, f) => f(t) ? t.BuildActiveMessage(false) : null;
		static readonly Func<Tile, CollisionInfo, Message> Pushed = (t, ci) => ci.WasReciever(t) ? t.BuildMoveMessage(t.Location + ci.Vector) : null;
		static readonly Func<Tile, CollisionInfo, Message> CrushedByBoulder = (tile, _) => KillIf(tile, _.WasCrushedByBolder);
		static readonly Func<Tile, CollisionInfo, Message> EatenByMonster = (tile, _) => KillIf(tile, _.WasEatenByAMonster);
		static readonly Func<Tile, CollisionInfo, Message> FellInHole = (tile, _) => KillIf(tile, _.FellInHole);
		Func<Message, string, bool> Deaded = (m, s) => (m.Name.Contains(s) && m.Type == GameConstants.ACTIVE && m.To == false.ToString());


		public void SetupBehaviours(TileSet mobiles, TileSet statics, List<Func<Message, Message>> gameMessageReactor)
		{

			var player = mobiles.WithName(GameConstants.PLAYER);
			var chaseBehaviour = MonsterAi.ChasePlayer(player);

			//Setup player behaviour
			player.OnCollision.Add(collision => EatenByMonster(player, collision));
			player.OnCollision.Add(collision => CrushedByBoulder(player, collision));
			player.OnCollision.Add(collision => FellInHole(player, collision));

			//Setup strong monster behaviour
			mobiles.WithType(GameConstants.STRONG_MONSTER).AddUpdateBehaviour(chaseBehaviour);
			mobiles.WithType(GameConstants.STRONG_MONSTER).AddCollisionRules(
				strongMonster => collision => CrushedByBoulder(strongMonster, collision));
			mobiles.WithType(GameConstants.STRONG_MONSTER).AddCollisionRules(
				strongMonster => collision => FellInHole(strongMonster, collision));
			
			//Setup weak monster behaviour
			mobiles.WithType(GameConstants.WEAK_MONSTER).AddUpdateBehaviour(chaseBehaviour);
			mobiles.WithType(GameConstants.WEAK_MONSTER).AddCollisionRules(
				weakMonster => collision => CrushedByBoulder(weakMonster, collision));
			mobiles.WithType(GameConstants.WEAK_MONSTER).AddCollisionRules(
				weakMonster => collision => FellInHole(weakMonster, collision));
			mobiles.WithType(GameConstants.WEAK_MONSTER).AddCollisionRules(
				weakMonster => collision => EatenByMonster(weakMonster, collision));

			gameMessageReactor.Add(message => 
				(Deaded(message, GameConstants.WEAK_MONSTER) || Deaded(message, GameConstants.PLAYER) || Deaded(message, GameConstants.STRONG_MONSTER))
					? statics[mobiles.WithName(message.Name).Location].BuildSplatMessage() : null);


			gameMessageReactor.Add(message =>
				Deaded(message, GameConstants.HOLE)
					? statics[mobiles.WithName(message.Name).Location].BuildCrashMessage() : null);

			//Setup boulder behaviour
			mobiles.WithType(GameConstants.BOULDER).AddCollisionRules(boulder => collision => Pushed(boulder,collision));
			mobiles.WithType(GameConstants.BOULDER).AddCollisionRules(boulder => collision => FellInHole(boulder,collision));
			
			//Setup hole behaviour
			mobiles.WithType(GameConstants.HOLE).AddCollisionRules(boulder => collision => CrushedByBoulder(boulder,collision));
		}

		
	}
}
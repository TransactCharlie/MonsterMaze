using System;
using MonsterMaze.GameModel.Tiles;

namespace MonsterMaze.GameModel.Behaviour
{
	public static class MonsterAi
	{
		public static Func<Tile, Func<Tile, Message>> ChasePlayer = p => c => ChaseTarget(p, c);

		private static Message ChaseTarget(Tile target, Tile chaser)
		{
            // The chaser will try to go in the directions that is closest to the player.
            
            // TODO: diff is a bad name but I can't think of a better on
            var diff = chaser.Location.NormalizedVector(target.Location);

            // diff suggests either go vertically or horizontally. Which is better?

		    var verticalMove = chaser.Location + new Point(0, diff.Y);
		    var horizontalMove = chaser.Location + new Point(diff.X, 0);

            if ( GetVectorLengthPointPoint(verticalMove, target.Location) < GetVectorLengthPointPoint(horizontalMove, target.Location) )
            {
                // TODO: should this check for a collision on this move? If there is collision should monster try the horizontal move?
                return chaser.BuildMoveMessage(verticalMove); 
            }

            if (GetVectorLengthPointPoint(horizontalMove, target.Location) < GetVectorLengthPointPoint(verticalMove, target.Location) )
            {
                // TODO: should this check for a collision on this move? If there is collision should monster try the vertical move?
                return chaser.BuildMoveMessage(horizontalMove);
            }

            // if either is as good then, y is preferable to x
            // TODO: Make this better, it's pretty atbitrary.
			var direction = diff.Y != 0 ? new Point(0, diff.Y) : new Point(diff.X, 0);
			return chaser.BuildMoveMessage(chaser.Location + direction); 
		}

        // TODO : Not sure where this should live.
        private static double GetVectorLengthPointPoint(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) ^ 2 + (a.Y - b.Y) ^ 2);
        }
	}
}
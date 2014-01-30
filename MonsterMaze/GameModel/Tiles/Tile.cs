using System;
using System.Collections.Generic;

namespace MonsterMaze.GameModel.Tiles
{
	public class Tile
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsActive = true;
		public Point Location { get; set; }
		public char Token { get; set; }
		
		// Static Logical properties
		public bool IsTraversible { get; set; }
		
		// Mobile logical properties
		public bool CanKill { get; set; }
		public bool CanBeKilled { get; set; }
		
		public bool CanPush { get; set; }
		public bool CanBePushed { get; set; }

		public bool CanFall{ get; set; }
		public bool CanBeFellInto{ get; set; }
		
		public Func<Tile, Message> OnUpdate { get; set; }
		public List<Func<CollisionInfo, Message>> OnCollision = new List<Func<CollisionInfo, Message>>();

		public Message Update()
		{
			if(OnUpdate != null)
				return OnUpdate(this);
			return null;
		}		

		public void MoveTo(Point location)
	    {
			Location = location;
		}	
			
		public override string ToString()
		{
			return String.Format("{0}:{1}", Name, Location);
		}
	}
}
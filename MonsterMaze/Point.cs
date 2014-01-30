using System;

namespace MonsterMaze
{
	public struct Point
	{
		public int X, Y;
		
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
		
		public bool Equals(Point other)
		{
			return other.X == X && other.Y == Y;
		}

		public static bool operator ==(Point left, Point right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Point left, Point right)
		{
			return !Equals(left, right);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (obj.GetType() != typeof (Point)) return false;
			return Equals((Point) obj);
		}

		public static Point operator +(Point a, Point b)
		{
			return new Point(a.X + b.X, a.Y + b.Y);
		}
		public static Point operator -(Point a, Point b)
		{
			return new Point(a.X - b.X, a.Y - b.Y);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X*397) ^ Y;
			}
		}

		public override string ToString()
		{
			return String.Format("{0},{1}", X, Y);
		}
	}
}
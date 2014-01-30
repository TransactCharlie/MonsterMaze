using System;
using System.Linq;

namespace MonsterMaze
{
	public static class StringExtensions
	{
		public static Point ToPoint(this string str)
		{
			var xy = str.Split(',').Select(int.Parse).ToArray();
			return new Point(xy[0], xy[1]);
		}
	}
}

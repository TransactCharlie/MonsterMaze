using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterMaze
{
    public static class PointExtensions
    {
        public static Point NormalizedVector(this Point self, Point point)
        {
            var x = point.X > self.X ? 1 : point.X == self.X ? 0 : -1;
            var y = point.Y > self.Y ? 1 : point.Y == self.Y ? 0 : -1;
            return new Point(x, y);
        }

        public static Point Up(this Point self, int range = 1)
        {
            return new Point(self.X, self.Y - range);
        }

        public static Point Down(this Point self, int range = 1)
        {
            return new Point(self.X, self.Y + range);
        }

        public static Point Left(this Point self, int range = 1)
        {
            return new Point(self.X - range, self.Y);
        }
        public static Point Right(this Point self, int range = 1)
        {
            return new Point(self.X + range, self.Y);
        }
    }
}

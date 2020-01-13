using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Cartography
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point() { }

        public static Point operator+ (Point p1, Point p2)
        {
            if (p1 is null || p2 is null)
                throw new ArgumentNullException("Null object");

            if (!p1.GetType().Equals(p2.GetType()) || !p1.GetType().Equals(typeof(Point)))
                throw new ArgumentException("Incompatible objects");

            Point point = new Point();
            point.X = p1.X + p2.X;
            point.Y = p1.Y + p2.Y;
            return point;
        }
            
    }
}

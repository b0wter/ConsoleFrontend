using System;
using System.Collections.Generic;
using System.Text;

namespace bCurses.Helpers
{
    public class Point
    {
        public Point()
        {
            //
        }

        public Point(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}

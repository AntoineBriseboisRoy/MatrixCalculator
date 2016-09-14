using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class Point:IComparable
    {
        public int B { get; private set; }
        public int A { get; private set; }
        public Point(int a, int b)
        {
            A = a;
            B = b;
        }

        public int CompareTo(object o)
        {
            Point asdasdasd = (o as Point);
            return this.A.CompareTo(asdasdasd.A);
        }
    }
}

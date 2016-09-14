using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class InfiniteRep
    {
        protected float Constante1 { get; set; }
        protected float Coefficient1 { get; set; }
        protected char Variable1 { get; set; }

        public InfiniteRep(float a, float b, char v)
        {
            Constante1 = a;
            Coefficient1 = b;
            Variable1 = v;
        }

        public static InfiniteRep operator *(InfiniteRep a, float b)
        {
            return new InfiniteRep(a.Constante1 * b, a.Coefficient1 * b, a.Variable1);
        }
        public static InfiniteRep operator *(float b, InfiniteRep a)
        {
            return new InfiniteRep(a.Constante1 * b, a.Coefficient1 * b, a.Variable1);
        }
        public static InfiniteRep operator /(float b, InfiniteRep a)
        {
            return new InfiniteRep(a.Constante1 / b, a.Coefficient1 / b, a.Variable1);
        }
        public static InfiniteRep operator /(InfiniteRep a, float b)
        {
            return new InfiniteRep(a.Constante1 / b, a.Coefficient1 / b, a.Variable1);
        }
        public static InfiniteRep operator /(InfiniteRep a, InfiniteRep b)
        {
            return new InfiniteRep(0,a.Constante1/b.Coefficient1,b.Variable1);
        }
        public static InfiniteRep operator +(InfiniteRep a, InfiniteRep b)
        {
            return new InfiniteRep(a.Constante1 + b.Constante1, a.Coefficient1 + b.Coefficient1, a.Variable1);
        }
        public static InfiniteRep operator -(InfiniteRep a, InfiniteRep b)
        {
            return new InfiniteRep(a.Constante1 - b.Constante1, a.Coefficient1 - b.Coefficient1, a.Variable1);
        }
        public override string ToString()
        {
            string a = Constante1.ToString() + "-" + Coefficient1.ToString() + Variable1;

            if (Constante1 == 0 && Coefficient1 == -1) a = Variable1.ToString();
            else if (Constante1 == 0 && Coefficient1 == 1) a = "-" + Variable1.ToString();
            else if (Constante1 == 0 && Coefficient1 == 0) a = "0";
            else if (Constante1 == 0) a = "-" + Coefficient1.ToString() + Variable1;
            else if (Coefficient1 == 0) a = Constante1.ToString();
            else if (Coefficient1 == -1) a = Constante1.ToString() + "+" + Variable1.ToString();
            else if (Coefficient1 == 1) a = Constante1.ToString() + "-" + Variable1.ToString();
            a = StringSimpler.Simplifier(a);

            return a;
        }
    }
}

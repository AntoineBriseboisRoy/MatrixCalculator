using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class InfiniteRep2:InfiniteRep
    {
        float Coefficient2 { get; set; }
        char Variable2 { get; set; }
        public InfiniteRep2(float a, float b, char v, float c, char z)
            :base(a,b,v)
        {
            Coefficient2 = c;
            Variable2 = z;
        }
        public static InfiniteRep2 operator *(InfiniteRep2 a, float b)
        {
            return new InfiniteRep2(a.Constante1*b,a.Coefficient1*b,a.Variable1,a.Coefficient2*b,a.Variable2);
        }
        public static InfiniteRep2 operator *(float b, InfiniteRep2 a)
        {
            return new InfiniteRep2(a.Constante1 * b, a.Coefficient1 * b, a.Variable1, a.Coefficient2 * b, a.Variable2);
        }
        public static InfiniteRep2 operator /(InfiniteRep2 a, float b)
        {
            return new InfiniteRep2(a.Constante1 / b, a.Coefficient1 / b, a.Variable1, a.Coefficient2 / b, a.Variable2);
        }
        public static InfiniteRep2 operator /(float b, InfiniteRep2 a)
        {
            return new InfiniteRep2(a.Constante1 / b, a.Coefficient1 / b, a.Variable1, a.Coefficient2 / b, a.Variable2);
        }
        public static InfiniteRep2 operator +(InfiniteRep2 a, InfiniteRep2 b)
        {
            return new InfiniteRep2(a.Constante1 + b.Constante1, a.Coefficient1 + b.Coefficient1, a.Variable1, a.Coefficient2 + b.Coefficient2,a.Variable2);
        }
        public static InfiniteRep2 operator -(InfiniteRep2 a, InfiniteRep2 b)
        {
            return new InfiniteRep2(a.Constante1 - b.Constante1, a.Coefficient1 - b.Coefficient1, a.Variable1, a.Coefficient2 - b.Coefficient2, a.Variable2);
        }
        public override string ToString()
        {
            string a = Constante1.ToString() + "-" + Coefficient1.ToString() + Variable1+ "-"+ Coefficient2.ToString()+Variable2;
            if (Constante1 == 0 && Coefficient1 == 0 && Coefficient2 == 0) a = "0";
            else if(Constante1 == 0 && Coefficient1 == 0 && Coefficient2 == 1) a = "-" + Variable2;
            else if(Constante1 == 0 && Coefficient1 == 0 && Coefficient2 == -1) a = Variable2.ToString();
            else if(Constante1 == 0 && Coefficient1 == 1 && Coefficient2 == 0) a = "-" + Variable1;
            else if(Constante1 == 0 && Coefficient1 == -1 && Coefficient2 == 0) a =  Variable1.ToString();
            else if(Constante1 == 0 && Coefficient1 == 1 && Coefficient2 == 1) a = "-" + Variable1 + "-" + Variable2;
            else if(Constante1 == 0 && Coefficient1 == -1 && Coefficient2 == -1) a = Variable1 + "+"+Variable2;
            else if(Constante1 == 0 && Coefficient1 == 1 && Coefficient2 == -1) a = "-"+Variable1 + "+"+Variable2;
            else if(Constante1 == 0 && Coefficient1 == -1 && Coefficient2 == 1) a = Variable1 + "-"+Variable2;
            else if(Coefficient1 == 0 && Coefficient2 == 1) a = Constante1.ToString() + "-" + Variable2;
            else if( Coefficient1 == 0 && Coefficient2 == -1) a = Constante1.ToString() + "+"+Variable2.ToString();
            else if(Coefficient1 == 1 && Coefficient2 == 0) a = Constante1.ToString() +"-" + Variable1;
            else if(Coefficient1 == -1 && Coefficient2 == 0) a = Constante1.ToString() +"+"+ Variable1.ToString();
            else if(Coefficient1 == 1 && Coefficient2 == 1) a = Constante1.ToString() +"-" + Variable1 + "-" + Variable2;
            else if(Coefficient1 == -1 && Coefficient2 == -1) a = Constante1.ToString() +"+"+ Variable1 + "+"+Variable2;
            else if(Coefficient1 == 1 && Coefficient2 == -1) a = Constante1.ToString() +"-"+Variable1 + "+"+Variable2;
            else if(Coefficient1 == -1 && Coefficient2 == 1) a = Constante1.ToString() +"+"+ Variable1 + "-"+Variable2;
            else if (Constante1 == 0) a = "-" + Coefficient1.ToString() + Variable1 + "-" + Coefficient2.ToString() + Variable2;
            else if (Coefficient1 == 0) a = a = Constante1.ToString() + "-"  + Coefficient2.ToString() + Variable2;
            else if (Coefficient1 == -1) a = Constante1.ToString() + "+"+Variable1 + "-" + Coefficient2.ToString() + Variable2;
            else if (Coefficient1 == 1) a = Constante1.ToString() + "-" + Variable1 + "-" + Coefficient2.ToString() + Variable2;
            else if (Coefficient2 == 0) a = a = Constante1.ToString() + "-" + Coefficient1.ToString() + Variable1;
            else if (Coefficient2 == -1) a = Constante1.ToString() + "-" + Coefficient1.ToString() + Variable1 + "+" + Variable2;
            else if (Coefficient2 == 1) a = Constante1.ToString() + "-" + Coefficient1.ToString() + Variable1 + "-" + Variable2;
            a = StringSimpler.Simplifier(a);
            return a;
        }
    }
}

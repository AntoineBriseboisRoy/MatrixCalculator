using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class StringSimpler
    {
        public static string Simplifier(string a)
        {
            a = a.Replace("--", " + ");
            a = a.Replace("-+", " - ");
            a = a.Replace("+-", " - ");
            a = a.Replace("++", " + ");
            a = a.Replace("-", " - ");
            a = a.Replace("+", " + ");
            return a;
        }
    }
}

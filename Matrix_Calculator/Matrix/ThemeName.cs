using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
   public class ThemeName : IComparable
   {
      public string name { get; private set; }
      public int numero { get; private set; }
      public string color { get; private set; }

      public ThemeName(string lecture)
      {
         char[] spliter = new char[] { ';' };
         string[] données = lecture.Split(spliter);
         numero = int.Parse(données[0]);
         name = données[1];
         color = données[2];
      }

      public int CompareTo(object o)
      {
         ThemeName tn = o as ThemeName;
         return this.name.CompareTo(tn.name);
      }
   }
}

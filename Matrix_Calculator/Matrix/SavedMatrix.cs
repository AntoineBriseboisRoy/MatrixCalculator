using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
   public class SavedMatrix:IComparable
   {
      public string Nom { get; private set; }
      public string[,] tableau;

      public SavedMatrix(string nom, string[,] tabl)
      {
         Nom = nom;
         tableau = tabl;
      }
      
      public int CompareTo(object o)
      {
         int i = 0;
         if (o is SavedMatrix)
         {
            SavedMatrix saved = o as SavedMatrix;
            i = this.Nom.CompareTo(saved.Nom);
         }
         else
         {
            throw new InvalidOperationException();
         }
         return i;
      }

      public string ToSavableString()
      {
         string s = "";
         s += tableau.GetLength(0) + "\r\n";
         s += tableau.GetLength(1);
         foreach (string st in tableau)
         {
            s += "\r\n" + st;
         }
         return s;
      }


   }
}

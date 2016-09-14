using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Matrix
{
   public class SaveManager
   {
      public List<SavedMatrix> matrices;
      public SaveManager()
      {
         matrices = new List<SavedMatrix>();
      }

      public void Add(SavedMatrix matrix)
      {
         matrices.Add(matrix);
         matrices.Sort();
      }
      public void Clear()
      {
         matrices.Clear();
      }

   }
}

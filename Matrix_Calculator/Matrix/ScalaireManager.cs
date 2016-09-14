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
   class ScalaireManager : TextBoxesManager
   {
      Label Label;

      public ScalaireManager(int x, int y, Cote cote,Form form)
         : base(x, y,1, 1, cote, form)
      {
         Label = CreateLabel(PositionX + 5, PositionY + 7, "Scalaire :");
      }

      public ScalaireManager(float f, int x, int y, Cote cote, Form form)
         : base(x, y, 1,1, cote, form)
      {
         Label = CreateLabel(PositionX + 5, PositionY + 7, "Scalaire :");
         TextBoxes[0, 0].Text = f.ToString();
      }
      public ScalaireManager(string s, int x, int y, Cote cote, Form form)
         : base(x, y,1, 1, cote, form)
      {
         Label = CreateLabel(PositionX + 5, PositionY + 7, "Scalaire :");
         TextBoxes[0, 0].Text = s;
         (Form as Form1).VerifierBouton();
      }

      public override void Empty()
      {
         base.Empty();
         Label.Dispose();
      }

      public float ToFloat()
      {
         return float.Parse(TextBoxes[0, 0].Text);
      }
   }
}

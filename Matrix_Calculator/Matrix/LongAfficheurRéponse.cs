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
   class LongAfficheurRéponse : AfficheurRéponse
   {

      public LongAfficheurRéponse(string es, int x, int y, Form form)
         :base(es, x, y, form)
      {

      }
      protected override Label CréerLabel(string s, int x, int y)
      {
         Label label1 = new Label();
         label1.AutoSize = true;
         label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         label1.Location = new System.Drawing.Point((int)((float)Form.Width/2 - 300), y);
         label1.Name = "label1";
         label1.MinimumSize = new System.Drawing.Size(600, 20);
         label1.Size = new System.Drawing.Size(600, 20);
         label1.TabIndex = 3;
         label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         label1.Text = s;
         Form.Controls.Add(label1);

         return label1;
      }
   }
}

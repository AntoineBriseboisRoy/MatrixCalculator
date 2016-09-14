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
   class SaveButton : ImagedButton
   {
      TextBoxesManager tbm;

      public SaveButton(int x, int y, Bitmap image, TextBoxesManager tbm, Form form)
         : base(x, y, image, form)
      {
         this.tbm = tbm;
      }

      protected override void Clic(object sender, EventArgs e)
      {
         SaveForm saveForm = new SaveForm();
         saveForm.LierLesForms(Form as Form1, tbm.ToStringArray());
         saveForm.ShowDialog();
      }
   }
}

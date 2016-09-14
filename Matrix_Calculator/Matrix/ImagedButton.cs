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
   abstract class ImagedButton
   {
      protected int PositionX
      {
         get;
         set;
      }
      protected int PositionY
      {
         get;
         set;
      }
      protected Form Form;
      protected Bitmap Image 
      {
         get;
         set;
      }
      protected Button bouton;

      public ImagedButton(int x, int y, Bitmap image, Form form)
      {
         PositionX = x;
         PositionY = y;
         Image = image;
         Form = form;
         bouton = CreerButton(PositionX, PositionY, Image, Form);

      }

      Button CreerButton(int x, int y, Bitmap image, Form form)
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
         Button button1 = new Button();
         button1.BackgroundImage = image;
         button1.Location = new System.Drawing.Point(x, y);
         button1.Name = "btn_imaged_" + x + "_" + y;
         button1.Size = new System.Drawing.Size(24, 24);
         button1.TabIndex = 4;
         button1.UseVisualStyleBackColor = true;
         button1.Click += new System.EventHandler(Clic);
         button1.BringToFront();
         form.Controls.Add(button1);
         return button1;
      }

      protected abstract void Clic(object sender, EventArgs e);

      public virtual void Empty()
      {
         bouton.Dispose();
      }
   }
}

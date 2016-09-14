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
   public abstract class TextBoxesManager
   {
      public TextBox[,] TextBoxes;
      TransferButton BoutonTransfer;
      NullButton BoutonNull;
      IdentityButton BoutonIdentity;
      RandomButton BoutonRandom;
      LoadButton BoutonLoad;
      SaveButton BoutonSave;
      public int MaxLignes
      {
         get;
         protected set;
      }
      public int MinLignes
      {
         get;
         protected set;
      }
      public int PositionX
      {
         get;
         protected set;
      }
      public int PositionY
      {
         get;
         protected set;
      }
      protected Form Form;
      public Cote Cote;

      public TextBoxesManager(int x, int y, int minLigne, int maxLignes, Cote cote ,Form form)
      {
         Form = form;
         PositionX = x;
         PositionY = y;
         this.MaxLignes = maxLignes;
         this.MinLignes = minLigne;
         this.Cote = cote;
         TextBoxes = new TextBox[0, 0];
         if (this.GetType() != Type.GetType("Matrix.TextBoxesManagerColonne"))
         {
             ChangeTextBoxArray(minLigne, minLigne);
         }


         if ((Form as Form1).RéponseExiste())
         {
            CreerTransferButton();
         }
         BoutonNull = new NullButton(PositionX + 5, PositionY + 240, global::Matrix.Properties.Resources.Nulle, this, Form);
         BoutonIdentity = new IdentityButton(PositionX + 35, PositionY + 240,global::Matrix.Properties.Resources.Identitée, this, Form);
         BoutonRandom = new RandomButton(PositionX + 65, PositionY + 240, global::Matrix.Properties.Resources.Random, this, Form);
        
         BoutonSave = new SaveButton(PositionX + 95, PositionY + 240, global::Matrix.Properties.Resources.Save, this, Form);
         BoutonLoad = new LoadButton(PositionX + 125, PositionY + 240, global::Matrix.Properties.Resources.Load, this, Form);
      }

      public void ChangeTextBoxArray(decimal x, decimal y)
      {
         TextBox[,] temps = new TextBox[(int)x, (int)y];

         bool formatPlusGrand = (temps.GetLength(0) > TextBoxes.GetLength(0) || temps.GetLength(1) > TextBoxes.GetLength(1));
         bool formatPlusPetit = temps.GetLength(0) < TextBoxes.GetLength(0) || temps.GetLength(1) < TextBoxes.GetLength(1);

         for (int i = 0; i < Math.Max(temps.GetLength(0), TextBoxes.GetLength(0)); ++i)
         {
            for (int j = 0; j < Math.Max(temps.GetLength(1), TextBoxes.GetLength(1)); ++j)
            {
               //doit créé
               if (formatPlusGrand && (i >= TextBoxes.GetLength(0) || j >= TextBoxes.GetLength(1)))
               {
                   temps[i, j] = CréerUneTextBox(PositionX + 5 + j * 40, PositionY + 30 + i * 30, i+10*j+ConvertirCote(Cote));
               }
               //doit delete
               else if (formatPlusPetit && (i >= temps.GetLength(0) || j >= temps.GetLength(1)))
               {
                  TextBoxes[i, j].Dispose();
               }
               //doit ne rien faire
               else
               {
                  temps[i, j] = TextBoxes[i, j];
               }
            }
         }
         TextBoxes = temps;
      }



      private TextBox CréerUneTextBox(int x, int y, int startingtabindex)
      {
         TextBox tb = new TextBox();
         tb.Location = new System.Drawing.Point(x, y);
         tb.Name = "TextBox_" + x + "_" + y;
         tb.Size = new System.Drawing.Size(30, 20);
         tb.TabIndex = startingtabindex;
         tb.TextChanged += new System.EventHandler(textBox_TextChanged);
         tb.Text = "0";
         Form.Controls.Add(tb);
         return tb;

      }

      private void textBox_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = (sender as TextBox);
          float f;
          if (float.TryParse(tb.Text, out f))
          {
              tb.ForeColor = System.Drawing.Color.Black;
          }
          else
          {
              tb.ForeColor = System.Drawing.Color.Red;
          }
          if (tb.Text == "")
          {
             (Form as Form1).DisablerBouton();
          }
          else
          {
             (Form as Form1).VerifierBouton();
          }
          
      }

      public bool IsOk
      {
          get
          {
              bool asd = true;
              foreach (TextBox t in TextBoxes)
              {
                  asd = asd && t.ForeColor == System.Drawing.Color.Black;
              }
              return asd;
          }
      }
           

      protected NumericUpDown CreateNumericUpDown(int x, int y, EventHandler methode)
      {
         NumericUpDown numericUpDown1 = new System.Windows.Forms.NumericUpDown();
         numericUpDown1.Location = new System.Drawing.Point(x, y);
         numericUpDown1.Maximum = new decimal(new int[] { MaxLignes, 0, 0, 0 });
         numericUpDown1.Minimum = new decimal(new int[] { MinLignes, 0, 0, 0 });
         numericUpDown1.Name = "numericUpDown_" + x + "_" + y;
         numericUpDown1.Size = new System.Drawing.Size(30, 20);
         numericUpDown1.TabIndex = 1;
         numericUpDown1.Value = new decimal(new int[] { MinLignes, 0, 0, 0 });
         numericUpDown1.ValueChanged += methode;
         Form.Controls.Add(numericUpDown1);
         return numericUpDown1;
      }

      public Matrix ToMatrix()
      {
         float[,] tabl = new float[TextBoxes.GetLength(0), TextBoxes.GetLength(1)];
            for (int i = 0; i < TextBoxes.GetLength(0); ++i)
            {
               for (int j = 0; j < TextBoxes.GetLength(1); ++j)
               {
                  tabl[i, j] = float.Parse(TextBoxes[i, j].Text);
               }
            }
         return new Matrix(tabl);
      }

      protected Label CreateLabel(int x, int y, string texte)
      {
         Label label1 = new Label();
         label1.AutoSize = true;
         label1.Location = new System.Drawing.Point(x, y);
         label1.Name = "label_" + x + "_" + y;
         label1.Size = new System.Drawing.Size(335, 313);
         label1.TabIndex = 1;
         label1.Text = texte;
         Form.Controls.Add(label1);
         return label1;
      }

      public virtual void Empty()
      {
         foreach (TextBox t in TextBoxes)
         {
            t.Dispose();
         }
         if (BoutonTransfer != null)
         {
            BoutonTransfer.Empty();
         }
         BoutonNull.Empty();
         BoutonIdentity.Empty();
         BoutonRandom.Empty();
         BoutonSave.Empty();
         BoutonLoad.Empty();
      }

      public void CreerTransferButton()
      {
         if (BoutonTransfer == null)
         {
            int x = 0;
            Bitmap image = image = global::Matrix.Properties.Resources.TransferGauche;

            if (Cote == Cote.GAUCHE)
            {
               x = 190;
               image = global::Matrix.Properties.Resources.TransferGauche;
            }
            else
            {
               x = 600;
               image = global::Matrix.Properties.Resources.TransferDroit;
            }

            BoutonTransfer = new TransferButton(x, 320, image, this, Form);
         }

      }

      public string ToSavableString()
      {
         string s = "";
         s += TextBoxes.GetLength(0) + "\r\n";
         s += TextBoxes.GetLength(1);
         foreach (TextBox tb in TextBoxes)
         {
            s += "\r\n" + tb.Text;
         }
         return s;
      }

      public string[,] ToStringArray()
      {
         string[,] tabl = new string[TextBoxes.GetLength(0), TextBoxes.GetLength(1)];
         for (int i = 0; i < TextBoxes.GetLength(0); ++i)
         {
            for (int j = 0; j < TextBoxes.GetLength(1); ++j)
            {
               tabl[i, j] = (TextBoxes[i, j].Text);
            }
         }
         return tabl;
      }

      public bool TransferButtonExists
      {
         get
         {
            return BoutonTransfer != null;
         }
      }

      public void DeleteBoutonTransfereEtSystem32()
      {
         if (TransferButtonExists)
         {
            BoutonTransfer.Empty();
            BoutonTransfer = null;
         }
      }

      int ConvertirCote(Cote cote)
      {
          return cote != Cote.DROITE ? 200 : 300; 
      }
   }
}

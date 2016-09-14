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
   class TextBoxesManagerRectangle : TextBoxesManager
   {
      NumericUpDown Lignes;
      NumericUpDown Colonnes;
      Label LabelLignes;
      Label LabelColonnes;

      public TextBoxesManagerRectangle(int x, int y,int minLignes, int maxLignes, Cote cote, Form form)
         : base(x, y, minLignes, maxLignes, cote, form)
      {
          Lignes = CreateNumericUpDown(PositionX + 50, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
          Colonnes = CreateNumericUpDown(PositionX + 190, PositionY + 5, new System.EventHandler(this.numericUpDown2_ValueChanged));
         
         LabelLignes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes :");
         LabelColonnes = CreateLabel(PositionX + 130, PositionY + 7, "Colonnes :");
      }

      public TextBoxesManagerRectangle(Matrix matrix, int x, int y, int minLignes, int maxLignes, Cote cote, Form form)
         : base(x, y, minLignes, maxLignes, cote, form)
      {
          Lignes = CreateNumericUpDown(PositionX + 50, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
          Colonnes = CreateNumericUpDown(PositionX + 190, PositionY + 5, new System.EventHandler(this.numericUpDown2_ValueChanged));
          LabelLignes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes :");
         LabelColonnes = CreateLabel(PositionX + 130, PositionY + 7, "Colonnes :");
         int m = matrix.M;
         int n = matrix.N;

         if (m > MaxLignes)
         {
            m = MaxLignes;
         }
         if (m < MinLignes)
         {
            m = MinLignes;
         }
         if (n > MaxLignes)
         {
            n = MaxLignes;
         }
         if (n < MinLignes)
         {
            n = MinLignes;
         }
         
         Lignes.Value = m;
         Colonnes.Value = n;

         ChangeTextBoxArray(m, n);

         for (int i = 0; i < Math.Min(matrix.M, m); ++i)
         {
            for (int j = 0; j < Math.Min(matrix.N, n); ++j)
            {
               TextBoxes[i, j].Text = matrix[i, j].ToString() ;
            }
         }
      }

      public TextBoxesManagerRectangle(string[,] matrix, int x, int y, int minLignes, int maxLignes, Cote cote, Form form)
         : base(x, y, minLignes, maxLignes, cote, form)
      {
         Lignes = CreateNumericUpDown(PositionX + 50, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
         Colonnes = CreateNumericUpDown(PositionX + 190, PositionY + 5, new System.EventHandler(this.numericUpDown2_ValueChanged));
         LabelLignes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes :");
         LabelColonnes = CreateLabel(PositionX + 130, PositionY + 7, "Colonnes :");



         int m = matrix.GetLength(0);
         int n = matrix.GetLength(1);

         if (m > MaxLignes)
         {
            m = MaxLignes;
         }
         if (m < MinLignes)
         {
            m = MinLignes;
         }
         if (n > MaxLignes)
         {
            n = MaxLignes;
         }
         if (n < MinLignes)
         {
            n = MinLignes;
         }

         Lignes.Value = m;
         Colonnes.Value = n;

         ChangeTextBoxArray(m, n);

         for (int i = 0; i < Math.Min(matrix.GetLength(0),m); ++i)
         {
            for (int j = 0; j < Math.Min(matrix.GetLength(1), n); ++j)
            {
               TextBoxes[i, j].Text = matrix[i, j].ToString();
            }
         }

         (Form as Form1).VerifierBouton();
      }

      private void numericUpDown1_ValueChanged(object sender, EventArgs e)
      {
         this.ChangeTextBoxArray((sender as NumericUpDown).Value, TextBoxes.GetLength(1));
         (Form as Form1).VerifierBouton();
      }

      private void numericUpDown2_ValueChanged(object sender, EventArgs e)
      {
         this.ChangeTextBoxArray(this.TextBoxes.GetLength(0), (sender as NumericUpDown).Value);
         (Form as Form1).VerifierBouton();
      }

      public override void Empty()
      {
         base.Empty();
         Lignes.Dispose();
         Colonnes.Dispose();
         LabelLignes.Dispose();
         LabelColonnes.Dispose();
      }


   }
}

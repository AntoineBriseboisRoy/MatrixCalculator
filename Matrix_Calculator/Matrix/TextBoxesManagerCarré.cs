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
   class TextBoxesManagerCarré : TextBoxesManager
   {
      NumericUpDown LignesColonnes;
      Label LabelLignesColonnes;

      public TextBoxesManagerCarré(int x, int y, int minLinges, int maxLignes, Cote cote, Form form)
         : base(x, y, minLinges, maxLignes, cote, form)
      {
         base.ChangeTextBoxArray(minLinges, minLinges);
         LignesColonnes = CreateNumericUpDown(PositionX + 100, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
         LabelLignesColonnes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes/Colonnes :");
      }

      public TextBoxesManagerCarré(Matrix matrix, int x, int y, int minLignes, int maxLignes, Cote cote, Form form)
         : base(x, y,minLignes, maxLignes, cote, form)
      {

         LignesColonnes = CreateNumericUpDown(PositionX + 100, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
         LabelLignesColonnes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes/Colonnes :");



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
         int max = Math.Max(m,n);
         LignesColonnes.Value = max;
         ChangeTextBoxArray(max, max);
         for (int i = 0; i < Math.Min(matrix.M, m); ++i)
         {
            for (int j = 0; j < Math.Min( matrix.N,n); ++j)
            {
               TextBoxes[i, j].Text = matrix[i, j].ToString() ;
            }
         }

      }

      public TextBoxesManagerCarré(string[,] matrix, int x, int y, int minlignes, int maxLignes, Cote cote, Form form)
         : base(x, y, minlignes, maxLignes, cote, form)
      {
         LignesColonnes = CreateNumericUpDown(PositionX + 100, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
         LabelLignesColonnes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes/Colonnes :");



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
         int max = Math.Max(m, n);
         LignesColonnes.Value = max;
         ChangeTextBoxArray(max, max);

         for (int i = 0; i < Math.Min(matrix.GetLength(0),m); ++i)
         {
            for (int j = 0; j < Math.Min(matrix.GetLength(1),n); ++j)
            {
               TextBoxes[i, j].Text = matrix[i, j].ToString();
            }
         }

         (Form as Form1).VerifierBouton();
      }

      private void numericUpDown1_ValueChanged(object sender, EventArgs e)
      {
         this.ChangeTextBoxArray(TextBoxes.GetLength(0), (sender as NumericUpDown).Value);
         this.ChangeTextBoxArray((sender as NumericUpDown).Value, TextBoxes.GetLength(1));
         (Form as Form1).VerifierBouton();
      }

      public override void Empty()
      {
         base.Empty();
         LignesColonnes.Dispose();
         LabelLignesColonnes.Dispose();
      }


   }
}

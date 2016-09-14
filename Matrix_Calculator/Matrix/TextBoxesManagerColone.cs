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
    class TextBoxesManagerColonne : TextBoxesManager
    {
        NumericUpDown LignesColonnes;
        Label LabelLignesColonnes;

        public TextBoxesManagerColonne(int x, int y, int minLinges, int maxLignes, Cote cote, Form form)
            : base(x, y, minLinges, maxLignes, cote, form)
        {
            base.ChangeTextBoxArray(minLinges, 1);
            LignesColonnes = CreateNumericUpDown(PositionX + 50, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
            LabelLignesColonnes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes :");
        }

        public TextBoxesManagerColonne(Matrix matrix, int x, int y, int minLignes, int maxLignes, Cote cote, Form form)
            : base(x, y, minLignes, maxLignes, cote, form)
        {
            LignesColonnes = CreateNumericUpDown(PositionX + 50, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
            LabelLignesColonnes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes :");

            int m = matrix.M;

            if (m > MaxLignes)
            {
               m = MaxLignes;
            }
            if (m < MinLignes)
            {
               m = MinLignes;
            }

            LignesColonnes.Value = m;
            ChangeTextBoxArray(m, 1);

            for (int i = 0; i < Math.Min(matrix.M, m); ++i)
            {
                    TextBoxes[i, 0].Text = matrix[i, 0].ToString();
            }

        }

        public TextBoxesManagerColonne(string[,] matrix, int x, int y, int minlignes, int maxLignes, Cote cote, Form form)
            : base(x, y, minlignes, maxLignes, cote, form)
        {
            LignesColonnes = CreateNumericUpDown(PositionX + 50, PositionY + 5, new System.EventHandler(this.numericUpDown1_ValueChanged));
            LabelLignesColonnes = CreateLabel(PositionX + 5, PositionY + 7, "Lignes :");

            int m = matrix.GetLength(0);

            if (m > MaxLignes)
            {
               m = MaxLignes;
            }
            if (m < MinLignes)
            {
               m = MinLignes;
            }

            LignesColonnes.Value = m;
            ChangeTextBoxArray(m, 1);


            for (int i = 0; i < Math.Min(matrix.GetLength(0),m); ++i)
            {
                    TextBoxes[i, 0].Text = matrix[i, 0].ToString();
            }

            (Form as Form1).VerifierBouton();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.ChangeTextBoxArray((sender as NumericUpDown).Value, 1);
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

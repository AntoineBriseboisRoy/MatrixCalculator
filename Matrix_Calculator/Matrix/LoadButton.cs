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
   class LoadButton : ImagedButton
   {
      TextBoxesManager tbm;
      Cote Cote;

      public LoadButton(int x, int y, Bitmap image, TextBoxesManager tbm, Form form)
         : base(x, y, image, form)
      {
         this.tbm = tbm;
         Cote = tbm.Cote;
      }

      protected override void Clic(object sender, EventArgs e)
      {
         LoadingForm loadingForm = new LoadingForm();
         loadingForm.LierLesForms(Form as Form1);
         loadingForm.ShowDialog();
         if (tbm != null && loadingForm.DoitLoader)
         {
            string[,] matrix = loadingForm.LastSelectedMatrix.tableau;
            if (tbm.GetType() == (typeof(TextBoxesManagerCarré)))
            {
               TextBoxesManagerCarré temps = new TextBoxesManagerCarré(matrix, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(TextBoxesManagerRectangle)))
            {
               TextBoxesManagerRectangle temps = new TextBoxesManagerRectangle(matrix, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(ScalaireManager)))
            {
               ScalaireManager temps = new ScalaireManager(matrix[0,0], tbm.PositionX, tbm.PositionY, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(TextBoxesManagerColonne)))
            {
               TextBoxesManagerColonne temps = new TextBoxesManagerColonne(matrix, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (Cote == Cote.DROITE)
            {
               (Form as Form1).Droite = tbm;
            }
            if (Cote == Cote.GAUCHE)
            {
               (Form as Form1).Gauche = tbm;
            }
            (Form as Form1).VerifierBouton();
         }
      }
   }
}

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
   class TransferButton : ImagedButton
   {
      TextBoxesManager tbm;
      Cote Cote;
      public TransferButton(int x, int y, Bitmap image, TextBoxesManager tbm, Form form) 
         : base(x,y,image,form)
      {
         this.tbm = tbm;
         Cote = tbm.Cote;
      }

      protected override void Clic(object sender, EventArgs e)
      {
         Matrix réponse = (Form as Form1).MatrixRéponse();
         if (tbm != null)
         {
            if (tbm.GetType() == (typeof(TextBoxesManagerCarré)))
            {
               TextBoxesManagerCarré temps = new TextBoxesManagerCarré(réponse, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(TextBoxesManagerRectangle)))
            {
               TextBoxesManagerRectangle temps = new TextBoxesManagerRectangle(réponse, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(ScalaireManager)))
            {
               ScalaireManager temps = new ScalaireManager(réponse.Determinant, tbm.PositionX, tbm.PositionY, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(TextBoxesManagerColonne)))
            {
               TextBoxesManagerColonne temps = new TextBoxesManagerColonne(réponse, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
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

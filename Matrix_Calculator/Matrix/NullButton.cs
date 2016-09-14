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
   class NullButton : ImagedButton
   {
      TextBoxesManager tbm;
      Cote Cote;
      public NullButton(int x, int y, Bitmap image, TextBoxesManager tbm, Form form) 
         : base(x,y,image,form)
      {
         this.tbm = tbm;
         Cote = tbm.Cote;
      }

      protected override void Clic(object sender, EventArgs e)
      {
         if (tbm != null)
         {
            Matrix nulle = Matrix.Null(tbm.TextBoxes.GetLength(0), tbm.TextBoxes.GetLength(1));
            if (tbm.GetType() == (typeof(TextBoxesManagerCarré)))
            {
               TextBoxesManagerCarré temps = new TextBoxesManagerCarré(nulle, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(TextBoxesManagerRectangle)))
            {
               TextBoxesManagerRectangle temps = new TextBoxesManagerRectangle(nulle, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(ScalaireManager)))
            {
               ScalaireManager temps = new ScalaireManager(0, tbm.PositionX, tbm.PositionY, tbm.Cote, Form);
               tbm.Empty();
               tbm = temps;
            }
            if (tbm.GetType() == (typeof(TextBoxesManagerColonne)))
            {
               TextBoxesManagerColonne temps = new TextBoxesManagerColonne(nulle, tbm.PositionX, tbm.PositionY, tbm.MinLignes, tbm.MaxLignes, tbm.Cote, Form);
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

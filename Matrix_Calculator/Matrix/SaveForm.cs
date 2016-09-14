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
   public partial class SaveForm : Form
   {
      string Texte { get; set; }
      Form1 form1;
      string[,] tabl;

      public SaveForm()
      {
         InitializeComponent();
      }

      private void btn_save_Click(object sender, EventArgs e)
      {
         form1.saveManager.Add(new SavedMatrix(Texte, tabl));
         this.Close();
      }

      private void tb_save_TextChanged(object sender, EventArgs e)
      {
         Texte = (sender as TextBox).Text;
         VérifierBouton();
      }

      public void LierLesForms(Form1 form1, string[,] matrice)
      {
         this.form1 = form1;
         tabl = matrice;
      }

      private void SaveForm_Load(object sender, EventArgs e)
      {
         Texte = "";
         VérifierBouton();
      }

      void VérifierBouton()
      {
         if (Texte == "")
         {
            btn_save.Enabled = false;
         }
         else
         {
            btn_save.Enabled = true;
         }
      }
   }
}

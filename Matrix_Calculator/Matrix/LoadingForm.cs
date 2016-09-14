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
   public partial class LoadingForm : Form
   {
      Form1 form1;
      AfficheurRéponse Ar;
      public SavedMatrix LastSelectedMatrix { get; private set; }
      public bool DoitLoader { get; private set; }

      public LoadingForm()
      {
         InitializeComponent();
      }

      private void Sauvegarde_Load(object sender, EventArgs e)
      {
         listBox1.DataSource = form1.saveManager.matrices;
         listBox1.DisplayMember = "Nom";
         if (form1.saveManager.matrices.Count == 0)
         {
            btn_loader.Enabled = false;
         }
      }

      public void LierLesForms(Form1 form1)
      {
         this.form1 = form1;
      }

      private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (Ar != null)
         {
            Ar.Empty();
            Ar = null;
         }

         ListBox lb = (sender as ListBox);
         SavedMatrix matrix = (lb.DataSource as List<SavedMatrix>)[lb.SelectedIndex];
         int x = ((this.Width - 139)/2) - (int)(matrix.tableau.GetLength(1)/2f*50) + 139 - 10;
         int y = (this.Height/2) - (int)(matrix.tableau.GetLength(0) / 2f * 20) - 20;
         Ar = new AfficheurRéponse(matrix.tableau, x, y, this);
         LastSelectedMatrix = matrix;
      }

      private void btn_loader_Click(object sender, EventArgs e)
      {
         DoitLoader = true;
         this.Close();
      }
   }
}

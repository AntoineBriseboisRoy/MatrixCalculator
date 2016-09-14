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
   class AfficheurRéponse
   {
      Label[,] labels;
      protected Form Form;
      int PositionX
      {
         get;
         set;
      }
      int PositionY
      {
         get;
         set;
      }

      public AfficheurRéponse(Matrix A, int x, int y, Form form)
      {
         PositionX = x;
         PositionY = y;
         Form = form;
         labels = CréerTableauLabels(A);
      }

      public AfficheurRéponse(string[,] tableau, int x, int y, Form form)
      {
         PositionX = x;
         PositionY = y;
         Form = form;
         labels = CréerTableauLabels(tableau);
      }

      public AfficheurRéponse(string es, int x, int y, Form form)
      {
         PositionX = x;
         PositionY = y;
         Form = form;

         string[,] newTableau = new string[1, 1];
            newTableau[0, 0] = es;
         

         labels = CréerTableauLabels(newTableau);
      }

      Label[,] CréerTableauLabels(Matrix A)
      {
         Label[,] tabl = new Label[A.M, A.N];
         for (int i = 0; i < tabl.GetLength(0); ++i)
         {
            for (int j = 0; j < tabl.GetLength(1); ++j)
            {
               tabl[i, j] = CréerLabel(A[i, j].ToString(), PositionX + j*50, PositionY + i * 20);
            }
         }
            return tabl;
      }

      Label[,] CréerTableauLabels(string[,] tableau)
      {
         Label[,] tabl = new Label[tableau.GetLength(0), tableau.GetLength(1)];
         for (int i = 0; i < tabl.GetLength(0); ++i)
         {
            for (int j = 0; j < tabl.GetLength(1); ++j)
            {
               tabl[i, j] = CréerLabel(tableau[i, j], PositionX + j * 50, PositionY + i * 20);
            }
         }
         return tabl;
      }

      protected virtual Label CréerLabel(string s, int x, int y)
      {
         Label label1 = new Label();
         label1.AutoSize = true;
         label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         label1.Location = new System.Drawing.Point(x, y);
         label1.Name = "label1";
         label1.MinimumSize = new System.Drawing.Size(50, 20);
         label1.Size = new System.Drawing.Size(50, 20);
         label1.TabIndex = 3;
         label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         label1.Text = s;
         Form.Controls.Add(label1);
         
         return label1;
      }

      public void Empty()
      {
         foreach (Label l in labels)
         {
            l.Dispose();
         }
      }

      public Matrix ToMatrix()
      {
         float[,] tbl = new float[labels.GetLength(0), labels.GetLength(1)];
         int nombre = 0;
         foreach (Label l in labels)
         {
            ++nombre;
         }
         for (int i = 0; i < tbl.GetLength(0); ++i)
         {
            for (int j = 0; j < tbl.GetLength(1); ++j)
            {
               tbl[i, j] = float.Parse(labels[i, j].Text);
            }
         }
         return new Matrix(tbl);
      }

      public string ToSavableString()
      {
         string s = "";
         s += labels.GetLength(0) + "\r\n";
         s += labels.GetLength(1);
         foreach (Label l in labels)
         {
            s += "\r\n" + l.Text;
         }
         return s;
      }
   }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Matrix
{
   public class Theme
   {
      public Color couleurDeFond { get; set; }
      PictureBox ImageGauche { get; set; }
      PictureBox ImageDroite { get; set; }
      Form1 form { get; set; }
      string Nom { get; set; } 
      int numero { get; set; }
      public List<ThemeName> themes { get; set; }
      const int DEFAULT_THEME = 4;
      int starting;
      public Theme(int numero, Form1 form)
      {
         this.form = form;
         CréerLesthemes();
        
         try
         {
             StreamReader reader = new StreamReader("../../Images/Themes/start.txt");
             int chose = int.Parse(reader.ReadLine());
             ImageGauche = CréerPictureBox(chose, Cote.GAUCHE);
             ImageDroite = CréerPictureBox(chose, Cote.DROITE);
             couleurDeFond = Color.FromName(themes.Find(x => x.numero == chose).color);
             starting = chose;
             reader.Close();
         }
         catch (Exception)
         {
             ImageGauche = CréerPictureBox(DEFAULT_THEME, Cote.GAUCHE);
             ImageDroite = CréerPictureBox(DEFAULT_THEME, Cote.DROITE);
             couleurDeFond = Color.FromName(themes.Find(x => x.numero == DEFAULT_THEME).color);
             starting = DEFAULT_THEME;

         }
      }

      PictureBox CréerPictureBox(int numero, Cote cote)
      {
         PictureBox pictureBox = new PictureBox();
         pictureBox.Image = ChoisirImage(numero, cote);
         
         pictureBox.Name = "pictureBox_" + numero.ToString();

         pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
         if (cote == Cote.GAUCHE)
         {
            pictureBox.Location = new System.Drawing.Point(0, 600 - pictureBox.Size.Height);
         }
         else
         {
            pictureBox.Location = new System.Drawing.Point(800 - pictureBox.Size.Width, 600 - pictureBox.Size.Height);
         }
         
        // pictureBox.Size = new System.Drawing.Size(175, 173);
         //pictureBox.TabIndex = 3;
         pictureBox.TabStop = false;
            pictureBox.SendToBack();
         form.Controls.Add(pictureBox);
         return pictureBox;
      }

      void CréerLesthemes()
      {
         StreamReader reader = new StreamReader("../../Images/Themes/info.txt");
         themes = new List<ThemeName>();
         while (!reader.EndOfStream)
         {
            themes.Add(new ThemeName(reader.ReadLine()));
         }
         reader.Close();
      }

      Bitmap ChoisirImage(int numero, Cote cote)
      {
         Bitmap image = global::Matrix.Properties.Resources._3_G;
         string nom = numero.ToString() + "_";
         nom += cote == Cote.GAUCHE ? "G" : "D";
         switch (nom)
         {
            case "1_D":
               image = global::Matrix.Properties.Resources._1_D;
               break;
            case "1_G":
               image = global::Matrix.Properties.Resources._1_G;
               break;
            case "2_D":
               image = global::Matrix.Properties.Resources._2_D;
               break;
            case "2_G":
               image = global::Matrix.Properties.Resources._2_G;
               break;
            case "3_D":
               image = global::Matrix.Properties.Resources._3_D;
               break;
            case "3_G":
               image = global::Matrix.Properties.Resources._3_G;
               break;
            case "4_D":
               image = global::Matrix.Properties.Resources._4_D;
               break;
            case "4_G":
               image = global::Matrix.Properties.Resources._4_G;
               break;
            case "5_D":
               image = global::Matrix.Properties.Resources._5_D;
               break;
            case "5_G":
               image = global::Matrix.Properties.Resources._5_G;
               break;
            case "6_D":
               image = global::Matrix.Properties.Resources._6_D;
               break;
            case "6_G":
               image = global::Matrix.Properties.Resources._6_G;
               break;
            case "7_D":
               image = global::Matrix.Properties.Resources._7_D;
               break;
            case "7_G":
               image = global::Matrix.Properties.Resources._7_G;
               break;
            case "8_D":
               image = global::Matrix.Properties.Resources._8_D;
               break;
            case "8_G":
               image = global::Matrix.Properties.Resources._8_G;
               break;
            case "9_D":
               image = global::Matrix.Properties.Resources._9_D;
               break;
            case "9_G":
               image = global::Matrix.Properties.Resources._9_G;
               break;
         }
         return image;
      }

      public void ChangeCurrentTheme(int numero)
      {
         ImageDroite.Dispose();
         ImageDroite = null;
         ImageGauche.Dispose();
         ImageGauche = null;
         ImageGauche = CréerPictureBox(numero, Cote.GAUCHE);
         ImageDroite = CréerPictureBox(numero, Cote.DROITE);
         couleurDeFond = Color.FromName(themes.Find(x => x.numero == numero).color);
         form.BackColor = couleurDeFond;
         Save(numero);
      }

      public int GetNumero(string nom)
      {
         return themes.First(x => x.name == nom).numero;
      }
      public string GetFirstName()
      {
          return themes.Find(x => x.numero == starting).name;
      }
      void Save(int numero)
      {
          StreamWriter writer = new StreamWriter("../../Images/Themes/start.txt");
          writer.Write(numero);
          writer.Close();
      }
   }
}

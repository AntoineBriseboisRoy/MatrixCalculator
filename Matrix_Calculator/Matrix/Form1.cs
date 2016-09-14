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
   public enum Cote { DROITE, GAUCHE }
   public partial class Form1 : Form
   {
      public SaveManager saveManager;
      public TextBoxesManager Gauche;
      public TextBoxesManager Droite;
      AfficheurRéponse Réponse;
      string operation = "";
      public string type = "";
      public Theme Theme {get;set;}

      string path = "";

      public Form1()
      {
         Theme = new Theme(7, this);

         InitializeComponent();
         saveManager = new SaveManager();
         VerifierBouton();
         this.BackColor = Theme.couleurDeFond;
         ExecuteThema();
         this.Text = "♥ Calculator Aligator by Samuel Ducharme & Antoine Brisebois-Roy ♥";
      }

      private void button1_Click(object sender, EventArgs e)
      {
         Console.WriteLine("Hatsune Marde ♥");
         if (Réponse != null)
         {
            Réponse.Empty();
            Réponse = null;
         }
         switch (type)
         {
            case "Matrices":
               Matrix résultat = FaireOperationMatrices();
               Réponse = new AfficheurRéponse(résultat, (int)(Width / 2 - (50 * résultat.N / 2f)), 320, this);
               CreerLesTransferButton();
               break;
            case "SEL":
               string résultat1 = FaireOperationSEL();
               Réponse = new LongAfficheurRéponse(résultat1, (int)(Width / 2 - 25), 320, this);
               RemoveLesTransferButton();
               break;
            case "Vecteurs":
               Vecteur résulatvector = FaireOperationVecteurs();
               Réponse = new AfficheurRéponse(résulatvector.A, (int)(Width / 2 - (50 * résulatvector.A.N / 2f)), 320, this);
               CreerLesTransferButton();
               break;
            case "Bases et repères":
               Réponse = new LongAfficheurRéponse(FaireOperationBasesEtReperes(), (int)(Width / 2 - 25), 320, this);
               RemoveLesTransferButton();
               break;
         }

      }

      private void CreerLesTransferButton()
      {
         Gauche.CreerTransferButton();
         if (Droite != null)
         {
            Droite.CreerTransferButton();
         }
      }

      private void RemoveLesTransferButton()
      {
         if (Gauche != null)
         {
            Gauche.DeleteBoutonTransfereEtSystem32();
         }
         if (Droite != null)
         {
            Droite.DeleteBoutonTransfereEtSystem32();
         }
      }

      private Matrix FaireOperationMatrices()
      {
         Matrix matrix = Matrix.Null(0, 0);
         switch (operation)
         {
            case "Addition":
               matrix = Gauche.ToMatrix() + Droite.ToMatrix();
               break;
            case "Soustraction":
               matrix = Gauche.ToMatrix() - Droite.ToMatrix();
               break;
            case "Transposition":
               matrix = Gauche.ToMatrix().Transposee;
               break;
            case "Produit de matrices":
               matrix = Gauche.ToMatrix() * Droite.ToMatrix();
               break;
            case "Faux produit":
               matrix = Gauche.ToMatrix() & Droite.ToMatrix();
               break;
            case "Produit par un scalaire":
               matrix = Gauche.ToMatrix() * (Droite as ScalaireManager).ToFloat();
               break;
            case "Déterminant":
               float[,] temps = new float[1, 1];
               temps[0, 0] = Gauche.ToMatrix().Determinant;
               matrix = new Matrix(temps);
               break;
            case "Inverse":
               matrix = Gauche.ToMatrix().Inverse;
               break;
         }


         return matrix;

      }


      private string FaireOperationSEL()
      {
         SEL sel = new SEL(Gauche.ToMatrix(), Droite.ToMatrix());
         string s = "";

         if (operation == "Gauss")
         {
            s = sel.Gauss();
         }
         if (operation == "Inverse")
         {
            s = sel.MatriceInverse();
         }
         return s;
      }

      private Vecteur FaireOperationVecteurs()
      {
         Vecteur vector = new Vecteur(Matrix.Null(1, 1));
         Vecteur vectorG = null;
         Vecteur vectorD = null;
         if (Gauche != null)
         {
            vectorG = new Vecteur(Gauche.ToMatrix());
         }
         if (Droite != null)
         {
            vectorD = new Vecteur(Droite.ToMatrix());
         }

         switch (operation)
         {
            case "Addition":
               vector = vectorG + vectorD;
               break;
            case "Soustraction":
               vector = vectorG - vectorD;
               break;
            case "Produit par un scalaire":
               vector = vectorG * vectorD.Norme;
               break;
            case "Norme":
               float[,] tabl = new float[1, 1];
               tabl[0, 0] = vectorG.Norme;
               vector = new Vecteur(new Matrix(tabl));
               break;
            case "Normalisation":
               vector = vectorG.Normalisation;
               break;
            case "Vecteur résultant":
               vector = Vecteur.VecteurResultant(vectorG, vectorD);
               break;
         }
         return vector;
      }

      private string FaireOperationBasesEtReperes()
      {
         string s = "";
         BetR chose = new BetR(Gauche.ToMatrix());
         if (operation == "Test sur les bases")
         {
            s = chose.Test;
         }
         if (operation == "Écriture d'un vecteur")
         {
            s = chose.BaseVector(new Vecteur(Droite.ToMatrix()));
         }
         return s;
      }

      private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (type != (sender as ComboBox).Text)
         {
            type = ((sender as ComboBox).Text);
            switch (type)
            {
               case "Matrices":
                  this.cb_operation.Enabled = true;
                  cb_operation.Items.Clear();
                  cb_operation.Items.AddRange(new object[] {
            "Addition",
            "Soustraction",
            "Transposition",
            "Produit de matrices",
            "Faux produit",
            "Produit par un scalaire",
            "Déterminant",
            "Inverse"});
                  break;
               case "SEL":
                  this.cb_operation.Enabled = true;
                  cb_operation.Items.Clear();
                  cb_operation.Items.AddRange(new object[] {
            "Gauss",
            "Inverse"});
                  break;
               case "Vecteurs":
                  this.cb_operation.Enabled = true;
                  cb_operation.Items.Clear();
                  cb_operation.Items.AddRange(new object[] {
            "Addition",
            "Soustraction",
            "Produit par un scalaire",
            "Norme",
            "Normalisation",
             "Vecteur résultant"});
                  break;
               case "Bases et repères":
                  cb_operation.Items.Clear();
                  cb_operation.Items.AddRange(new object[] {
            "Test sur les bases",
            "Écriture d'un vecteur"});
                  this.cb_operation.Enabled = true;
                  break;
            }
            operation = cb_operation.Text;
            VerifierBouton();
         }
      }



      private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (operation != (sender as ComboBox).Text)
         {
            operation = ((sender as ComboBox).Text);
            ChangerOperation();

            VerifierBouton();
         }
      }


      private void ChangerOperation()
      {
         if (Gauche == null)
         {
            Gauche = CréerManager(Gauche, ChoisirTextBoxesManager(type, operation, Cote.GAUCHE), ChoisirMinLignes(type), ChoisirMaxLignes(type), Cote.GAUCHE);
         }
         else
         {
            Gauche = ChangerManager(Gauche, ChoisirTextBoxesManager(type, operation, Cote.GAUCHE), ChoisirMinLignes(type), ChoisirMaxLignes(type));
         }
         if (Droite == null)
         {
            Droite = CréerManager(Droite, ChoisirTextBoxesManager(type, operation, Cote.DROITE), ChoisirMinLignes(type), ChoisirMaxLignes(type), Cote.DROITE);
         }
         else
         {
            Droite = ChangerManager(Droite, ChoisirTextBoxesManager(type, operation, Cote.DROITE), ChoisirMinLignes(type), ChoisirMaxLignes(type));
         }
      }

      private TextBoxesManager ChangerManager(TextBoxesManager tbm, string newType, int minLignes, int maxLignes)
      {
         TextBoxesManager temps = null;
         switch (newType)
         {
            case "TextBoxesManagerRectangle":
               temps = new TextBoxesManagerRectangle(tbm.ToMatrix(), tbm.PositionX, tbm.PositionY, minLignes, maxLignes, tbm.Cote, this);
               tbm.Empty();
               tbm = temps;
               break;
            case "TextBoxesManagerCarré":
               temps = new TextBoxesManagerCarré(tbm.ToMatrix(), tbm.PositionX, tbm.PositionY, minLignes, maxLignes, tbm.Cote, this);
               tbm.Empty();
               tbm = temps;
               break;
            case "TextBoxesManagerColonne":
               temps = new TextBoxesManagerColonne(tbm.ToMatrix(), tbm.PositionX, tbm.PositionY, minLignes, maxLignes, tbm.Cote, this);
               tbm.Empty();
               tbm = temps;
               break;
            case "ScalaireManager":
               temps = new ScalaireManager(tbm.ToMatrix()[0, 0], tbm.PositionX, tbm.PositionY, tbm.Cote, this);
               tbm.Empty();
               tbm = temps;
               break;
            case "null":
               tbm.Empty();
               tbm = null;
               temps = null;
               break;
         }
         return temps;
      }

      private TextBoxesManager CréerManager(TextBoxesManager tbm, string newType, int minLignes, int maxLignes, Cote cote)
      {
         int x = cote == Cote.GAUCHE ? 0 : 520;
         int y = 20;
         TextBoxesManager temps = null;
         switch (newType)
         {
            case "TextBoxesManagerRectangle":
               temps = new TextBoxesManagerRectangle(x, y, minLignes, maxLignes, cote, this);
               tbm = temps;
               break;
            case "TextBoxesManagerCarré":
               temps = new TextBoxesManagerCarré(x, y, minLignes, maxLignes, cote, this);
               tbm = temps;
               break;
            case "TextBoxesManagerColonne":
               temps = new TextBoxesManagerColonne(x, y, minLignes, maxLignes, cote, this);
               tbm = temps;
               break;
            case "ScalaireManager":
               temps = new ScalaireManager(x, y, cote, this);
               tbm = temps;
               break;
         }
         return temps;
      }

      private TextBoxesManager CréerManagerAvecUnTableau(string[,] tableau, TextBoxesManager tbm, string newType, int minLignes, int maxLignes, Cote cote)
      {
         int x = cote == Cote.GAUCHE ? 0 : 520;
         int y = 20;
         TextBoxesManager temps = null;
         switch (newType)
         {
            case "TextBoxesManagerRectangle":
               temps = new TextBoxesManagerRectangle(tableau, x, y, minLignes, maxLignes, cote, this);
               tbm = temps;
               break;
            case "TextBoxesManagerCarré":
               temps = new TextBoxesManagerCarré(tableau, x, y, minLignes, maxLignes, cote, this);
               tbm = temps;
               break;
            case "TextBoxesManagerColonne":
               temps = new TextBoxesManagerColonne(tableau, x, y, minLignes, maxLignes, cote, this);
               tbm = temps;
               break;
            case "ScalaireManager":
               temps = new ScalaireManager(tableau[0, 0], x, y, cote, this);
               tbm = temps;
               break;
         }
         return temps;
      }

      string ChoisirTextBoxesManagerMatrices(string operation, Cote cote)
      {
         string s = "";
         if (operation == "Déterminant" || operation == "Inverse" || operation == "Transposition")
         {
            s = cote == Cote.GAUCHE ? "TextBoxesManagerCarré" : "null";
         }
         else if (operation == "Produit par un scalaire" && cote == Cote.DROITE)
         {
            s = "ScalaireManager";
         }
         else
         {
            s = "TextBoxesManagerRectangle";
         }
         return s;
      }

      string ChoisirTextBoxesManagerSEL(string operation, Cote cote)
      {
         string s = "";
         if (cote == Cote.GAUCHE)
         {
            if (operation == "Gauss")
            {
               s = "TextBoxesManagerRectangle";
            }
            else
            {
               s = "TextBoxesManagerCarré";
            }
         }
         else
         {
            s = "TextBoxesManagerColonne";
         }
         return s;
      }

      string ChoisirTextBoxesManagerVecteurs(string operation, Cote cote)
      {

         string s = "";
         if (cote == Cote.GAUCHE)
         {
            s = "TextBoxesManagerColonne";
         }
         else
         {
            if (operation == "Norme" || operation == "Normalisation")
            {
               s = "null";
            }
            else if (operation == "Produit par un scalaire")
            {
               s = "ScalaireManager";
            }
            else
            {
               s = "TextBoxesManagerColonne";
            }
         }

         return s;
      }


      string ChoisirTextBoxesManagerBasesEtReperes(string operation, Cote cote)
      {
         string s = "";
         if (cote == Cote.GAUCHE)
         {
               s = "TextBoxesManagerCarré";
         }
         else
         {
            if (operation == "Test sur les bases")
            {
               s = "null";
            }
            else
            {
               s = "TextBoxesManagerColonne";
            }
         }
         return s;
      }




      string ChoisirTextBoxesManager(string type, string operation, Cote cote)
      {
         string s = "";
         switch (type)
         {
            case "Matrices":
               s = ChoisirTextBoxesManagerMatrices(operation, cote);
               break;
            case "SEL":
               s = ChoisirTextBoxesManagerSEL(operation, cote);
               break;
            case "Vecteurs":
               s = ChoisirTextBoxesManagerVecteurs(operation, cote);
               break;
            case "Bases et repères":
               s = ChoisirTextBoxesManagerBasesEtReperes(operation, cote);
               break;
         }
         return s;
      }

      int ChoisirMinLignes(string type)
      {
         return type == "Matrices" ? 1 : 2;
      }
      int ChoisirMaxLignes(string type)
      {
         return type == "Matrices" ? 7 : 3;
      }

      public void VerifierBouton()
      {
         switch (type)
         {
            case "Matrices":
               VerifierBoutonMatrices();
               break;
            case "SEL":
               VerifierBoutonSEL();
               break;
            case "Vecteurs":
               VerifierBoutonVecteurs();
               break;
            case "Bases et repères":
               VerifierBoutonBasesEtReperes();
               break;
            case "":
               btn_calculer.Enabled = false;
               break;
         }
         if (operation == "")
         {
            btn_calculer.Enabled = false;
         }

      }

      void VerifierBoutonMatrices()
      {
         if (MatricesInvalides() || !EstOperationValide())
         {
            DisablerBouton();
         }
         else
         {
            btn_calculer.Enabled = true;
         }
      }

      void VerifierBoutonSEL()
      {
         if (Droite == null || MatricesInvalides() || Gauche.TextBoxes.GetLength(0) != Droite.TextBoxes.GetLength(0))
         {
            DisablerBouton();
         }
         else
         {
            btn_calculer.Enabled = true;
         }
      }

      void VerifierBoutonVecteurs()
      {
         if (operation == "")
         {
            DisablerBouton();
         }
         else if (Droite == null && MatricesInvalides())
         {
            DisablerBouton();
         }
         else if (Droite != null && operation != "Produit par un scalaire" && (MatricesInvalides() || Gauche.TextBoxes.GetLength(0) != Droite.TextBoxes.GetLength(0)))
         {
            DisablerBouton();
         }

         else
         {
            btn_calculer.Enabled = true;
         }
      }

      void VerifierBoutonBasesEtReperes()
      {
         if (operation == "")
         {
            DisablerBouton();
         }
         else if (Droite == null && MatricesInvalides())
         {
            DisablerBouton();
         }
         else if ((Droite != null && Gauche != null) && (operation == "Écriture d'un vecteur" && Gauche.TextBoxes.GetLength(0) != Droite.TextBoxes.GetLength(0)))
         {
            DisablerBouton();
         }
         else
         {
            btn_calculer.Enabled = true;
         }
      }

      public void DisablerBouton()
      {
         btn_calculer.Enabled = false;
      }

      bool MatricesInvalides()
      {
         return Gauche == null || !Gauche.IsOk || Droite != null && !Droite.IsOk;
      }

      bool EstOperationValide()
      {
         bool valide = true;

         if (Gauche != null && Droite != null)
         {
            if ((operation == "Addition" || operation == "Soustraction" || operation == "Faux produit"))
            {
               valide = !(Gauche.ToMatrix().M != Droite.ToMatrix().M || Gauche.ToMatrix().N != Droite.ToMatrix().N);
            }
            else if (operation == "Produit de matrices")
            {
               valide = Gauche.ToMatrix().N == Droite.ToMatrix().M;
            }
         }
         else if (operation == "")
         {
            valide = false;
         }

         return valide;
      }

      public Matrix MatrixRéponse()
      {
         return Réponse.ToMatrix();
      }

      public bool RéponseExiste()
      {
         return Réponse != null && Réponse.GetType() != typeof(LongAfficheurRéponse);
      }

      private void Form1_Load(object sender, EventArgs e)
      {

      }

      private void enregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
      {
         SaveFileDialog dialog = new SaveFileDialog();
         dialog.AddExtension = true;
         dialog.DefaultExt = "matrixproj";
         dialog.Filter = "Fichier de projet de matrice (*.matrixproj)|*.matrixproj";
         dialog.FileName = "sans titre.matrixproj";
         dialog.ShowDialog();
         if (dialog.FileName != "sans titre.matrixproj")
         {
            string path = dialog.FileName;
            CreateMatrixProj(path);
            this.path = path;
            this.Text = path + "♥ Calculator Aligator by Samuel Ducharme & Antoine Brisebois-Roy ♥";
         }

         dialog.Dispose();

      }

      private void CreateMatrixProj(string path)
      {
         StreamWriter writer = new StreamWriter(path);
         writer.WriteLine(cb_type.Text);
         writer.WriteLine(cb_operation.Text);
         if (Gauche != null)
         {
            writer.WriteLine("Gauche");
            writer.WriteLine("");
            writer.WriteLine(Gauche.ToSavableString());
         }
         if (Droite != null)
         {
            writer.WriteLine("Droite");
            writer.WriteLine("");
            writer.WriteLine(Droite.ToSavableString());
         }
         if (Réponse != null)
         {
            writer.WriteLine("Reponse");
            writer.WriteLine(Réponse.GetType().ToString());
            writer.WriteLine(Réponse.ToSavableString());
         }
         foreach (SavedMatrix s in saveManager.matrices)
         {
            writer.WriteLine("SaveManager");
            writer.WriteLine(s.Nom);
            writer.WriteLine(s.ToSavableString());
         }
         writer.Close();
      }

      private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
      {
         OpenFileDialog dialog = new OpenFileDialog();
         dialog.DefaultExt = "matrixproj";
         dialog.Filter = "Fichier de projet de matrice (*.matrixproj)|*.matrixproj";
         dialog.ShowDialog();
         if (dialog.FileName != "")
         {
            string path = dialog.FileName;
            this.Text = path + " ♥ Calculator Aligator by Samuel Ducharme & Antoine Brisebois-Roy ♥";
            LireUnFichierMatrixProj(path);
         }
      }

      private void LireUnFichierMatrixProj(string path)
      {
         StreamReader reader = new StreamReader(path);
         string type = reader.ReadLine();
         cb_type.Text = type;
         this.type = type;
         string operationLue = reader.ReadLine();
         cb_operation.Text = operationLue;
         operation = operationLue;
         saveManager.Clear();
         while (!reader.EndOfStream)
         {
            string nom = reader.ReadLine();
            string sousNom = reader.ReadLine();
            int x = int.Parse(reader.ReadLine());
            int y = int.Parse(reader.ReadLine());

            string[,] tableau = new string[x, y];
            for (int i = 0; i < x; ++i)
            {
               for (int j = 0; j < y; ++j)
               {
                  tableau[i, j] = reader.ReadLine();
               }
            }
            
            CreerAPartirDuFichier(nom, sousNom, operationLue, tableau);
         }
         
         VerifierBouton();
         reader.Close();
         this.path = path;
      }

      private void CreerAPartirDuFichier(string nom, string sousNom, string operationLue, string[,] tableau)
      {
         switch (nom)
         {
            case "Gauche":
               if (Gauche != null)
               {
                  Gauche.Empty();
                  Gauche = null;
               }
               //Gauche = ChoisirTextBoxesManager(tableau, operationLue, 0, 20,1, 7, this, Cote.GAUCHE);
               Gauche = CréerManagerAvecUnTableau(tableau, Gauche, ChoisirTextBoxesManager(type, operation, Cote.GAUCHE), ChoisirMinLignes(type), ChoisirMaxLignes(type), Cote.GAUCHE);


               break;
            case "Droite":
               if (Droite != null)
               {
                  Droite.Empty();
                  Droite = null;
               }
               //Droite = ChoisirTextBoxesManager(tableau, operationLue, 520, 20, 1,7, this, Cote.DROITE);
               Droite = CréerManagerAvecUnTableau(tableau, Droite, ChoisirTextBoxesManager(type, operation, Cote.DROITE), ChoisirMinLignes(type), ChoisirMaxLignes(type), Cote.DROITE);
               break;
            case "Reponse":
               if (Réponse != null)
               {
                  Réponse.Empty();
                  Réponse = null;
               }
               if (sousNom == typeof(LongAfficheurRéponse).ToString())
               {
                  Réponse = new LongAfficheurRéponse(tableau[0,0], (int)(Width / 2 - (50 * tableau.GetLength(1) / 2f)), 320, this);
               }
               else
               {
                  Réponse = new AfficheurRéponse(tableau, (int)(Width / 2 - (50 * tableau.GetLength(1) / 2f)), 320, this);
                  CreerLesTransferButton();
               }
              
               break;
            case "SaveManager":
               saveManager.Add(new SavedMatrix(sousNom, tableau));
               break;
         }
      }

      private void ThemeChangerClicSelectionLOL(object sender, EventArgs e)
      {
         Theme.ChangeCurrentTheme(Theme.GetNumero((sender as ToolStripMenuItem).Text));
            foreach (ToolStripItem t in this.themeToolStripMenuItem.DropDownItems)
            {
               (t as ToolStripMenuItem).Checked = false;
            }
            (sender as ToolStripMenuItem).Checked = true;
            
      }

      private void ExecuteThema()
      {
         
         ToolStripItem[] tabluea = new System.Windows.Forms.ToolStripItem[Theme.themes.Count];
         Theme.themes.Sort();
         for (int i = 0; i < Theme.themes.Count; ++i)
         {
            ToolStripMenuItem ToolStripchose = new ToolStripMenuItem();
            ToolStripchose.CheckOnClick = true;
            ToolStripchose.Name = "toolStripMenuItem"+i.ToString();
            ToolStripchose.Size = new System.Drawing.Size(80, 22);
            ToolStripchose.Text = Theme.themes[i].name;
            if (Theme.themes[i].name == Theme.GetFirstName())
            {
               ToolStripchose.Checked = true;
            }
            tabluea[i] = ToolStripchose;
            ToolStripchose.Click += new System.EventHandler(this.ThemeChangerClicSelectionLOL);
         }
         this.themeToolStripMenuItem.DropDownItems.AddRange(tabluea);
      }

      private void themeToolStripMenuItem_Click(object sender, EventArgs e)
      {

      }

      private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (path == "")
         {
            enregistrerSousToolStripMenuItem_Click(sender, e);
         }
         else
         {
            CreateMatrixProj(this.path);
         }
         
      }

      private void quitterToolStripMenuItem1_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class Vecteur
    {
        public Matrix A { get; set; }
        public Vecteur(Matrix a)
        {
            A = a;
        }

        static Vecteur Addition(Vecteur a, Vecteur b)
        {
            return new Vecteur(a.A + b.A);
        }

        static Vecteur Soustraction(Vecteur a, Vecteur b)
        {
            return new Vecteur(a.A - b.A);
        }

        static Vecteur ProduitParScalaire(Vecteur a, float b)
        {
            return new Vecteur(a.A * b);
        }

        public float Norme
        {
           get{
float produit = 0;
            foreach(float a in A.TableauElements)
            {
                produit += a * a;
            }
            return (float)Math.Sqrt(produit);
           }
            
        }

        public Vecteur Normalisation
        {
           get
           {
              float norme = Norme;
              float[,] tableau = new float[A.M,1];
              for(int i = 0; i < tableau.GetLength(0); ++i)
              {
                  tableau[i,0] = A.TableauElements[i,0]/norme;
              }
              return new Vecteur(new Matrix(tableau));
           }
        }

        public static Vecteur VecteurResultant(Vecteur a, Vecteur b)
        {
            return new Vecteur(b.A - a.A);
        }


       //Opérateurs... !!!
        public static Vecteur operator +(Vecteur a, Vecteur b)
        {
           return Addition(a, b);
        }
        public static Vecteur operator -(Vecteur a, Vecteur b)
        {
           return Soustraction(a, b);
        }
        public static Vecteur operator *(Vecteur a, float b)
        {
           return ProduitParScalaire(a, b);
        }
        public static Vecteur operator *(float b, Vecteur a)
        {
           return ProduitParScalaire(a, b);
        }
    }
}

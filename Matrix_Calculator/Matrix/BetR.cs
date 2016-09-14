using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class BetR
    {
        Matrix A { get; set; }
        private bool Veritas { get { return A.Determinant != 0; } }
        public string Test
        {
            get 
            {
                return !Veritas ? "La base donnée n'est pas valide." : "La base donnée est valide.";
            }
        }
        public BetR(Matrix a)
        {
            A = a;
        }
        public string BaseVector(Vecteur vector)
        {
            return Veritas ? "Le vecteur donné vaut "+new SEL(A, vector.A).MatriceInverseSansEnsemble() + " dans la base donnée.": "Le vecteur donné ne peut être exprimé dans la base donnée.";
        }



    }
}

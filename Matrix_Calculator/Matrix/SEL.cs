using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class SEL
    {
        Matrix A { get; set; }
        Matrix B { get; set; }
        Matrix C { get; set; }
        public SEL(Matrix a, Matrix b)
        {
            A = a;
            B = b;
            C = FindAugment();
        }

        private Matrix FindAugment()
        {
            float[,] tableauElementsA = A.TableauElements;
            float[,] tableauElementsB = B.TableauElements;
            float[,] tableauElementsC = new float[A.M, A.N + B.N];

            for (int i = 0; i < tableauElementsC.GetLength(0); ++i)
            {
                for (int j = 0; j < tableauElementsC.GetLength(1); ++j)
                {
                    if (j < tableauElementsA.GetLength(1))
                    {
                        tableauElementsC[i, j] = tableauElementsA[i, j];
                    }
                    else
                    {
                        tableauElementsC[i, j] = tableauElementsB[i, j - A.N];
                    }
                }
            }
            return new Matrix(tableauElementsC);
        }

        public string Gauss()
        {
            string reponse = "";
            Matrix echelon = Matrix.ÉchelonnerMatrice(C, A.N);
            Matrix aEchelon = Matrix.ÉchelonnerMatrice(A, A.N);
            int rangA = Matrix.NombrePivot(aEchelon.TableauElements);
            int rangEchelon = Matrix.NombrePivot(echelon.TableauElements);
            if (rangEchelon > rangA)           // Aucune Solution
            {
                reponse = "Le SEL ne possède aucune solution.";
            }
            else if (rangA == aEchelon.N)     //Solution Unique
            {
               reponse = SolutionUnique(echelon.TableauElements);
            }
            else                               //Infinité Solution
            {
                reponse = InfiniteSolution(echelon.TableauElements);
            }
            return reponse;
        }

        public string MatriceInverse()
        {
            string s = "Ensemble Solution : { ";
            try
            {
               Matrix a = A.Inverse * B;
               s += a[0, 0].ToString() + " ; ";
               s += a[1, 0].ToString();
               if (a.M == 3)
               {
                  s += " ; " + a[2, 0].ToString();
               }
               s += " }";
            }
            catch (Exception)
            {
               s = "Ce SEL ne peut pas être résolu par la méthode de la matrice inverse.";
            }
            return s;
        }

        public string MatriceInverseSansEnsemble()
        {
           string s = "( ";
           try
           {
              Matrix a = A.Inverse * B;
              s += a[0, 0].ToString() + " ; ";
              s += a[1, 0].ToString();
              if (a.M == 3)
              {
                 s += " ; " + a[2, 0].ToString();
              }
              s += " )";
           }
           catch (Exception)
           {
              s = "Ce SEL ne peut pas être résolu par la méthode de la matrice inverse.";
           }
           return s;
        }
        private string SolutionUnique(float[,] elements)
        {
            if(elements.GetLength(0)==2 || Matrix.EstLigneNulle(2, elements))
            {
                float y = elements[1, 2] / elements[1, 1];
                float x = (elements[0, 2] - (elements[0, 1] * y)) / elements[0, 0];
                return "Ensemble Solution : { " + x + " ; " + y + " }";
            }
            else
            {
                float z = elements[2, 3] / elements[2, 2];
                float y = (elements[1, 3] - (elements[1, 2] * z)) / elements[1, 1];
                float x = (elements[0, 3] - (elements[0, 2] * z) - (elements[0, 1] * y)) / elements[0, 0];
                return "Ensemble Solution : { " + x + " ; " + y + " ; "+ z +" }";
            }
        }

        private string InfiniteSolution(float[,] elements)
        {
            if (Matrix.EstLigneNulle(0, elements) && elements.GetLength(1) == 4) return "Ensemble Solution : { r ; s ; t } r,s,t ∈ ℝ";
            if (Matrix.EstLigneNulle(0, elements) && elements.GetLength(1) == 3) return "Ensemble Solution : { s ; t } s,t ∈ ℝ";
            if (elements.GetLength(0) == 1 && elements.GetLength(1) == 4 || Matrix.EstLigneNulle(1, elements))     //Matrix 1 par 3
            {
                if (elements.GetLength(1) == 3)
                {
                    if (elements[0, 0] == 0 && elements[0,2] == 0) return "Ensemble Solution : { t ; 0 } t ∈ ℝ";
                    if (elements[0, 1] == 0 && elements[0, 2] == 0) return "Ensemble Solution : { 0 ; t } t ∈ ℝ";
                    if (elements[0, 0] == 0) return "Ensemble Solution : { t ; "+ elements[0,2]/elements[0,1]+" } t ∈ ℝ";
                    if (elements[0, 1] == 0) return "Ensemble Solution : { "+ elements[0, 2] / elements[0, 0] +" ; t } t ∈ ℝ";
                    else
                    {
                        InfiniteRep x = new InfiniteRep(elements[0, 2], elements[0, 1], 't') / elements[0, 0];
                        return "Ensemble Solution : { "+ x +" ; t } t ∈ ℝ";
                    }
                }
                else
                {
                    if (elements[0, 0] == 0 && elements[0, 1] == 0)
                    {
                        if (elements[0, 3] == 0)
                        {
                            return "Ensemble Solution : { s ; t ; 0 } s,t ∈ ℝ";
                        }
                        return "Ensemble Solution : { s ; t ; " + elements[0, 3] / elements[0, 2] + " } s,t ∈ ℝ";
                    }
                    if (elements[0, 0] == 0 && elements[0, 2] == 0)
                    {
                        if (elements[0, 3] == 0)
                        {
                            return "Ensemble Solution : { s ; 0 ; t } s,t ∈ ℝ";
                        }
                        return "Ensemble Solution : { s ; " + elements[0, 3] / elements[0, 1] + " ; t } s,t ∈ ℝ";
                    }
                    if (elements[0, 2] == 0 && elements[0, 1] == 0)
                    {
                        if (elements[0, 3] == 0)
                        {
                            return "Ensemble Solution : { 0 ; s ; t } s,t ∈ ℝ";
                        }
                        return "Ensemble Solution : { " + elements[0, 3] / elements[0, 0] + " ; s ; t } s,t ∈ ℝ";
                    }
                    if (elements[0, 0] == 0)
                    {
                        if (elements[0, 3] == 0)
                        {
                            InfiniteRep d = new InfiniteRep(elements[0, 2], 0, 't') / new InfiniteRep(0, elements[0, 1], 't');
                            return "Ensemble Solution : { s ; " + d.ToString() + " ; t } s,t ∈ ℝ";
                        }
                        InfiniteRep y = new InfiniteRep(elements[0, 3], elements[0, 2], 't') / elements[0, 1];
                        return "Ensemble Solution : { s ; " + y.ToString() + " ; t } s,t ∈ ℝ";
                    }
                    if (elements[0, 1] == 0)
                    {
                        if (elements[0, 3] == 0)
                        {
                            InfiniteRep d = new InfiniteRep(elements[0, 2], 0, 't') / new InfiniteRep(0, elements[0, 0], 't');
                            return "Ensemble Solution : { " + d.ToString() + " ; s ; t } s,t ∈ ℝ";
                        }
                        InfiniteRep x = new InfiniteRep(elements[0, 3], elements[0, 2], 't') / elements[0, 0];
                        return "Ensemble Solution : { " + x.ToString() + " ; s ; t } s,t ∈ ℝ";
                    }
                    if (elements[0, 2] == 0)
                    {
                        if (elements[0, 3] == 0)
                        {
                            InfiniteRep d = new InfiniteRep(elements[0, 1], 0, 't') / new InfiniteRep(0, elements[0, 0], 's');
                            return "Ensemble Solution : { " + d.ToString() + " ; s ; t } s,t ∈ ℝ";
                        }
                        InfiniteRep x = new InfiniteRep(elements[0, 3], elements[0, 1], 's') / elements[0, 0];
                        return "Ensemble Solution : { " + x.ToString() + " ; s ; t } s,t ∈ ℝ";
                    }
                    else
                    {
                        InfiniteRep2 x = new InfiniteRep2(elements[0, 3], elements[0, 2], 't', elements[0, 1], 's') / elements[0, 0];
                        return "Ensemble Solution : { " + x.ToString() + " ; s ; t } s,t ∈ ℝ";
                    }
                }
            }
            else                                       //Matrix 2 par 4 ou 3 par 4 (derniere ligne de zero)
            {
                if (elements[0, 0] == 0)
                {
                    float z = (elements[1, 3] - elements[1, 1]) / elements[1, 2];
                    return "Ensemble Solution : { t ; " + (elements[0, 3] - elements[0, 2]*z) / elements[0, 1] + " ; " + z + " } t ∈ ℝ";
                }
                if(elements[1,1]==0)
                {
                    float z = elements[1, 3] / elements[1, 2];
                    float xa = elements[0, 3] - elements[0, 2] * z;
                    InfiniteRep x = new InfiniteRep(xa, elements[0, 1], 't') / elements[0, 0];
                    return "Ensemble Solution : { " + x.ToString() +" ; t ; " + z + " } t ∈ ℝ";
                }
                if (elements[1, 2] == 0)
                {
                    float y1 = elements[1, 3] / elements[1, 1];
                    float xa = elements[0, 3] - elements[0, 1] * y1;
                    InfiniteRep x = new InfiniteRep(xa, elements[0, 2], 't') / elements[0, 0];
                    return "Ensemble Solution : { " + x.ToString() + " ; "+ y1 +" ; t } t ∈ ℝ"; 
                }
                
                InfiniteRep y = new InfiniteRep(elements[1, 3], elements[1, 2], 't') / elements[1, 1];
                InfiniteRep x1 = elements[0, 1] * y;
                InfiniteRep x2 = new InfiniteRep(elements[0, 3], elements[0, 2], 't');
                InfiniteRep x3 = x2 - x1;
                InfiniteRep x4 = x3 / elements[0, 0];
                
                return "Ensemble Solution : { " + x4.ToString() + " ; " + y.ToString() + " ; t } t ∈ ℝ";    
                
            }
        }
        

    }
}

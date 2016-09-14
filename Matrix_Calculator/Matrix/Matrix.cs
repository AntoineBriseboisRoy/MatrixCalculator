using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    class AdditionInvalideException : ApplicationException { }
    class MultiplicationInvalideException : ApplicationException { }
    class EstPasCarreeException : ApplicationException { }
    class FauxProduitInvalideException : ApplicationException { }
    class InversionInvalideException : ApplicationException { }

    public class Matrix
    {
        float[,] Elements { get; set; }

        public Matrix(float[,] elements)
        {
            Elements = elements;
        }

        //Choses privates

        static Matrix Addition(Matrix A, Matrix B)
        {
            float[,] elements = new float[0, 0];
            if (A.M == B.M && A.N == B.N)
            {
                elements = new float[A.M, A.N];
                for (int i = 0; i < A.M; ++i)
                {
                    for (int j = 0; j < A.N; ++j)
                    {
                        elements[i, j] = A[i, j] + B[i, j];
                    }
                }
            }
            else
            {
                throw new AdditionInvalideException();
            }

            return new Matrix(elements);
        }

        static Matrix Multiplication(Matrix A, Matrix B)
        {
            float[,] elements = new float[0, 0];
            if (A.N == B.M)
            {
                elements = new float[A.M, B.N];
                for (int i = 0; i < elements.GetLength(0); ++i)
                {
                    for (int j = 0; j < elements.GetLength(1); ++j)
                    {
                        float somme = 0;
                        for (int k = 0; k < A.N; ++k)
                        {
                            somme += A[i, k] * B[k, j];
                        }
                        elements[i, j] = somme;
                    }
                }
            }
            else
            {
                throw new MultiplicationInvalideException();
            }

            return new Matrix(elements);
        }

        static Matrix MultiplicationScalaire(Matrix A, float f)
        {
            float[,] elements = new float[A.M, A.N];

            for (int i = 0; i < elements.GetLength(0); ++i)
            {
                for (int j = 0; j < elements.GetLength(1); ++j)
                {
                    elements[i, j] = A[i, j] * f;
                }
            }


            return new Matrix(elements);
        }

        static Matrix FauxProduit(Matrix A, Matrix B)
        {
            float[,] elements = new float[0, 0];
            if (A.M == B.M && A.N == B.N)
            {
                elements = new float[A.M, A.N];
                for (int i = 0; i < A.M; ++i)
                {
                    for (int j = 0; j < A.N; ++j)
                    {
                        elements[i, j] = A[i, j] * B[i, j];
                    }
                }
            }
            else
            {
                throw new FauxProduitInvalideException();
            }
            return new Matrix(elements);
        }

        static Matrix FaireTransposee(Matrix A)
        {
            float[,] elements = new float[A.N, A.M];
            for (int i = 0; i < A.M; ++i)
            {
                for (int j = 0; j < A.N; ++j)
                {
                    elements[j, i] = A[i, j];
                }
            }


            return new Matrix(elements);
        }

        static float TrouverDeterminant(Matrix A)
        {
            float det = 0;

            if (A.M == 2)
            {
                det = A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];
            }
            else if (A.M == 1)
            {
                det = A[0, 0];
            }
            else
            {
                for (int i = 0; i < A.M; ++i)
                {
                    float nombre = A[0, i];
                    if (i % 2 == 1)
                    {
                        nombre *= -1;
                    }
                    det += nombre * TrouverDeterminant(A.Mineur(0, i));
                }
            }
            return det;
        }



        //Choses publiques

        public float[,] TableauElements
        {
            get
            { 
               return Elements.Clone() as float[,];
            }
        }
        public int M
        {
            get
            {
                return Elements.GetLength(0);
            }
        }

        public int N
        {
            get
            {
                return Elements.GetLength(1);
            }
        }

        public float this[int m, int n]
        {
            get
            {
               return Elements[m, n];
            }
        }

        public float Determinant
        {
            get
            {
                if (EstCarree)
                {
                    return TrouverDeterminant(this);
                }
                else
                {
                    throw new EstPasCarreeException();
                }
            }
        }

        public bool EstCarree
        {
            get
            {
                return M == N;
            }
        }

        public Matrix Transposee
        {
            get
            {
                return FaireTransposee(this);
            }
        }

        public Matrix Mineur(int m, int n)
        {
            float[,] elements = new float[M - 1, N - 1];
            for (int i = 0; i < M; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    if (i != m && j != n)
                    {
                        int positioni = i;
                        int positionj = j;
                        if (i > m)
                        {
                            --positioni;
                        }
                        if (j > n)
                        {
                            --positionj;
                        }

                        elements[positioni, positionj] = this[i, j];
                    }
                }
            }
            return new Matrix(elements);
        }

        public Matrix Cofacteurs
        {
            get
            {
                float[,] elements = new float[this.M, this.N];
                for (int i = 0; i < elements.GetLength(0); ++i)
                {
                    for (int j = 0; j < elements.GetLength(1); ++j)
                    {
                        float multiplimode = 1;
                        if ((i + j) % 2 == 1)
                        {
                            multiplimode = -1;
                        }
                        elements[i, j] = multiplimode * (this.Mineur(i, j).Determinant);
                    }
                }
                return new Matrix(elements);

            }
        }

        public Matrix Adjointe
        {
            get
            {
                return this.Cofacteurs.Transposee;
            }
        }

        public Matrix Inverse
        {
            get
            {
                float det = this.Determinant;
                if (det == 0)
                {
                    throw new InversionInvalideException();
                }
                else
                {
                    return (1f / det) * this.Adjointe;
                }
            }
        }

        public static Matrix Random(int m, int n)
        {
            int min = -100;
            int max = 101;
            Random random = new Random();
            float[,] tabl = new float[m, n];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    tabl[i, j] = random.Next(min, max);
                }
            }
            return new Matrix(tabl);
        }

        public static Matrix Null(int m, int n)
        {
            float[,] tabl = new float[m, n];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    tabl[i, j] = 0;
                }
            }
            return new Matrix(tabl);
        }

        public static Matrix Identity(int m)
        {
            float[,] tabl = new float[m, m];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (i == j)
                    {
                        tabl[i, j] = 1;
                    }
                    else
                    {
                        tabl[i, j] = 0;
                    }

                }
            }
            return new Matrix(tabl);
        }

        //Code  erroné 
        public static Matrix ÉchelonnerMatrice(Matrix A, int nombreDeColonne)
        {
            int i = 0,j = 0;
            float[,] elements = A.Elements.Clone() as float[,];
            elements = PivoterLigne(elements);
           

            while(j < nombreDeColonne && i < A.M)
            {
                    if(EstUnPivot(elements,new Point(i,j)))
                    {
                        ++i;
                    }
                    else if(!EstNulle(elements[i,j]))
                    {
                        elements = ApparaitreUnPivot(elements, new Point(i, j));
                        ++i;
                    }
                ++j;
            }
            elements = PivoterLigne(elements);
            return new Matrix(elements);
        }

        private static float[,] ApparaitreUnPivot(float[,] elements, Point ligne1)
        {
            float coefficient1 = elements[ligne1.A, ligne1.B];
            for(int i = 1; i < elements.GetLength(0)-ligne1.A; ++i)
            {
                float coefficient2 = elements[ligne1.A + i, ligne1.B];
                if (coefficient2 != 0)
                {
                    for (int k = 0; k < elements.GetLength(1); ++k)
                    {
                        elements[ligne1.A + i, k] = coefficient1 * elements[ligne1.A + i, k] - coefficient2 * elements[ligne1.A, k];
                    }
                }
            }
            
            return elements;

        }



        public static bool EstLigneNulle(int ligne, float[,] elements)
        {
            bool nul = true;
            for (int i = 0; i < elements.GetLength(1); ++i)
            {
                nul = nul && elements[ligne, i] == 0;
            }
            return nul;
        }
        private static bool EstColonneNulle(int colonne, float[,] elements)
        {
            bool nul = true;
            for (int i = 0; i < elements.GetLength(0); ++i)
            {
                nul = nul && elements[i, colonne] == 0;
            }
            return nul;
        }

        private static int PositionPivot(float[,] elements, int indice)
        {
            int positionPivot = int.MaxValue;
            for (int i = 0; i < elements.GetLength(1); ++i)
            {
                if (elements[indice, i] != 0)
                {
                    positionPivot = i;
                    break;
                }
            }
            return positionPivot;
        }

        public static float[,] PivoterLigne(float[,] elements)
        {
            List<Point> positionsDesPivots = new List<Point>();
            for (int i = 0; i < elements.GetLength(0); ++i)
            {
                positionsDesPivots.Add(new Point(PositionPivot(elements, i), i));
            }
            positionsDesPivots.Reverse();
            positionsDesPivots.Sort();
            float[,] position = new float[elements.GetLength(0), elements.GetLength(1)];
            for (int i = 0; i < elements.GetLength(0); ++i)
            {
                for (int j = 0; j < elements.GetLength(1); ++j)
                {
                    position[i, j] = elements[positionsDesPivots[i].B, j];
                }
            }
            return position;
        }

        static bool EstUnPivot(float[,] elements, Point position)
        {
            bool veritas = !EstNulle(elements[position.A, position.B]);
            if (veritas)
            {
                for (int i = 0; i < position.B; ++i)
                {
                    veritas = veritas && elements[position.A, i] == 0;
                }
                for (int i = position.A; i < elements.GetLength(0)-1; ++i)
                {
                    veritas = veritas && elements[i+1, position.B] == 0;
                }
            }
            return veritas;
        }

        public static int NombrePivot(float[,] elements)
        {
            int nbPivot = 0;
            for(int i = 0; i< elements.GetLength(0); ++i)
            {
                for(int j = 0; j<elements.GetLength(1);++j)
                {
                    if(EstUnPivot(elements,new Point(i,j)))
                    {
                        ++nbPivot;
                    }
                }
            }
            return nbPivot;
        }
        private static bool EstNulle(float e)
        {
            return e == 0;
        }
        //Operateurs----------------------------------------------------------------------------

        public static Matrix operator +(Matrix A, Matrix B)
        {
            return Addition(A, B);
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            return Addition(A, -1 * B);
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            return Multiplication(A, B);
        }

        public static Matrix operator *(Matrix A, float f)
        {
            return MultiplicationScalaire(A, f);
        }

        public static Matrix operator *(float f, Matrix A)
        {
            return MultiplicationScalaire(A, f);
        }

        public static Matrix operator &(Matrix A, Matrix B)
        {
            return FauxProduit(A, B);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < M; ++i)
            {
                s += "(     ";
                for (int j = 0; j < N; ++j)
                {
                    s += this[i, j].ToString().PadRight(15);
                }
                s += ")\n";
            }
            return s;
        }

        public string[,] ToStringArray()
        {
            string[,] s = new string[M, N];
            for (int i = 0; i < M; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    s[i, j] = this[i, j].ToString();
                }
            }
            return s;
        }

    }
}

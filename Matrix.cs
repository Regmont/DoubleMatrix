using System;

namespace DoubleMatrix
{
    class Matrix
    {
        protected double[,] matrix;
        protected int width, length;

        public Matrix(int width = 0, int length = 0)
        {
            if (width >= 0 && length >= 0)
            {
                this.width = width;
                this.length = length;
            }
            else
            {
                width = length = 0;
            }

            matrix = new double[width, length];
        }

        public void FillRandom(int seed = 0)
        {
            Random rnd = new Random(seed);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    matrix[i, j] = rnd.Next(-100000, 100000) + rnd.NextDouble();
                }
            }
        }
        public double[,] FillMatrix
        {
            set
            {
                width = value.GetLength(1);
                length = value.GetLength(0);

                matrix = new double[width, length];

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        matrix[i, j] = value[i, j];
                    }
                }
            }
        }
        public void Print()
        {
            for (int i = 0; i < width; i++)
            {
                Console.Write("|   ");

                for (int j = 0; j < length; j++)
                {
                    Console.Write(matrix[i, j].ToString("F3").PadLeft(11) + "   ");
                }

                Console.WriteLine("|");
            }
        }
        public void Transpose()
        {
            double[,] tempMatrix = new double[length, width];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    tempMatrix[j, i] = matrix[i, j];
                }
            }

            int temp = width;
            width = length;
            length = temp;
            matrix = tempMatrix;
        }
        void FillAlgCompl(double[,] matr, int y, int x)
        {
            bool flag = false;

            for (int i = 0, i2 = 0; i <= length; i++)
            {
                for (int j = 0, j2 = 0; j <= length; j++)
                {
                    if (!(i == y || j == x))
                    {
                        matrix[i2, j2] = matr[i, j];
                        j2++;
                        flag = true;
                    }
                }

                if (flag)
                {
                    i2++;
                    flag = false;
                }
            }
        }
        public void Inverse()
        {
            double x = 1 / Determinant;

            Transpose();

            double[,] algComplMatrix = new double[width, length];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Matrix tempMatrix = new Matrix(length - 1, length - 1);
                    tempMatrix.FillAlgCompl(matrix, i, j);
                    algComplMatrix[i, j] = tempMatrix.Determinant;

                    if ((i + j) % 2 != 0)
                    {
                        algComplMatrix[i, j] = -algComplMatrix[i, j];
                    }
                }
            }

            matrix = algComplMatrix;
            Matrix temp = this * x;
            this.matrix = temp.matrix;
        }

        public double Determinant
        {
            get
            {
                double[,] temp = new double[length, length];

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        temp[i, j] = matrix[i, j];
                    }
                }

                double x = 0;
                bool flag = true;

                for (int k = 0; k < length - 1; k++)
                {
                    for (int i = 1 + k; i < length; i++)
                    {
                        for (int j = k; j < length; j++)
                        {
                            if (flag)
                            {
                                x = temp[i, k] / temp[k, k];
                                flag = false;
                            }
                            temp[i, j] -= x * temp[k, j];
                        }
                        flag = true;
                    }
                }

                flag = true;
                int y = length - 1;

                for (int k = length - 1; k > 0; k--)
                {
                    for (int i = length - 2 - y + k; i >= 0; i--)
                    {
                        for (int j = length - 1 - y + k; j >= 0; j--)
                        {
                            if (flag)
                            {
                                x = temp[i, k] / temp[k, k];
                                flag = false;
                            }
                            temp[i, j] -= x * temp[k, j];
                        }
                        flag = true;
                    }
                }

                double det = 1;

                for (int i = 0; i < length; i++)
                {
                    det *= temp[i, i];
                }

                return det;
            }
        }

        public static Matrix operator *(Matrix m, double digit)
        {
            Matrix temp = new Matrix(m.width, m.length);

            for (int i = 0; i < temp.width; i++)
            {
                for (int j = 0; j < temp.length; j++)
                {
                    temp.matrix[i, j] = m.matrix[i, j] * digit;
                }
            }

            return temp;
        }
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (!(m1.width == m2.width && m1.length == m2.length))
            {
                return null;
            }
            else
            {
                Matrix sum = new Matrix(m1.width, m1.length);

                for (int i = 0; i < m1.width; i++)
                {
                    for (int j = 0; j < m1.length; j++)
                    {
                        sum.matrix[i, j] = m1.matrix[i, j] + m2.matrix[i, j];
                    }
                }

                return sum;
            }
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return m1 + m2 * (-1);
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.length != m2.width)
            {
                return null;
            }
            else
            {
                Matrix product = new Matrix(m1.width, m2.length);

                for (int i = 0; i < product.width; i++)
                {
                    for (int j = 0; j < product.length; j++)
                    {
                        product.matrix[i, j] = 0;

                        for (int k = 0; k < m1.length; k++)
                        {
                            product.matrix[i, j] += m1.matrix[i, k] * m2.matrix[k, j];
                        }
                    }
                }

                return product;
            }
        }
    }
}
using System;
using System.Globalization;

namespace DoubleMatrix
{
    class Matrix
    {
        protected double[,] matrix;
        protected int width, length;

        public Matrix(int width, int length)
        {
            this.width = width;
            this.length = length;
            matrix = new double[width, length];
        }

        public void FillMatrix()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    bool notOK = true;
                    int top = 0;
                    int left;

                    while (notOK)
                    {
                        Console.Write($"Введите matrix[{i}][{j}] (действительное число от -100000 до 100000): ");

                        top = Console.CursorTop;
                        left = Console.CursorLeft;

                        string str = Console.ReadLine();

                        if (!(double.TryParse(str, out matrix[i, j]) ||
                            double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out matrix[i, j])))
                        {
                            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                            Console.SetCursorPosition(0, top + 1);
                            Console.WriteLine("Ошибка! Вы ввели не число, повторите ввод.");
                            Console.SetCursorPosition(left, top);
                            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                            Console.SetCursorPosition(0, top);
                        }
                        else if (Math.Abs(matrix[i, j]) > 100000)
                        {
                            Console.WriteLine("Ошибка! Число не входит в заданный диапазон! Повторите ввод.");
                            Console.SetCursorPosition(left, top);
                            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                            Console.SetCursorPosition(0, top);
                        }
                        else
                        {
                            notOK = false;
                        }
                    }

                    Console.SetCursorPosition(0, top + 1);
                    Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                    Console.SetCursorPosition(0, top + 1);

                    string formattedNumber = matrix[i, j].ToString("0." + new string('#', 99));
                    matrix[i, j] = double.Parse(formattedNumber);
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
                    if (Math.Abs(matrix[i, j]) < 0.001 && matrix[i, j] != 0)
                    {
                        if (matrix[i, j] > 0)
                        {
                            Console.Write(" ");
                        }
                        Console.Write("  {0:0.000E+0}".PadLeft(11), matrix[i, j]);
                    }
                    else if (Math.Abs(matrix[i, j]) > 99999.999)
                    {
                        Console.Write(" {0:000.000E+0}".PadLeft(11), matrix[i, j]);
                    }
                    else
                    {
                        Console.Write(matrix[i, j].ToString("F3").PadLeft(11));
                    }

                    Console.Write("   ");
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
        public bool Inverse()
        {
            if (Determinant == 0)
            {
                return true;
            }

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
            return false;
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
                                if ((temp[k, k]) == 0) { return 0; }
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
                                if ((temp[k, k]) == 0) { return 0; }
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
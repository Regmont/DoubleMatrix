using System;
using System.Diagnostics;

namespace DoubleMatrix
{
    class Program
    {
        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Matrix matrix = new Matrix(10, 10);

            matrix.FillRandom();
            matrix.Print();
            Console.WriteLine();
            matrix.Inverse();
            matrix.Print();

            stopwatch.Stop();
            Console.WriteLine("Время выполнения: " + stopwatch.ElapsedMilliseconds / 1000f + "с");
        }
    }
}
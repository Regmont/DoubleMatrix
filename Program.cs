using System;
using System.Globalization;

namespace DoubleMatrix
{
    class Program
    {
        static void PrintChoice(int choice)
        {
            Console.SetCursorPosition(0, 7);
            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
            Console.SetCursorPosition(17 * (choice - 1) + (choice - 1), 7);
            Console.Write(new string('#', 17));
            Console.SetCursorPosition(0, 0);
        }
        static void SetSizes(out int size, string type)
        {
            size = 0;
            bool notOK = true;
            int top = 0;
            int left;

            while (notOK)
            {
                Console.Write("Введите количество " + type + " матрицы (от 1 до 10): ");

                top = Console.CursorTop;
                left = Console.CursorLeft;

                if (!int.TryParse(Console.ReadLine(), out size))
                {
                    Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                    Console.SetCursorPosition(0, top + 1);
                    Console.WriteLine("Ошибка! Вы ввели не число, повторите ввод.");
                    Console.SetCursorPosition(left, top);
                    Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                    Console.SetCursorPosition(0, top);
                }
                else if (size < 1 || size > 10)
                {
                    Console.WriteLine("Ошибка! Вы ввели некорректный размер! Повторите ввод.");
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
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(128, 30);
            Console.CursorVisible = false;

            #region Menu
            Console.SetCursorPosition(54, 0);
            Console.Write("Калькулятор матриц");
            Console.SetCursorPosition(0, 2);
            Console.Write(
            "╔═══════════════╗ ╔═══════════════╗ ╔═══════════════╗ ╔═══════════════╗ ╔═══════════════╗ ╔═══════════════╗ ╔═══════════════╗\n" +
            "║    умножить   ║ ║    сложить    ║ ║    вычесть    ║ ║  перемножить  ║ ║     найти     ║ ║транспонировать║ ║     найти     ║\n" +
            "║    матрицу    ║ ║    матрицы    ║ ║    матрицы    ║ ║    матрицы    ║ ║ определитель  ║ ║    матрицу    ║ ║    обратную   ║\n" +
            "║    на число   ║ ║               ║ ║               ║ ║               ║ ║    матрицы    ║ ║               ║ ║    матрицу    ║\n" +
            "╚═══════════════╝ ╚═══════════════╝ ╚═══════════════╝ ╚═══════════════╝ ╚═══════════════╝ ╚═══════════════╝ ╚═══════════════╝\n");
            Console.SetCursorPosition(0, 10);
            Console.Write("Выберите действие! Используйте стрелочки на клавиатуре ← →, чтобы перемещаться. Используйте Enter для ввода.");
            #endregion

            Matrix matrix;
            Matrix matrix2;

            #region Choice
            int choice = 1;
            ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo();

            while(consoleKeyInfo.Key != ConsoleKey.Enter)
            {
                PrintChoice(choice);
                consoleKeyInfo = Console.ReadKey(true);

                if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (choice == 7)
                    {
                        choice = 1;
                    }
                    else
                    {
                        choice++;
                    }
                }

                if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (choice == 1)
                    {
                        choice = 7;
                    }
                    else
                    {
                        choice--;
                    }
                }
            }

            Console.Clear();
            #endregion

            #region FirstMatrix
            int width, length;
            Console.WriteLine("Ввод размеров матрицы");
            Console.CursorVisible = true;
            SetSizes(out width, "строк");
            SetSizes(out length, "столбцов");
            matrix = new Matrix(width, length);
            Console.Clear();
            Console.WriteLine("Ввод матрицы");
            matrix.FillMatrix();

            Console.Clear();
            Console.WriteLine("Ваша матрица");
            matrix.Print();
            #endregion

            #region SecondMatrix
            if (choice > 1 && choice < 5)
            {
                Console.WriteLine("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Ввод размеров второй матрицы");
                SetSizes(out width, "строк");
                SetSizes(out length, "столбцов");
                matrix2 = new Matrix(width, length);
                Console.Clear();
                Console.WriteLine("Ввод второй матрицы");
                matrix2.FillMatrix();

                Console.Clear();
                Console.WriteLine("Ваша матрица");
                matrix2.Print();
            }
            else { matrix2 = new Matrix(0, 0); }

            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Console.Clear();
            #endregion

            switch (choice)
            {
                case 1:
                    bool notOK = true;
                    double x = 0;
                    int top = 0;
                    int left;

                    while (notOK)
                    {
                        Console.Write("Введите действительное число от -100000 до 100000: ");

                        top = Console.CursorTop;
                        left = Console.CursorLeft;

                        string str = Console.ReadLine();
                        if (!(double.TryParse(str, out x) ||
                            double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out x)))
                        {
                            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                            Console.SetCursorPosition(0, top + 1);
                            Console.WriteLine("Ошибка! Вы ввели не число, повторите ввод.");
                            Console.SetCursorPosition(left, top);
                            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                            Console.SetCursorPosition(0, top);
                        }
                        else if (Math.Abs(x) > 100000)
                        {
                            Console.WriteLine("Ошибка! Число не входит в диапазон! Повторите ввод.");
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

                    string formattedNumber = x.ToString("0." + new string('#', 99));
                    x = double.Parse(formattedNumber);

                    matrix *= x;

                    Console.Clear();

                    Console.WriteLine("Произведение исходной матрицы на чило " + x + "\n");
                    matrix.Print();
                    break;
                case 2:
                    try
                    {
                        matrix += matrix2;

                        Console.WriteLine("Сумма исходных матриц\n");
                        matrix.Print();
                    }
                    catch
                    {
                        Console.WriteLine("Нельзя сложить матрицы разных размеров!");
                    }
                    break;
                case 3:
                    try
                    {
                        matrix -= matrix2;

                        Console.WriteLine("Разность исходных матриц\n");
                        matrix.Print();
                    }
                    catch
                    {
                        Console.WriteLine("Нельзя вычесть матрицы разных размеров!");
                    }
                    break;
                case 4:
                    try
                    {
                        matrix *= matrix2;

                        Console.WriteLine("Произведение исходных матриц\n");
                        matrix.Print();
                    }
                    catch
                    {
                        Console.WriteLine("При умножении матриц количество столбцов первой матрицы" +
                            "должно быть равно количеству строк второй матрицы!");
                    }
                    break;
                case 5:
                    try
                    {
                        Console.WriteLine("Определитель данной матрицы\n");
                        Console.WriteLine(matrix.Determinant);
                    }
                    catch
                    {
                        Console.WriteLine("Определитель есть только у квадратной матрицы!");
                    }
                    break;
                case 6:
                    matrix.Transpose();

                    Console.WriteLine("Транспонированная матрица\n");
                    matrix.Print();
                    break;
                case 7:
                    try
                    {
                        bool flag = matrix.Inverse();

                        if (flag)
                        {
                            Console.WriteLine("Невозможно найти обратную матрицу, т.к. определитель матрицы равен нулю");
                        }
                        else
                        {
                            Console.WriteLine("Обратная матрица для исходной\n");
                            matrix.Print();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Чтобы найти обратную матрицу, матрица должна быть квадратной!");
                    }
                    break;
            }

            Console.WriteLine("\n");
        }
    }
}
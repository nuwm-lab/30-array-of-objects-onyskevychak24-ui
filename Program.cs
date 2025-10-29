using System;

namespace EquationApp
{
    // ================= Клас "Квадратне рівняння" =================
    class QuadraticEquation
    {
        protected double b2, b1, b0;

        // Метод задання коефіцієнтів
        public virtual void SetCoefficients()
        {
            Console.Write("Введіть коефіцієнт b2: ");
            b2 = ReadDouble();
            Console.Write("Введіть коефіцієнт b1: ");
            b1 = ReadDouble();
            Console.Write("Введіть коефіцієнт b0: ");
            b0 = ReadDouble();
        }

        // Метод виведення коефіцієнтів
        public virtual void ShowCoefficients()
        {
            Console.WriteLine($"\nКоефіцієнти квадратного рівняння:");
            Console.WriteLine($"b2 = {b2}, b1 = {b1}, b0 = {b0}");
            Console.WriteLine($"Рівняння: {b2}x² + {b1}x + {b0} = 0");
        }

        // Метод перевірки, чи задовольняє число x рівняння
        public virtual bool CheckValue(double x)
        {
            double result = b2 * x * x + b1 * x + b0;
            return Math.Abs(result) < 1e-6;
        }

        // Метод пошуку коренів квадратного рівняння
        public void FindRoots()
        {
            if (Math.Abs(b2) < 1e-6)
            {
                Console.WriteLine("\nЦе не квадратне рівняння (b2 = 0).");
                return;
            }

            double D = b1 * b1 - 4 * b2 * b0;
            Console.WriteLine($"\nДискримінант D = {D:F3}");

            if (D > 0)
            {
                double x1 = (-b1 + Math.Sqrt(D)) / (2 * b2);
                double x2 = (-b1 - Math.Sqrt(D)) / (2 * b2);
                Console.WriteLine($"Два корені: x1 = {x1:F3}, x2 = {x2:F3}");
            }
            else if (Math.Abs(D) < 1e-6)
            {
                double x = -b1 / (2 * b2);
                Console.WriteLine($"Один корінь: x = {x:F3}");
            }
            else
            {
                Console.WriteLine("Дійсних коренів немає.");
            }
        }

        // Допоміжний метод для зчитування числа
        protected double ReadDouble()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double val))
                    return val;
                Console.Write("Помилка! Введіть коректне число: ");
            }
        }
    }

    // ================= Клас "Кубічне рівняння" =================
    class CubicEquation : QuadraticEquation
    {
        protected double a3, a2, a1, a0;

        // Перевантажений метод задання коефіцієнтів
        public override void SetCoefficients()
        {
            Console.Write("Введіть коефіцієнт a3: ");
            a3 = ReadDouble();
            Console.Write("Введіть коефіцієнт a2: ");
            a2 = ReadDouble();
            Console.Write("Введіть коефіцієнт a1: ");
            a1 = ReadDouble();
            Console.Write("Введіть коефіцієнт a0: ");
            a0 = ReadDouble();
        }

        // Перевантажений метод виведення коефіцієнтів
        public override void ShowCoefficients()
        {
            Console.WriteLine($"\nКоефіцієнти кубічного рівняння:");
            Console.WriteLine($"a3 = {a3}, a2 = {a2}, a1 = {a1}, a0 = {a0}");
            Console.WriteLine($"Рівняння: {a3}x³ + {a2}x² + {a1}x + {a0} = 0");
        }

        // Перевантажений метод перевірки, чи задовольняє число x
        public override bool CheckValue(double x)
        {
            double result = a3 * x * x * x + a2 * x * x + a1 * x + a0;
            return Math.Abs(result) < 1e-6;
        }
    }

    // ================= Головна програма =================
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Квадратне рівняння ===");
            QuadraticEquation quad = new QuadraticEquation();
            quad.SetCoefficients();
            quad.ShowCoefficients();
            quad.FindRoots();

            Console.WriteLine("\n=== Кубічне рівняння ===");
            CubicEquation cubic = new CubicEquation();
            cubic.SetCoefficients();
            cubic.ShowCoefficients();

            Console.Write("\nВведіть число x для перевірки кубічного рівняння: ");
            double x;
            while (!double.TryParse(Console.ReadLine(), out x))
            {
                Console.Write("Помилка! Введіть коректне число: ");
            }

            if (cubic.CheckValue(x))
                Console.WriteLine($"Число {x} задовольняє кубічне рівняння.");
            else
                Console.WriteLine($"Число {x} не задовольняє кубічне рівняння.");
        }
    }
}

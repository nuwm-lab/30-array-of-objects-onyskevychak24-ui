using System;

namespace LabWork
{
    // Клас, який моделює матеріальну точку
    class MaterialPoint
    {
        // Поля координат і швидкості
        private double x, y, z;
        private double vx, vy, vz;

        // Конструктор
        public MaterialPoint(double x, double y, double z, double vx, double vy, double vz)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.vx = vx;
            this.vy = vy;
            this.vz = vz;
        }

        // Метод: чи буде точка в першому октанті через час t
        public bool IsInFirstOctantAfter(double t)
        {
            double newX = x + vx * t;
            double newY = y + vy * t;
            double newZ = z + vz * t;
            return newX > 0 && newY > 0 && newZ > 0;
        }

        // Метод: обчислення пройденої відстані за час t
        public double DistanceAfter(double t)
        {
            double dx = vx * t;
            double dy = vy * t;
            double dz = vz * t;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Введіть кількість матеріальних точок: ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m <= 0)
            {
                Console.WriteLine("Помилка: кількість має бути додатним числом.");
                return;
            }

            MaterialPoint[] points = new MaterialPoint[m];
            double[] distances = new double[m];

            Console.Write("\nВведіть час t: ");
            if (!double.TryParse(Console.ReadLine(), out double t))
            {
                Console.WriteLine("Помилка: введено некоректне значення часу.");
                return;
            }

            // Введення даних для кожної точки
            for (int i = 0; i < m; i++)
            {
                Console.WriteLine($"\nМатеріальна точка #{i + 1}:");

                double x = ReadDouble("Введіть x: ");
                double y = ReadDouble("Введіть y: ");
                double z = ReadDouble("Введіть z: ");
                double vx = ReadDouble("Введіть vx: ");
                double vy = ReadDouble("Введіть vy: ");
                double vz = ReadDouble("Введіть vz: ");

                points[i] = new MaterialPoint(x, y, z, vx, vy, vz);
                distances[i] = points[i].DistanceAfter(t);

                Console.WriteLine($"Пройдена відстань за час {t}: {distances[i]:F3}");
                Console.WriteLine(points[i].IsInFirstOctantAfter(t)
                    ? "→ Точка буде у першому октанті."
                    : "→ Точка не буде у першому октанті.");
            }

            // Знаходження точки з найбільшою пройденою відстанню
            double maxDistance = distances[0];
            int maxIndex = 0;
            for (int i = 1; i < m; i++)
            {
                if (distances[i] > maxDistance)
                {
                    maxDistance = distances[i];
                    maxIndex = i;
                }
            }

            Console.WriteLine($"\nНайбільша відстань = {maxDistance:F3}");
            Console.WriteLine($"Номер точки з найбільшою відстанню: {maxIndex + 1}");
        }

        // Безпечне зчитування double
        static double ReadDouble(string message)
        {
            double value;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Помилка: введіть коректне число.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;

namespace PhysicsSimulation
{
    /// <summary>
    /// Клас, що описує матеріальну точку у тривимірному просторі.
    /// </summary>
    public class MaterialPoint
    {
        // Приватні поля (інкапсуляція)
        private double _x, _y, _z;
        private double _vx, _vy, _vz;

        // Публічні властивості для читання (тільки get)
        public double PositionX => _x;
        public double PositionY => _y;
        public double PositionZ => _z;

        public double VelocityX => _vx;
        public double VelocityY => _vy;
        public double VelocityZ => _vz;

        /// <summary>
        /// Конструктор для ініціалізації координат і швидкості.
        /// </summary>
        public MaterialPoint(double x, double y, double z, double vx, double vy, double vz)
        {
            _x = x;
            _y = y;
            _z = z;
            _vx = vx;
            _vy = vy;
            _vz = vz;
        }

        /// <summary>
        /// Обчислює нову позицію точки через час t.
        /// </summary>
        public (double x, double y, double z) GetPositionAfter(double t)
        {
            double newX = _x + _vx * t;
            double newY = _y + _vy * t;
            double newZ = _z + _vz * t;
            return (newX, newY, newZ);
        }

        /// <summary>
        /// Перевіряє, чи знаходиться точка у першому октанті після часу t.
        /// </summary>
        public bool IsInFirstOctantAfter(double t)
        {
            var (newX, newY, newZ) = GetPositionAfter(t);
            return newX > 0 && newY > 0 && newZ > 0;
        }

        /// <summary>
        /// Повертає пройдену відстань за час t.
        /// </summary>
        public double GetDistanceAfter(double t)
        {
            double speedMagnitude = Math.Sqrt(_vx * _vx + _vy * _vy + _vz * _vz);
            return speedMagnitude * t;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Рух матеріальних точок у просторі ===");

            int n = ReadInt("Введіть кількість точок: ", min: 1);
            double t = ReadDouble("Введіть час t (секунди): ", min: 0);

            var points = new List<MaterialPoint>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nТочка #{i + 1}:");

                double x = ReadDouble("  Введіть координату x: ");
                double y = ReadDouble("  Введіть координату y: ");
                double z = ReadDouble("  Введіть координату z: ");

                double vx = ReadDouble("  Введіть швидкість vx: ");
                double vy = ReadDouble("  Введіть швидкість vy: ");
                double vz = ReadDouble("  Введіть швидкість vz: ");

                points.Add(new MaterialPoint(x, y, z, vx, vy, vz));
            }

            Console.WriteLine("\n=== Результати ===");

            double maxDistance = double.MinValue;
            int maxIndex = -1;

            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];
                var (xAfter, yAfter, zAfter) = point.GetPositionAfter(t);
                double distance = point.GetDistanceAfter(t);
                bool inFirstOctant = point.IsInFirstOctantAfter(t);

                Console.WriteLine($"\nТочка #{i + 1}:");
                Console.WriteLine($"  Позиція після {t} c: ({xAfter:F2}, {yAfter:F2}, {zAfter:F2})");
                Console.WriteLine($"  Пройдена відстань: {distance:F2}");
                Console.WriteLine($"  У першому октанті: {(inFirstOctant ? "Так" : "Ні")}");

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxIndex = i;
                }
            }

            if (maxIndex >= 0)
            {
                Console.WriteLine($"\nТочка #{maxIndex + 1} пройшла найбільшу відстань: {maxDistance:F2}");
            }

            Console.WriteLine("\nРоботу завершено. Натисніть Enter для виходу...");
            Console.ReadLine();
        }

        // ==== Допоміжні методи для безпечного вводу ====

        private static int ReadInt(string message, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= min && value <= max)
                    return value;

                Console.WriteLine($"Помилка! Введіть ціле число від {min} до {max}.");
            }
        }

        private static double ReadDouble(string message, double min = double.NegativeInfinity, double max = double.PositiveInfinity)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                    && value >= min && value <= max)
                    return value;

                Console.WriteLine($"Помилка! Введіть дійсне число у діапазоні [{min}, {max}].");
            }
        }
    }
}

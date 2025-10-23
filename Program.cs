using System;

class Sum
{
    private double a;
    private int n;
//lkjlkjlkjlk
    // Конструктор класу
    public Sum(double a, int n)
    {
        this.a = a;
        this.n = n;
    }
    ~Sum()
    {
        Console.WriteLine("Деструктор викликано");
    }

    // Метод для обчислення суми
    public double Calculate()
    {
        double s = 0;
        for (int i = 1; i <= n; i++)
        {
            s += a / (i * (i + 1));
        }
        return s;
    }
}
class Program
{
    static void Main()
    {
        Console.Write("Введіть кількість об'єктів: ");
        int m = Convert.ToInt32(Console.ReadLine());

        Sum[] arr = new Sum[m];
        double[] results = new double[m];

        // Введення даних для кожного об’єкта
        for (int i = 0; i < m; i++)
        {
            Console.WriteLine($"\nОб'єкт #{i + 1}:");
            Console.Write("Введіть a: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            arr[i] = new Sum(a, n);
            results[i] = arr[i].Calculate();

            Console.WriteLine($"Сума для об'єкта #{i + 1} = {results[i]}");
        }

        // Знаходження найбільшої суми
        double maxSum = results[0];
        int maxIndex = 0;

        for (int i = 1; i < m; i++)
        {
            if (results[i] > maxSum)
            {
                maxSum = results[i];
                maxIndex = i;
            }
        }

        Console.WriteLine($"\nНайбільша сума = {maxSum}");
        Console.WriteLine($"Номер об'єкта з найбільшою сумою: {maxIndex + 1}");
    }
}

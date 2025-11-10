using System;
using System.Drawing;

class Calculator // сделать так, чтобы я выводил операцию, которая необходима именно сейчас
{
    public void Add(int x, int y)
    {
        int z = x + y;
        Console.WriteLine($"Сумма {x} и {y} равна {z}");
        
        z = x - y ;
        Console.WriteLine($"Разность {x} и {y} равна {z}");
        
        z = x * y;
        Console.WriteLine($"Произведение {x} и {y} равно {z}");
        
        if (y != 0)
        {
            double z_div = (double)x / y;
            Console.WriteLine($"Деление {x} на {y} равно {z_div}");
        }
        else
        {
            Console.WriteLine("Деление на ноль нельзя.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        do {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Введите первое число:");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите второе число:");
            int num2 = int.Parse(Console.ReadLine());
            Calculator calc = new Calculator();

            calc.Add(num1, num2);
        } while (Console.ReadKey().Key != ConsoleKey.Escape);
    }
}


using System;

enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Power,
    Sqrt
}

class Program
{
    static void Main()
    {

		double a = ReadDouble("Введите a: ");
		double b = ReadDouble("Введите b: ");

		// Выводим результаты сразу по всем операциям
		foreach (Operation operation in Enum.GetValues(typeof(Operation)))
		{
			switch (operation)
			{
				case Operation.Add:
					Console.WriteLine($"a + b = {a + b}");
					break;
				case Operation.Subtract:
					Console.WriteLine($"a - b = {a - b}");
					break;
				case Operation.Multiply:
					Console.WriteLine($"a * b = {a * b}");
					break;
				case Operation.Divide:
					if (b == 0)
					{
						Console.WriteLine("a / b = ошибка (деление на ноль)");
					}
					else
					{
						Console.WriteLine($"a / b = {a / b}");
					}
					break;
				case Operation.Power:
					Console.WriteLine($"a ^ b = {Math.Pow(a, b)}");
					break;
				case Operation.Sqrt:
					if (a < 0)
					{
						Console.WriteLine("sqrt(a) = ошибка (a < 0)");
					}
					else
					{
						Console.WriteLine($"sqrt(a) = {Math.Sqrt(a)}");
					}
					break;
			}
		}
    }

    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
			string input = Console.ReadLine();
			if (input == null || input.Trim() == "")
            {
                Console.WriteLine("Введите число.");
                continue;
            }

			double value;
			bool ok = double.TryParse(input, out value);
			if (ok)
			{
				return value;
			}
            Console.WriteLine("Некорректное число. Попробуйте ещё раз.");
        }
    }
}
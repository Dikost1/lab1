namespace ClassStackApp
{
using System;
using System.Collections.Generic; // нужно, чтобы работал List<T>
using System.Linq;


class Stack
{
    private List<int> elements = new List<int>();

    public static Stack operator +(Stack stack, int value)
    {
        stack.elements.Add(value);
        return stack;
    }
    public void Print()
    {
        Console.WriteLine( "Содержимое стека: " + string.Join(", ", elements));
    }

    public int this [int index]
    {
        get
        {
            if (index < 0 || index >= elements.Count)
                throw new IndexOutOfRangeException();

            int real = elements.Count - 1 - index;
            return elements[real];
        }
        set
        {
            if (index < 0 || index >= elements.Count)
                throw new IndexOutOfRangeException();

            int real = elements.Count - 1 - index;
            elements[real] = value;
        }
    }
    
    public static bool operator true(Stack stack) 
    {
        return stack.elements.Count == 0;
    }

    public static bool operator false(Stack stack) 
    {
        return stack.elements.Count != 0;
    }
    public static bool operator >(Stack A, Stack B)
    {
        if (A == null || B == null)
        {
            return false;
        }
        List<int> temp = new List<int>(A.elements);
        temp.Sort();
        temp.Reverse();
        B.elements.Clear();
        B.elements.AddRange(temp);
        return true;
    }

    public static bool operator <(Stack A, Stack B)
    {
        if (A == null || B == null)
        {
            return false;
        }
        return A.elements.Count < B.elements.Count;
    }
    public void PrintState()
    {
        if (this)
        {
            Console.WriteLine("Стек пуст");
        }
        else
        {
            Console.WriteLine("Стек не пуст");
        }
    }
    public static Stack operator --(Stack stack)
    {
        if (stack.elements.Count == 0)
        {
            Console.WriteLine("Стек пуст — удалить нечего");
            return stack;
        }

        int removed = stack.elements[stack.elements.Count - 1];
        stack.elements.RemoveAt(stack.elements.Count - 1);
        Console.WriteLine($"Элемент {removed} удалён");
        return stack;
    }
public int Count => elements.Count;
public IReadOnlyList<int> Items => elements.AsReadOnly();
}

static class Extensions 
    {
       public static int CountSentences(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return 0;

        int count = 0;
        bool inEnd = false;
        foreach (char c in s)
        {
            if (c == '.' || c == '!' || c == '?')
            {
                if (!inEnd)
                {
                    count++;
                    inEnd = true;
                }
            }
            else if (!char.IsWhiteSpace(c))
            {
                inEnd = false;
            }
        }
        return count;
    }
       
    public static double AverageElement(this Stack stack)
        {
            if (stack == null || stack.Count == 0)
                return double.NaN;

            double sum = 0;
            foreach (int x in stack.Items)
                sum += x;

            return sum / stack.Count;
        }
    }
class Program
{
    static void Main()
    {
        Stack stack = new Stack();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Меню:");
            Console.WriteLine("1) Добавить элемент в стек");
            Console.WriteLine("2) Удалить элемент со стека (--)");
            Console.WriteLine("3) Показать стек");
            Console.WriteLine("4) Проверить состояние стека");
            Console.WriteLine("5) Показать среднее значение элементов");
            Console.WriteLine("6) Посчитать количество предложений в тексте");
            Console.WriteLine("0) Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите число: ");
                    if (int.TryParse(Console.ReadLine(), out int val))
                    {
                        stack = stack + val;
                        Console.WriteLine("Добавлено.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод.");
                    }
                    break;

                case "2":
                    stack--;
                    break;

                case "3":
                    stack.Print();
                    break;

                case "4":
                    stack.PrintState();
                    break;

                case "5":
                    double avg = stack.AverageElement();
                    if (double.IsNaN(avg))
                        Console.WriteLine("Стек пуст — среднее не определено");
                    else
                        Console.WriteLine($"Среднее: {avg:F2}");
                    break;

                case "6":
                    Console.Write("Введите текст: ");
                    string text = Console.ReadLine();
                    Console.WriteLine($"Предложений в тексте: {text.CountSentences()}");
                    break;

                case "0":
                    Console.WriteLine("Выход...");
                    return;

                default:
                    Console.WriteLine("Нет такого пункта.");
                    break;
            }
        }
    }
}
}
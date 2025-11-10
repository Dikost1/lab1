using System;


// задание 1
public class Program1
{
    public static void Main()
    {
        string line = Console.ReadLine();
        int n = int.Parse(line);

        int[] arr = new int[n];

        for (int i = 0; i < n; i++)
        {
            arr[i] = i + 1;
        }

        Array.Reverse(arr);
        Console.WriteLine("Массив в обратном порядке:");
        foreach (int x in arr)
        {
            Console.Write(x + " ");
        }
    }
}


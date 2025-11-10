using System;

public class Program4
{
    public static void Main()
    {
        Console.Write("Введите количество строк n: ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Введите количество столбцов m: ");
        int m = int.Parse(Console.ReadLine());

        int[,] arr = new int[n, m];

        int value = 1;
        int top = 0;
        int bottom = n - 1;
        int left = 0;
        int right = m - 1;

        while (top <= bottom && left <= right)
        {
            // слева направо
            for (int j = left; j <= right; j++)
                arr[top, j] = value++;
            top++;

            // сверху вниз
            for (int i = top; i <= bottom; i++)
                arr[i, right] = value++;
            right--;

            if (top <= bottom)
            {
                // справа налево
                for (int j = right; j >= left; j--)
                    arr[bottom, j] = value++;
                bottom--;
            }

            if (left <= right)
            {
                // снизу вверх
                for (int i = bottom; i >= top; i--)
                    arr[i, left] = value++;
                left++;
            }
        }

        // вывод массива
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write(arr[i, j].ToString().PadLeft(4));
            }
            Console.WriteLine();
        }
    }
}

using System;

public class Program3
{
    public static void Main()
    {
        int n = 5; // размер квадратного массива
        int[,] arr = new int[n, n];

        int value = 1;      // число, которое будем ставить
        int top = 0;        // верхняя граница
        int bottom = n - 1; // нижняя граница
        int left = 0;       // левая граница
        int right = n - 1;  // правая граница

        while (value <= n * n)
        {
            // слева направо
            for (int j = left; j <= right; j++)
                arr[top, j] = value++;
            top++;

            // сверху вниз
            for (int i = top; i <= bottom; i++)
                arr[i, right] = value++;
            right--;

            // справа налево
            for (int j = right; j >= left; j--)
                arr[bottom, j] = value++;
            bottom--;

            // снизу вверх
            for (int i = bottom; i >= top; i--)
                arr[i, left] = value++;
            left++;
        }

        // вывод
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(arr[i, j].ToString().PadLeft(3));
            }
            Console.WriteLine();
        }
    }
}
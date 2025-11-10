using System;

public class Program2
{
    public static void Main()
    {
        int n = 5; // размер квадратного массива
        int[,] arr = new int[n, n];

        // заполняем массив
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                arr[i, j] = 1;

        // Поставим нули в нижнем левом углу в виде треугольника высоты 3
        int triangleHeight = 3; 
        for (int i = n - triangleHeight; i < n; i++)
        {
            
            int maxColZero = i - (n - triangleHeight);
            for (int j = 0; j <= maxColZero; j++)
            {
                arr[i, j] = 0;
            }
        }

        // выводим массив
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(arr[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
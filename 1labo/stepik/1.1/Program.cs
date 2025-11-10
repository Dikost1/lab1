using System;

public class MainClass
{
    public static void Main()
    {
        string line = Console.ReadLine(); // ввод числа в десятеричной системе 

        int x = int.Parse(line);

         string answer = "";
        int temp = x;
        while (temp > 0)
        {
            int remainder = temp % 16;
            if (remainder < 10)
                answer = remainder + answer;
            else
                answer = (char)('A' + remainder - 10) + answer;
            temp /= 16;
        }

        Console.WriteLine(answer);
    }
}
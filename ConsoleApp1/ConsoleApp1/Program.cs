using System;

class Program
{
    static void Main(string[] args)
    {
        int n = 100; // Максимальное загадываемое число
        int k = 10; // Количество попыток

        Random random = new Random();
        int secretNumber = random.Next(1, n + 1);

        Console.WriteLine("Компьютер загадал число от 1 до " + n + ".");

        for (int i = 1; i <= k; i++)
        {
            Console.Write("Попытка " + i + ": ");
            int guess = int.Parse(Console.ReadLine());

            if (guess == secretNumber)
            {
                Console.WriteLine("Вы угадали!");
                return;
            }
            else if (guess < secretNumber)
            {
                Console.WriteLine("Загаданное число больше.");
            }
            else
            {
                Console.WriteLine("Загаданное число меньше.");
            }
        }

        Console.WriteLine("Попытки закончились. Загаданное число было " + secretNumber + ".");
    }
}

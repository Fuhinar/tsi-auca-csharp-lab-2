using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите коэффициенты уравнения (разделяйте пробелом):");
        double[] coefficients;
        while (true)
        {
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');
            coefficients = new double[parts.Length];
            bool parseSuccess = true;
            for (int i = 0; i < parts.Length; i++)
            {
                if (!double.TryParse(parts[i], out coefficients[i]))
                {
                    Console.WriteLine("Некорректный ввод, попробуйте снова:");
                    parseSuccess = false;
                    break;
                }
            }
            if (parseSuccess) break;
        }

        try
        {
            Polynomial polynomial = Equations.CreateEquation(coefficients);
            Complex[] roots = polynomial.FindRoots();
            Console.WriteLine("Корни уравнения:");
            foreach (var root in roots)
            {
                Console.WriteLine(root);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
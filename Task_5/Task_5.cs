using System;

namespace Task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input x: "); double x = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input y: "); double y = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input z: "); double z = Convert.ToDouble(Console.ReadLine());
            if(x * z + 2 == 0)
            {
                Console.WriteLine("Divide by zero!");
            } else if(x < 0 && y < 1 && y > -1 && y != 0)
            {
                Console.WriteLine("raising a negative number to a non-integer power!");
            } else
            {
                double a = Math.Tan(Math.Pow(x, -y) + z / (y * y * 1.0 + 1) + Math.Pow(y / (x * z * 1.0 + 2), 1 / 3.0));
                double b = a / (Math.Sin((x + y * Math.PI) / z));
                Console.WriteLine($"a = {a}\nb = {b}");
            }
        }
    }
}

using System;

namespace Task_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter year: "); int year = Convert.ToInt32(Console.ReadLine());

            int c = year / 100; int y = year - c * 100;
            int day = 31;
            while (((day + y + y / 4 + c / 4 - 2 * c + 20) % 7) != 0)
                day--;

            Console.WriteLine("Перехiд на зимовий час здiйсниться " + day + "го Жовтня!");
        }
    }
}

using System;

namespace Lab_6
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Interface();
            }
        }
        static void Interface()
        {
            Console.WriteLine("Оберіть дію:\n1 - Ввести довiльне число\n0 - контрольний приклад(з числом 98765)");
            try
            {
                int act = Convert.ToInt32(Console.ReadLine());
                if(act == 0)
                {
                    Console.WriteLine("Число - " + 98765);
                    CountPolyndrom(98765);
                    return;
                }
                else if (act != 1)
                {
                    Console.WriteLine("Обраної дії не існує\n(Enter для продовження)");
                    Console.ReadLine();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Обраної дії не існує\n(Enter для продовження)");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Введiть цiле додатнє число");
            long number;
            try
            {
                number = Convert.ToInt64(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ви ввели не число! \nEnter для продовження...");
                Console.ReadLine();
                return;
            }
            if (number < 0)
            {
                Console.WriteLine("Ви ввели не додатнє число! \nEnter для продовження...");
                Console.ReadLine();
                return;
            }
            CountPolyndrom(number);
        }
        static void CountPolyndrom(long number)
        {
            Deque deque = new Deque();
            long num = number;
            while (num != 0)
            {
                int temp = (int)(num % 10);
                num /= 10;
                deque.insertHead(temp);
            }
            while (deque.Tail != deque.Head && deque.Tail != null)
            {
                if (deque.tail() == deque.head())
                {
                    deque.removeTail();
                    deque.removeHead();
                }
                else
                {
                    MakePolynd(number);
                    return;
                }
            }
            Console.WriteLine("Число є полiндромом!");
            Console.WriteLine("Enter для продовження...");
            Console.ReadLine();
            return;
        }
        static void MakePolynd(long num)
        {
            Deque deque = new Deque();
            while (num != 0)
            {
                int temp = (int)(num % 10);
                num /= 10;
                deque.insertHead(temp);
                deque.insertTail(temp);
            }
            Console.WriteLine("Число не є полiндромом! \nПеретворимо його у полiндром шляхом послiдовного " +
                "\nдодавання цифр iнвертованого числа у хвiст деку:");
            while (deque.Head != null)
            {
                Console.Write(deque.head());
                deque.removeHead();
            }
            Console.WriteLine("\nEnter для продовження...");
            Console.ReadLine();
        }
    }
}
using System;

namespace lab4
{
    class DLNode
    {
        public DLNode Prev { get; set; }
        public DLNode Next { get; set; }
        public int data;
        public DLNode(int data)
        {
            this.data = data;
        }
    }
    class List
    {
        public DLNode head;
        public List()
        {
            this.head = null;
        }
        public bool AddFirst(int data)
        {
            if (head == null)
            {
                head = new DLNode(data);
                Print();
                return true;
            } 
            else
            {
                DLNode toAdd = new DLNode(data);
                toAdd.Next = head;
                toAdd.Prev = head.Prev;
                head.Prev.Next = toAdd;
                head.Prev = toAdd;
                head = toAdd;
                Print();
                return true;
            }
        }
        public bool AddLast(int data)
        {
            if (head == null)
            {
                AddFirst(data);
                return true;
            }
            if (head.Next == null)
            {
                DLNode toAddSec = new DLNode(data);
                head.Next = toAddSec;
                toAddSec.Next = head;
                head.Prev = toAddSec;
                toAddSec.Prev = head;
                Print();
                return true;
            }
            DLNode current = head;
            while (current.Next != head)
            {
                current = current.Next;
            }
            DLNode toAdd = new DLNode(data);
            toAdd.Prev = current;
            toAdd.Next = head;
            current.Next = toAdd;
            head.Prev = toAdd;
            Print();
            return true;
        }
        public bool AddAtPosition(int data, int pos)
        {
            if (head == null && pos == 1)
            {
                AddFirst(data);
                return true;
            }
            if (head == null && pos != 1)
            {
                return false;
            }
            DLNode current = head;
            int counter = 1;
            while (counter < pos)
            {
                if (current.Next == head && counter < pos)
                {
                    return false;
                }
                current = current.Next;
                counter++;
            }
            DLNode toAdd = new DLNode(data);
            if (current.Next != head)
            {
                toAdd.Next = current.Next;
                current.Next.Prev = toAdd;
                toAdd.Prev = current;
                current.Next = toAdd;
                Print();
                return true;
            }
            else
            {
                toAdd.Prev = current;
                current.Next = toAdd;
                Print();
                return true;
            }
            
        }
        public bool DeleteFirst()
        {
            if (head != null && head.Next != null)
            {
                head.Prev.Next = head.Next;
                head.Next.Prev = head.Prev;
                head = head.Next;
                return true;
            }
            if (head != null && head.Next == null) { head = null; return true; }
            else
            {
                return false;
            }
        }
        public bool DeleteLast()
        {
            if (head == null)
            {
                return false;
            }
            if (head.Next == null)
            {
                DeleteFirst();
            }
            DLNode current = head;
            while(current.Next.Next != head)
            {
                current = current.Next;
            }
            head.Prev = current;
            current.Next = head;
            return true;
        }
        public bool DeleteAtPosition(int pos)
        {
            if (head == null)
            {
                return false;
            }
            if (head.Next == null && pos == 1)
            {
                DeleteFirst();
            }
            DLNode current = head;
            for (int i = 0; i < pos; i++)
            {
                current = current.Next;
            }
            current.Next.Prev = current.Prev;
            current.Prev.Next = current.Next;
            return true;
        }
        public void Print()
        {
            if (head == null)
            {
                Console.WriteLine("List is empty.");
            }
            else
            {
                DLNode current = head;
                string s = "";
                if (current.Next == null)
                {
                    Console.WriteLine(head.data);
                    return;
                }
                do
                {
                    s += current.data + ", ";
                    current = current.Next;
                } while (current != head);
                Console.WriteLine(s);
            }
        }
        public void FindMinEven()
        {
            if (head == null)
            {
                return;
            }
            if (head.Next == null && head.data % 2 == 0)
            {
                AddAtPosition(0, 1);
                return;
            }
            if (head.Next == null)
            {
                AddLast(0);
                return;
            }
            DLNode current = head;

            DLNode value = null;

            do
            {
                if (value == null && current.data % 2 == 0)
                {
                    value = current;
                }
                if (value != null && current.data < value.data && current.data % 2 == 0)
                {
                    value = current;
                }
                current = current.Next;
            } while (current != head);
            DLNode addFlag = new DLNode(0);
            addFlag.Prev = value;
            addFlag.Next = value.Next;
            value.Next = addFlag;
            addFlag.Next.Prev = addFlag;
            Print();
            return;
        }
    }

    class Program
    {
        static Random rnd = new Random();

        static void Interface(List list)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------\n" +
                              "----------------------List----------------------");
            list.Print();
            Console.WriteLine("Оберiть дiю (число вiд одного до семи):\n" +
                "1 - Додати перший елемент (AddFirst)\n" +
                "2 - Додати останнiй елемент (AddLast)\n" +
                "3 - Додати елемент на позицiї (AddAtPosition)\n" +
                "4 - Видалити перший елемент (DeleteFirst)\n" +
                "5 - Видалити останнiй елемент (DeleteLast)\n" +
                "6 - Видалити елемент з позицiї (DeleteAtPosition)\n" +
                "7 - Знайти мiнiмальний додатнiй елемент (FindMinEven)\n");
            string change = Console.ReadLine();
            switch (change)
            {
                case "1":
                    Console.WriteLine("Введiть значення елементу:");
                    int data = Convert.ToInt32(Console.ReadLine());
                    list.AddFirst(data);
                    return;
                case "2":
                    Console.WriteLine("Введiть значення елементу:");
                    data = Convert.ToInt32(Console.ReadLine());
                    list.AddLast(data);
                    return;
                case "3":
                    Console.WriteLine("Введiть значення елементу:");
                    data = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введiть позицiю:");
                    int pos = Convert.ToInt32(Console.ReadLine());
                    list.AddAtPosition(data, pos);
                    return;
                case "4":
                    list.DeleteFirst();
                    return;
                case "5":
                    list.DeleteLast();
                    return;
                case "6":
                    Console.WriteLine("Введiть позицiю:");
                    pos = Convert.ToInt32(Console.ReadLine());
                    list.DeleteAtPosition(pos);
                    return;
                case "7":
                    list.FindMinEven();
                    return;
            }
        }

        static void Main(string[] args)
        {
            List list = new List();
            for (int i = 1; i < 10; i++)
            {
                list.AddLast(rnd.Next(1, 100));
            }
            while (true)
            {
                Interface(list);
            }
        }
    }
}

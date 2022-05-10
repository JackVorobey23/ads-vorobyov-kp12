namespace Lab_5
{
    public static class Program
    {
        static void Main(string[] Args)
        {
            while (true)
            {
                Interface();
            }
        }
        static Random rnd = new Random();
        static int M;
        static void Interface()
        {
            int typeOfInout;
            Console.WriteLine("Оберiть тип заповнення масиву:\n1 - вручну\n2 - випадково");
            try
            {
                typeOfInout = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Ви ввели не число!"); System.Threading.Thread.Sleep(1000);
                Console.Clear();
                return;
            }
            if (typeOfInout != 1 && typeOfInout != 2)
            {
                Console.WriteLine("Ви ввели неiснуючий тип заповнення масиву!"); System.Threading.Thread.Sleep(1000);
                Console.Clear();
                return;
            }
            Console.Clear();
            Console.Write("Оберiть розмiр масиву\nМ = ");
            try
            {
                M = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Розмiр массиву повинен бути числом!"); System.Threading.Thread.Sleep(1000);
                Console.Clear();
                return;
            }
            Console.Clear();
            int[,] arr = new int[M, M];
            if (typeOfInout == 1)
            {
                for (int i = 0; i < M; i++)
                {
                    Console.WriteLine($"Запишiть елементи першого рядка через кому(,) Кiлькiсть елементiв - {M}");
                    string elementsOfRow = Console.ReadLine();
                    for (int j = 0; j < M; j++)
                    {
                        try
                        {
                            arr[i, j] = Convert.ToInt32(elementsOfRow.Split(',')[j]);
                        }
                        catch
                        {
                            Console.WriteLine("Ви ввели неправильну кiлькiсть елементiв!"); System.Threading.Thread.Sleep(1000);
                            Console.Clear();
                            return;
                        }
                    }
                }
            }
            else if (typeOfInout == 2)
            {
                arr = GenerRandomList(M);
            }
            while (true)
            {
                Console.WriteLine("1 - вивести масив, 2 - перейти до виконання алгоритму");
                int act = 0;
                try
                {
                    act = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Номер дiї повинен бути цiлим числом!"); System.Threading.Thread.Sleep(1000);
                }
                if (act == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Масив чисел:");
                    for (int i = 0; i < M; i++)
                    {
                        for (int j = 0; j < M; j++)
                        {
                            Console.Write(String.Format("{0,5}", arr[i, j]));
                        }
                        Console.WriteLine();
                    }
                } else if (act == 2)
                {
                    Sort(arr);
                    return;
                }
                else
                {
                    Console.WriteLine("Номер дiї введено неправильно!"); System.Threading.Thread.Sleep(1000);
                }
            }
        }
        public static void Sort(int[,] array)
        {
            List<int> SortList = new List<int>();
            bool direction = true;
            int c = 1;
            while (M - c > c)
            {
                if (direction)
                {
                    for (int i = c; i < M - c; i++)
                        SortList.Add(array[i, c - 1]);
                }
                else
                {
                    for (int i = M - c - 1; i > c - 1; i--)
                        SortList.Add(array[i, c - 1]);
                }
                direction = !direction;
                c++;
            }
            c = 1;
            direction = true;
            SortList = QuickSort(SortList);
            SortList = InsertionSort(SortList);
            int counter = 0;
            while (M - c > c)
            {
                if (direction)
                    for (int i = c; i < M - c; i++)
                    {
                        array[i, c - 1] = SortList[counter];
                        counter++;
                    }
                else
                    for (int i = M - c - 1; i > c - 1; i--)
                    {
                        array[i, c - 1] = SortList[counter];
                        counter++;
                    }
                direction = !direction;
                c++;
            }
            counter = 0;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if(j < counter && j < M - 1 - counter)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(String.Format("{0,5}", array[i, j]));
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(String.Format("{0,5}", array[i, j]));
                    }
                }
                Console.WriteLine();
                counter++;
            }
            Console.WriteLine("\nЩоб повернутися до початку натиснiть \"Enter\"");
            Console.ReadLine();
            Console.Clear();
        }
        static List<int> QuickSort(List<int> array)
        {
            return QuickSort(array, 0, array.Count - 1);
        }
        static List<int> QuickSort(List<int> array, int minIndex, int maxIndex)
        {
            if(maxIndex - minIndex < M) return array;
            if (minIndex < maxIndex)
            {
                var pivotIndex = Partition(array, minIndex, maxIndex);
                QuickSort(array, minIndex, pivotIndex);
                QuickSort(array, pivotIndex + 1, maxIndex);
            }

            return array;
        }
        static List<int> InsertionSort(List<int> array)
        {
            for (var i = 1; i < array.Count; i++)
            {
                var key = array[i];
                var j = i;
                while ((j > 1) && (array[j - 1] > key))
                {
                    (array[j - 1], array[j]) = (array[j], array[j - 1]);
                    j--;
                }

                array[j] = key;
            }
            return array;
        }
        static (int,int) Mediana(List<int> array, int minIndex, int maxIndex)
        {
            int a = array[minIndex];
            int b = array[(minIndex + maxIndex) / 2];
            int c = array[maxIndex];
            if(a > b)
            {
                if (c > a) return (a, minIndex);
                if (b > c) return (b, (minIndex + maxIndex) / 2);
                return (c, maxIndex);
            }
            if(b > a)
            {
                if (c > b) return (b, (minIndex + maxIndex) / 2);
                if (a > c) return (a, minIndex);
                return (c, maxIndex);
            }
            return (a, minIndex);
        }
        static int Partition(List<int> array, int minIndex, int maxIndex)
        {
            (int pivot, int pivIndex) = Mediana(array, minIndex, maxIndex);
            (array[pivIndex], array[maxIndex]) =
                (array[maxIndex], array[pivIndex]);
            int j = maxIndex - 1;
            int i = minIndex;
            while(i < j)
            {
                if (array[i] >= pivot && array[j] <= pivot)
                    (array[i], array[j]) = (array[j], array[i]);
                if (array[i] < pivot)
                    i++;
                if(array[j] > pivot)
                    j--;
            }
            (array[i], array[maxIndex]) = (array[maxIndex], array[i]);
            return i;
        }
        static int[,] GenerRandomList(int M)
        {
            int[,] res = new int[M, M];

            int i = rnd.Next(-1 * (int)Math.Pow(M, 2), 0);

            for (int t = 0; t < M; t++)
            {
                for (int k = 0; k < M; k++)
                {
                    res[t, k] = i;
                    i += rnd.Next(1, 4);
                }
            }
            for (int j = 0; j < M * 25; j++)
            {
                int pos1X = rnd.Next(0, M); int pos1Y = rnd.Next(0, M);
                int pos2X = rnd.Next(0, M); int pos2Y = rnd.Next(0, M);
                (res[pos1X, pos1Y], res[pos2X, pos2Y]) =
                    (res[pos2X, pos2Y], res[pos1X, pos1Y]);
            }
            return res;
        }
    }
}
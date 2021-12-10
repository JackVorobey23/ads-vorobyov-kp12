using System;
using System.Collections.Generic;

namespace Lab_03
{
    class Program
    {
        static void Main(string[] args)
        { 
            Console.Write("Input N:"); int N = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input M:"); int M = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input K:"); int K = Convert.ToInt32(Console.ReadLine());
            int[,] matrix = GenerateMatrix(N, M);
            (int[] condMatrix, bool[,] colorPlaces) = CountElems(matrix, N, M, K);
            int[] res = InsertBin(condMatrix);
            ReplaceElem(matrix, res, colorPlaces, N, M);
            PrintMatrix(matrix, colorPlaces, N, M);
        }
        static void ReplaceElem(int[,] matrix, int[] res, bool[,] flag, int n, int m)
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (flag[i, j])
                    {
                        matrix[i, j] = res[count];
                        count++;
                    }
                }
            }
        }
        static void PrintMatrix(int[,] matrix, bool[,] flag, int n, int m)
        {
            Console.WriteLine("\n");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (flag[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write($"{matrix[i,j]}; ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                        Console.Write($"{matrix[i, j]}; ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }
        static (int[], bool[,]) CountElems(int[,] matrix, int n, int m, int k)
        {
            int[] res = { 0 };
            bool[,] flag = new bool[n,m];
            List<int> data = new List<int>();
            for(int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int dev = matrix[i, j] % k;
                    if (dev == 0 || dev % 2 == 0)
                    {
                        data.Add(matrix[i, j]);
                        flag[i, j] = true;
                    }
                }
                res = new int[data.Count];
                for(int j = 0; j < res.Length; j++)
                    res[j] = data[j];
            }
            return (res,flag);
        }
        static int[,] GenerateMatrix(int N, int M)
        {
            Random rnd = new Random();
            int[,] matrix = new int[N,M];
            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < M; j++)
                {
                    matrix[i, j] = rnd.Next(0, 100);
                }
            }
            return matrix;
        }
        static int[] InsertBin(int[] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                int key = matrix[i];
                int low = 0;
                int hight = i - 1;
                while (low < hight)
                {
                    int mid = low + (hight - low) / 2;
                    if (key < matrix[mid])
                        hight = mid;
                    else
                        low = mid + 1;
                }
                for (int j = i; j > low + 1; j--)
                    matrix[j] = matrix[j - 1];
                matrix[low] = key;
            }
            return matrix;
        }
    }
}

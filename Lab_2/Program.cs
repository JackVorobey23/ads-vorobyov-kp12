using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Choose a method of generate (Input random/control): ");
                string input = Console.ReadLine();
                if (input != "random" && input != "control")
                    Console.WriteLine("Incorrect Input!");
                else
                {
                    Console.Write("Enter N:");
                    int N = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter M:");
                    int M = Convert.ToInt32(Console.ReadLine());
                    if (N != M)
                        Console.WriteLine("Incorrect Input!");
                    else
                    {
                        if (input == "random")
                        {
                            int[,] matrix = CreateMatrix(N, M);
                            MainCount(N, M, matrix);
                        }
                        else
                        {
                            int[,] matrix = CreateControlMatrix(N, M);
                            MainCount(N, M, matrix);
                        }
                    }
                }
            }
        }
        static void MainCount(int N, int M, int[,] matrix)
        {
            (int[] ans1, int max, string index1) = CountBottom(N, M, matrix);
            (int[] ans2, int min, string index2) = CountTop(N, M, matrix);
            PrintMatrix(matrix, N, M);
            for (int i = 0; i < N; i++) Console.Write("****");
            Console.WriteLine("\nPart of matrix under the diagonal:\n ");
            PrintODMatrix(ans1, (M * M - M) / 2); Console.Write("\n\nmax: " + max + index1 + "\n");
            Console.WriteLine("\nPart of matrix above the diagonal:\n ");
            PrintODMatrix(ans2, (M * M - M) / 2 + M); Console.Write("\n\nmin: " + min + index2 + "\n");
        }
        static int[,] CreateMatrix(int n, int m)
        {
            Random rnd = new Random();
            int[,] res = new int[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    res[i, j] = rnd.Next(0, 100);
            return res;
        }
        static int[,] CreateControlMatrix(int n, int m)
        {
            int k = 0;
            int[,] res = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    res[i, j] = k;
                    k++;
                }
            }
            return res;
        }
        static void PrintMatrix(int[,] matrix, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"{matrix[i, j],3};");
                }
                Console.WriteLine("\n");
            }
        }
        static void PrintODMatrix(int[] matrix, int m)
        {
            for (int j = 0; j < m; j++)
                Console.Write(matrix[j] + ", ");
        }
        static (int[], int, string) CountBottom(int n, int m, int[,] matrix)
        {
            string index = $"[{n - 1},1]";
            int k = 0, t = 1, d = 1, max = matrix[n - 1, 1];
            int[] res = new int[(n * n - n) / 2];
            while (k < (n * n - n) / 2)
            {
                int l = m - d - 1;
                for (int i = t; i < m - d + 1; i++)
                {
                    res[k] = matrix[n - d, i];
                    if (res[k] > max)
                    {
                        index = "";
                        index += Convert.ToString($"[{n - 1},{i}]");
                        max = res[k];
                    }
                    k++;
                }
                for (int i = n - d - 1; i > t - 1; i--)
                {
                    res[k] = matrix[i, m - d];
                    if (res[k] > max)
                    {
                        index = "";
                        index += Convert.ToString($"[{i},{m - 1}]");
                        max = res[k];
                    }
                    k++;
                }
                for (int i = 2 * d; i < n - d; i++)
                {
                    res[k] = matrix[i, l];
                    if (res[k] > max)
                    {
                        index = "";
                        index += Convert.ToString($"[{i},{l}]");
                        max = res[k];
                    }
                    l = l - 1;
                    k++;
                }
                t += 2; d += 1;
            }
            return (res, max, index);
        }
        static (int[], int, string) CountTop(int n, int m, int[,] matrix)
        {
            string index = $"0,{m - 1}";
            int k = 0, t = 1, d = 1, min = matrix[m - 1, 0];
            int[] res = new int[(n * n - n) / 2 + n];
            while (k < (n * n - n) / 2 + n)
            {
                int l = m - t;
                for (int i = d - 1; i <= n - t; i++)
                {
                    res[k] = matrix[i, l];
                    if (res[k] < min)
                    {
                        index = "";
                        index += Convert.ToString($"[{i},{l}]");
                        min = res[k];
                    }
                    k++; l--;
                }
                for (int i = n - t - 1; i >= d - 1; i--)
                {
                    res[k] = matrix[i, d - 1];
                    if (res[k] < min)
                    {
                        index = "";
                        index += Convert.ToString($"[{i},{d - 1}]");
                        min = res[k];
                    }
                    k++;
                }
                for (int i = d; i <= m - t - 1; i++)
                {
                    res[k] = matrix[d - 1, i];
                    if (res[k] < min)
                    {
                        index = "";
                        index += Convert.ToString($"[{d - 1},{i}]");
                        min = res[k];
                    }
                    k++;
                }
                t += 2; d += 1;
            }
            return (res, min, index);
        }
    }
}
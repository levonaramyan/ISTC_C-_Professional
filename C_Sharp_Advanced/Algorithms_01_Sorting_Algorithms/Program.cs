using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_01_Sorting_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = new int[] { 110, 15, 9, 10, 9, 35, 3 };
            foreach (int a in data) Console.Write($"{a} ");
            Console.WriteLine();
            //SortBubble(data);
            //SortSelection(data);
            SortInsertion(data);
            foreach (int a in data) Console.Write($"{a} ");
            Console.ReadKey();
        }

        static void SortBubble(int[] data)
        {
            int len = data.Length;
            for (int j = 0; j < len - 1; j++)
                for (int i = 0; i < len - 1 - j; i++)
                    if (data[i] > data[i + 1]) Swap(ref data[i], ref data[i + 1]);
        }

        static void SortSelection(int[] data)
        {
            int len = data.Length;
            int minIndex;
            for (int j = 0; j < len - 1; j++)
            {
                minIndex = j;
                for (int i = j + 1; i < len; i++)
                    if (data[i] < data[minIndex]) minIndex = i;
                Swap(ref data[j], ref data[minIndex]);
            }
        }

        static void SortInsertion(int[] data)
        {
            int len = data.Length;
            for (int i = 1; i < len; i++)
            {
                int j = i;
                while (j > 0 && data[j] < data [j - 1])
                    Swap(ref data[j], ref data[j-- - 1]);
            }
        }

        static void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
    }
}

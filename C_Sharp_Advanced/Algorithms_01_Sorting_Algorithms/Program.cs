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
            int[] data = new int[] { 110, 15, 47, 25, 10, 6, 47, 18 };
            foreach (int a in data) Console.Write($"{a} ");
            Console.WriteLine();
            //SortBubble(data);
            //SortSelection(data);
            //SortInsertion(data);
            SortMerge(data);
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
                while (j > 0 && data[j] < data[j - 1])
                    Swap(ref data[j], ref data[j-- - 1]);
            }
        }


        static int[] SortMerge(int[] data)
        {
            int len = data.Length;
            if (len == 1) return data;
            if (data[0] > data[1]) Swap(ref data[1], ref data[0]);
            if (len == 2) return data;
            int bin = (int)Math.Ceiling(Math.Log(len, 2));
            int curBin = 1;

            for (int h = 1; h < bin; h++)
            {
                curBin *= 2;
                int[] subdataR = new int[Math.Min(curBin, len - curBin)];
                for (int i = 0; i < subdataR.Length; i++)
                    subdataR[i] = data[i + curBin];
                int[] subdataL = data.Take(curBin).ToArray();
                int[] subdata = OneStep(subdataL.Concat(SortMerge(subdataR)).ToArray());
                for (int i = 0; i < subdata.Length; i++)
                    data[i] = subdata[i];
            }

            int[] OneStep(int[] subData)
            {
                int lenSub = subData.Length;
                int[] sorted = new int[lenSub];
                int lIndex = 0;
                int rIndex = curBin;
                for (int i = 0; i < lenSub; i++)
                {
                    if (lIndex < curBin && rIndex < lenSub)
                        sorted[i] = (subData[lIndex] <= subData[rIndex]) ? subData[lIndex++] : subData[rIndex++];
                    else sorted[i] = (lIndex >= curBin) ? subData[rIndex++] : subData[lIndex++];
                }
                return sorted;
            }

            return data;
        }

        static void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
    }
}

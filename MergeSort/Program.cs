using System.Diagnostics;
using static MergeSort.MergeSort;

namespace MergeSort;

class Program
{
    public static int[] Generate(int size)
    {
        int[] result = new int[size];
        for (int i = 0; i < result.Length; i++)
            result[i] = new Random().Next(0, size);

        return result;
    }


    static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        int[] test1 = Generate(10000000);
        sw.Start();
        //MergeSortSequential(test1);
        MultithreadedMergeSort(test1, 6);
        sw.Stop();
        Console.WriteLine("Время, затраченное на выполнение: " + sw.ElapsedMilliseconds + "ms");
    }
}

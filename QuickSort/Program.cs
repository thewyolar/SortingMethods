using System.Diagnostics;
using static QuickSort.ParallelSort;

namespace QuickSort;

public class Program
{
    public static int[] Generate(int size)
    {
        int[] result = new int[size];
        for (int i = 0; i < result.Length; i++)
            result[i] = new Random().Next(0, size);

        return result;
    }
    
    public static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        int[] test1 = Generate(10000);
        //Console.WriteLine(String.Join(" ", test1));
        sw.Start();
        //QuicksortSequential(test1);
        QuicksortParallel(test1);
        sw.Stop();
        Console.WriteLine("Время, затраченное на выполнение: " + sw.ElapsedMilliseconds + "ms");
        //Console.WriteLine(String.Join(" ", test1));
    }
}


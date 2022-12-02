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
    
    public static void Test(int size)
    {
        Stopwatch sw = new Stopwatch();
        int[] test1 = Generate(size);
        int[] test2 = new int[test1.Length];
        Array.Copy(test1, test2, test1.Length);
        
        Console.WriteLine($"Тест. Кол-во элементов {size}:");
        
        sw.Start();
        MergeSortSequential(test1);
        sw.Stop();
        Console.WriteLine("[Merge Sort] Время, затраченное на выполнение: " + sw.ElapsedMilliseconds + "ms");
        
        sw.Restart();
        MultithreadedMergeSort(test2, 6);
        sw.Stop();
        Console.WriteLine("[Multithreaded Merge Sort] Время, затраченное на выполнение: " + sw.ElapsedMilliseconds + "ms\n");
    }
    
    static void Main(string[] args)
    {
        Console.WriteLine("Запуск тестов.");

        Test(10000);
        Test(100000);
        Test(1000000);
        Test(10000000);
        Test(100000000);
        
        Console.WriteLine("Для выхода нажмите любую клавишу...");
        Console.ReadKey();
    }
}

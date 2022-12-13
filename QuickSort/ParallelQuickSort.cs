using System.Numerics;

namespace QuickSort;

public class ParallelQuickSort
{
    public static void QuickSortSequential<T>(T [] arr) where T : INumber<T>
    {
        QuickSortSequential(arr, 0, arr.Length - 1);
    }
    
    public static void QuickSortParallel<T>(T[] arr) where T : INumber<T>
    {
        QuickSortParallel(arr, 0, arr.Length - 1);
    }
    
    private static void QuickSortSequential<T>(T[] arr, int left, int right) where T : INumber<T>
    {
        if (right > left)
        {
            int pivot = Partition(arr, left, right);
            QuickSortSequential(arr, left, pivot - 1);
            QuickSortSequential(arr, pivot + 1, right);
        }
    }

    private static void QuickSortParallel<T>(T[] arr, int left, int right) where T : INumber<T>
    {
        const int SEQUENTIAL_THRESHOLD = 2048;
        if (right > left)
        {
            if (right - left < SEQUENTIAL_THRESHOLD)
            {
                QuickSortSequential(arr, left, right);
            }
            else
            {
                int pivot = Partition(arr, left, right);
                Parallel.Invoke(
                    () => QuickSortParallel(arr, left, pivot - 1),
                    () => QuickSortParallel(arr, pivot + 1, right));
            }
        }
    }

    private static void Swap<T>(T[] arr, int i, int j) where T : INumber<T>
    {
        T tmp = arr[i];
        arr[i] = arr[j];
        arr[j] = tmp;
    }

    private static int Partition<T>(T[] arr, int low, int high) where T : INumber<T>
    {
        int pivotPos = (high + low) / 2;
        T pivot = arr[pivotPos];
        Swap(arr, low, pivotPos);

        int left = low;
        for (int i = low + 1; i <= high; i++)
        {
            if (arr[i].CompareTo(pivot) < 0)
            {
                left++;
                Swap(arr, i, left);
            }
        }

        Swap(arr, low, left);
        return left;
    }
}
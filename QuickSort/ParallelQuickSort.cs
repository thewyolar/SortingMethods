using System.Numerics;

namespace QuickSort;

public static class ParallelQuickSort
{
    /// <summary>
    /// Запускает последовательную быструю сортировку.
    /// </summary>
    /// <param name="arr">сортируемый массив</param>
    /// <typeparam name="T">тип элементов массива</typeparam>
    public static void QuickSortSequential<T>(T[] arr) where T : INumber<T>
    {
        QuickSortSequential(arr, 0, arr.Length - 1);
    }
    
    /// <summary>
    /// Запускает параллельную быструю сортировку. 
    /// </summary>
    /// <param name="arr">сортируемый массив.</param>
    /// <typeparam name="T">тип элементов массива.</typeparam>
    public static void QuickSortParallel<T>(T[] arr) where T : INumber<T>
    {
        QuickSortParallel(arr, 0, arr.Length - 1);
    }
    
    /// <summary>
    /// Перегруженный метод последовательной быстрой сортировки.
    /// Осуществляет выбор опорного элемента в массиве.
    /// Разбиение: перераспределяет элементы в массиве таким образом, что элементы,
    /// меньшие опорного, помещаются перед ним, а большие или равные - после.
    /// Рекурсивно применяет первые два шага к двум подмассивам слева и справа от опорного элемента.
    /// </summary>
    /// <param name="arr">сортируемый массив.</param>
    /// <param name="left">нижняя граница сортируемого массива.</param>
    /// <param name="right">верхняя граница сортируемого массива.</param>
    /// <typeparam name="T">тип элементов массива.</typeparam>
    private static void QuickSortSequential<T>(T[] arr, int left, int right) where T : INumber<T>
    {
        if (right > left)
        {
            int pivot = Partition(arr, left, right);
            QuickSortSequential(arr, left, pivot - 1);
            QuickSortSequential(arr, pivot + 1, right);
        }
    }

    /// <summary>
    /// Перегруженный метод параллельной быстрой сортировки.
    /// Параллельно запускает рекурсию к двум подмассивам слева и справа от опорного элемента.
    /// </summary>
    /// <param name="arr">сортируемый массив.</param>
    /// <param name="left">нижняя граница сортируемого массива.</param>
    /// <param name="right">верхняя граница сортируемого массива.</param>
    /// <typeparam name="T">тип элементов массива.</typeparam>
    private static void QuickSortParallel<T>(T[] arr, int left, int right) where T : INumber<T>
    {
        const int sequentialThreshold = 2048;
        if (right > left)
        {
            if (right - left < sequentialThreshold)
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

    /// <summary>
    /// Меняет местами два элемента массива.
    /// </summary>
    /// <param name="arr">массив, элементы которого меняются местами.</param>
    /// <param name="i">индекс первого элемента.</param>
    /// <param name="j">индекс второго элемента.</param>
    /// <typeparam name="T">тип элементов массива.</typeparam>
    private static void Swap<T>(T[] arr, int i, int j) where T : INumber<T>
    {
        (arr[i], arr[j]) = (arr[j], arr[i]);
    }

    /// <summary>
    /// Осуществляет выбор опорного элемента в массиве.
    /// </summary>
    /// <param name="arr">сортируемый массив.</param>
    /// <param name="low">нижняя граница сортируемого массива.</param>
    /// <param name="high">верхняя граница сортируемого массива.</param>
    /// <typeparam name="T">тип элементов массива.</typeparam>
    /// <returns>Возвращает индекс опорного элемента массива.</returns>
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
using System;
using System.Numerics;
using System.Threading;

namespace MergeSort;

public class MergeSort
{
    public static void MergeSortSequential<T>(T[] arr) where T : INumber<T>
    {
        MergeSortSequential(arr, 0, arr.Length - 1);
    }

    public static void MultithreadedMergeSort<T>(T[] arr, int mtCount) where T : INumber<T>
    {
        MultithreadedMergeSort(arr, 0, arr.Length - 1, mtCount);
    }

    private static void MultithreadedMergeSort<T>(T[] arr, int lowIndex, int highIndex, int mtCount) where T : INumber<T>
    {
        int middleIndex = (lowIndex + highIndex) / 2;

        if (lowIndex < highIndex)
        {
            if (mtCount > 1)
            {
                Thread t = new Thread(() => MultithreadedMergeSort(arr, lowIndex, middleIndex, mtCount - 1));
                t.Start();
                
                MultithreadedMergeSort(arr, middleIndex + 1, highIndex, mtCount - 1);

                t.Join();
            }
            else
            {
                MergeSortSequential(arr, lowIndex, middleIndex);
                MergeSortSequential(arr, middleIndex + 1, highIndex);
            }

            Merge(arr, lowIndex, middleIndex, highIndex);
        }
    }

    private static void MergeSortSequential<T>(T[] arr, int lowIndex, int highIndex) where T : INumber<T>
    {
        if (lowIndex < highIndex)
        {
            int middleIndex = (lowIndex + highIndex) / 2;
            MergeSortSequential(arr, lowIndex, middleIndex);
            MergeSortSequential(arr,middleIndex + 1, highIndex);
            Merge(arr, lowIndex, middleIndex, highIndex);
        }
    }
    
    private static void Merge<T>(T[] arr, int lowIndex, int middleIndex, int highIndex) where T : INumber<T>
    {
        int left = lowIndex;
        int right = middleIndex + 1;
        T[] temp = new T[highIndex - lowIndex + 1];
        int index = 0;

        while (left <= middleIndex && right <= highIndex)
        {
            if (arr[left] < arr[right])
            {
                temp[index] = arr[left];
                left++;
            }
            else
            {
                temp[index] = arr[right];
                right++;
            }

            index++;
        }

        for (int i = left; i <= middleIndex; i++)
        {
            temp[index] = arr[i];
            index++;
        }

        for (int i = right; i <= highIndex; i++)
        {
            temp[index] = arr[i];
            index++;
        }

        for (int i = 0; i < temp.Length; i++)
            arr[lowIndex + i] = temp[i];
    }
}
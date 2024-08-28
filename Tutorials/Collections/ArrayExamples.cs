using System;

namespace Tutorials.Collections;

static class ArrayExamples
{
    public static void Test()
    {
        // Declare a single-dimensional string array
        string[] array1d = {
            "zero", "one", "two", "three"
        };
        ShowArrayInfo(array1d);

        // Declare a two-dimensional string array
        string[,] array2d = {
            { "zero", "0" }, { "one", "1" }, { "two", "2" }, { "three", "3"}, { "four", "4" }, { "five", "5" }
        };
        ShowArrayInfo(array2d);

        // Declare a three-dimensional integer array
        int[,,] array3d = new int[,,] {
            {
                { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }
            },
            {
                { 10, 11, 12 }, { 11, 12, 13 }, {12, 13, 14 }
            }
        };
        ShowArrayInfo(array3d);

        // staggered arrays
        int[][] arr2d = {
            new int[] {1, 2},
            new int[] {4, 5, 6},
            new int[4] {7, 8, 9, 0}
        };
        ShowArrayInfo(arr2d);

        int[][][] arr3d =
        {
            new int[][]
            {
                new int[] {1, 2},
                new int[] {4, 1, 7},
            },
            new int[][]
            {
                new int[] {11, 2},
                new int[] {41, 57, 25},
                new int[] {32, 24, 99, 3, 6, 89},
            }
        };

        // 6
        Console.WriteLine(arr2d[1][2]);

        // 7
        Console.WriteLine(arr3d[0][1][2]);

        Array.ForEach(arr3d,
            x => Array.ForEach(x,
                z =>
                {
                    Array.Sort(z);
                    Array.ForEach(z, c => Console.Write($"{c} "));
                    Console.WriteLine();
                }
            )
        );
    }

    private static void ShowArrayInfo(Array arr)
    {
        Console.WriteLine("Length of Array:      {0,3}", arr.Length);
        Console.WriteLine("Number of Dimensions: {0,3}", arr.Rank);
        // For multidimensional arrays, show number of elements in each dimension.
        if (arr.Rank > 1)
        {
            for (int dimension = 1; dimension <= arr.Rank; dimension++)
                Console.WriteLine("   Dimension {0}: {1,3}", dimension,
                                    arr.GetUpperBound(dimension - 1) + 1);
        }
        Console.WriteLine();
    }
}
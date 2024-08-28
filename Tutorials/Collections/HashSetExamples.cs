using System;
using System.Collections.Generic;

namespace Tutorials.Collections;

static class HashSetExamples
{
    public static void Test()
    {
        HashSet<int> big = new() { 1, 2, 3, 8, 9, 10, 11 };
        HashSet<int> s32 = new() { 3, 2 };
        HashSet<int> t23 = new() { 2, 3 };
        HashSet<int> x249 = new() { 2, 4, 9 };
        HashSet<int> y = new() { 3, 3, 1, 1, 1, 1 };

        // proper subset can never be equal to other
        Console.WriteLine("S32 is proper subset of big: {0}", s32.IsProperSubsetOf(big));   // true
        Console.WriteLine("S32 is proper subset of T23: {0}", s32.IsProperSubsetOf(t23));   // false

        // subset can have equal number of elements
        Console.WriteLine("S32 is subset of big: {0}", s32.IsSubsetOf(big));    // true
        Console.WriteLine("S32 is subset of T23: {0}", s32.IsSubsetOf(t23));    // true

        Console.WriteLine("X249 is subset of big: {0}", x249.IsSubsetOf(big));  // false
        Console.WriteLine("x249 overlaps big: {0}", x249.Overlaps(big));        // true
        Console.WriteLine("Big overlaps x249: {0}", big.Overlaps(x249));        // true

        ShowSet(y);
        t23.UnionWith(y);
        ShowSet(t23);
        t23.ExceptWith(y);
        ShowSet(t23);
    }

    static void ShowSet(HashSet<int> s)
    {
        Console.WriteLine(string.Join(", ", s));
    }
}
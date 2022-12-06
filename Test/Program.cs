using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Globalization;

namespace Tutorials
{
    internal class Program
    {
        public static void Main()
        {
            LinkedList_Example.Test();
        }

        static void cw(params object[] p)
        {
            if (p[0] is string s) Console.WriteLine(s);
            else Console.WriteLine(p[0].ToString());
        }

        public static void Test()
        {
            int[] x = { 1, 2, 3, 4, 5 };
            cw(x[^2]);
            cw(string.Join(" ", x[1..^1]));
        }
    }
}
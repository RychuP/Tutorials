﻿namespace InRefOut
{
    // in cannot be modified, ref may be modified, out must be assigned

    using System;

    class Program
    {
        static void Main1()
        {
            int s = 2, g = 4;
            Foo(in s, ref g, out int z);

            Console.WriteLine(z);
        }

        static void Foo(in int x, ref int u, out int y)
        {
            u *= 2;
            y = 5 + x * u;
        }
    }
}

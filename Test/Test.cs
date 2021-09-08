namespace Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main1(string[] args)
        {
            int[] numbers = { 2, 3, 4, 5 };

            // method 1
            IEnumerable<int> query = from n in numbers
                        select n * n;
            string output = string.Empty;
            foreach (int n in query)
            {
                output += n + " ";
            }
            Console.WriteLine(output);

            // method 1.5
            Console.WriteLine("\n" + string.Join(" ", query));

            // method 2
            var squaredNumbers = numbers.Select(n => n * n);
            Console.WriteLine("\n" + string.Join(" ", squaredNumbers));
        }
    }
}
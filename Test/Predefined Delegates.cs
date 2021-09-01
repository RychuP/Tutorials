namespace PredefinedDelegates
{
    // good explanation at https://docs.microsoft.com/en-gb/dotnet/csharp/delegates-strongly-typed

    using System;

    class Program
    {
        static void Main()
        {
            int x = 1;

            // action that takes double as a parameter
            Action<double> y = Nothing;

            // action that takes int as a parameter
            Action<int> y1 = Nothing;

            // function that takes int as a parameter and returns string
            Func<int, string> z = (x) => $"String representation of the {x.GetType()} {x} is {x}.";

            // takes int and returns bool
            Predicate<int> g = (x) => x == 69 ? true : false;
            
            // test calls
            y(23);
            y1(32);
            Console.WriteLine(z((int)23f));
            Console.WriteLine("Predicate g with a param 69 returns {0}.", g(69) ? "true" : "false");
            Console.WriteLine("Predicate g with a param 78 returns {0}.", g(78) ? "true" : "false");
            Console.WriteLine("Variable x value: {0}", x);
        }

        static void Nothing<T>(T cos)
        {
            Console.WriteLine("Inside generic Nothing. {0} is type {1}", cos, cos.GetType());
        }

        static void Nothing(int cos)
        {
            Console.WriteLine("Inside not generic Nothing. {0} is type {1}", cos, cos.GetType());
        }
    }
}
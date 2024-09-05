using System;

namespace Tutorials.Delegates;

// good explanation at https://docs.microsoft.com/en-gb/dotnet/csharp/delegates-strongly-typed
static class PredefinedDelegates
{
    public static void Test()
    {
        int x = 1;

        // function that takes double as a parameter
        Action<double> doubleAction = DisplayArg;

        // function that takes int as a parameter
        Action<int> intAction = DisplayArg;

        // function that takes int as a parameter and returns string
        Func<int, string> intStrFunc = DisplayArgType;

        // function that takes int as a parameter and returns bool
        Predicate<int> predicFunc = CheckArgIs69;

        // test calls
        doubleAction(23);
        intAction(32);
        Console.WriteLine(intStrFunc((int)23.4f));
        Console.WriteLine("Predicate func checking if param 69 is 69 returns {0}.", predicFunc(69) ? "true" : "false");
        Console.WriteLine("Predicate func checking if param 78 is 69 returns {0}.", predicFunc(78) ? "true" : "false");
        Console.WriteLine("Variable x value: {0}", x);
    }

    static void DisplayArg<T>(T arg) =>
        Console.WriteLine("Inside generic DisplayArg. {0} is type {1}", arg, arg!.GetType());

    static void DisplayArg(int arg) =>
        Console.WriteLine($"Inside not generic DisplayArg. {arg} is type {arg.GetType()}");

    static bool CheckArgIs69(int arg) =>
        arg == 69;

    static string DisplayArgType(int arg) =>
        $"String representation of the {arg.GetType()} {arg} is {arg}.";
}
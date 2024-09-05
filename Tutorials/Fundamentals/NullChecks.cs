using static System.Console;

namespace Tutorials.Fundamentals;

static class NullChecks
{
    public static void Test()
    {
        SampleStruct x = default;
        WriteLine(x.Prop1 ?? "failure");

        x = new SampleStruct();
        WriteLine(x.Prop1 ?? "failure");

        WriteLine(x.Prop2 ?? "prop2 is null");
        WriteLine(x.Prop2 ??= "something");
        WriteLine(x.Prop2);
    }

    struct SampleStruct
    {
        public string Prop1 = "Default Value";
        public string? Prop2 = null;
        public SampleStruct()
        {
            Prop1 = "Some Value";
        }
    }
}
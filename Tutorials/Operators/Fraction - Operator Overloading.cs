namespace Tutorials.Operators;

class Fraction
{
    readonly int _num;
    readonly int _den;

    public Fraction(int num, int den)
    {
        if (den == 0)
        {
            throw new ArgumentException("Denominator cannot be zero");
        }

        _num = num;
        _den = den;
    }
    public static void Test()
    {
        try
        {
            var n = new Fraction(1, 0);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Fraction creation didn't succeed:\n{e.Message}");
        }
        Fraction a = new(2, 3);
        Fraction c = new(3, 4);
        var d = a / c;
        Console.WriteLine($"{a} / {c} = {d}");
        Console.WriteLine($"{d} = {d + 0.2d:F2}");
    }
    public static Fraction operator +(Fraction a) => a;
    public static Fraction operator -(Fraction a) => new(-a._num, a._den);
    public static Fraction operator +(Fraction a, Fraction b) =>
        new(a._num * b._den + b._num * a._den, a._den * b._den);
    public static Fraction operator -(Fraction a, Fraction b) => a + -b;
    public static Fraction operator *(Fraction a, Fraction b) =>
        new(a._num * b._num, a._den * b._den);
    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b._num == 0)
        {
            throw new DivideByZeroException();
        }
        return new Fraction(a._num * b._den, a._den * b._num);
    }

    public static implicit operator double(Fraction a) => (double)a._num / a._den;

    public static explicit operator Fraction(double a) => throw new NotImplementedException();
    public override string ToString() => $"<{_num}/{_den}>";
}
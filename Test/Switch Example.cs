namespace SwitchExample
{
    using System;

    class Switch
    {
        public static void Main1()
        {
            var t = new Test();
            try
            {
                Console.WriteLine(t.Sprawdz("2"));
                Console.WriteLine($"{t.CalculateDiscount(new Order(20, 5000)) * 100:0.0}%");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }


    class Test
    {
        public string Sprawdz(string t) =>
            t switch
            {
                "1" => "We have 1.",
                "2" => "We have 2.",
                "3" => "We have 3.",
                _ => throw new ArgumentException("Invalid string number.", nameof(t)),
            };

        string WaterState(int tempInFahrenheit) =>
            tempInFahrenheit switch
            {
                (> 32) and (< 212) => "liquid",
                < 32 => "solid",
                > 212 => "gas",
                32 => "solid/liquid transition",
                212 => "liquid / gas transition",
            };

        string WaterState2(int tempInFahrenheit) =>
            tempInFahrenheit switch
            {
                < 32 => "solid",
                32 => "solid/liquid transition",
                < 212 => "liquid",
                212 => "liquid / gas transition",
                _ => "gas",
            };

        public decimal CalculateDiscount(Order order) =>
            order switch
            {
                { Items: > 10, Cost: > 1000.00m } => 0.10m,
                (Items: > 5, Cost: > 500.00m) => 0.05m,
                { Cost: > 250.00m } => 0.02m,
                null => throw new ArgumentNullException(nameof(order), "Can't calculate discount on null order"),
                _ => 0m,
            };
    }

    public record Order(int Items, decimal Cost);
}
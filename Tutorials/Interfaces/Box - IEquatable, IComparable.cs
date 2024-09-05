using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Tutorials.Interfaces
{
    internal struct Box : IEquatable<Box>, IComparable<Box>
    {
        static int IDSeed = 0;

        public int ID { get; init; } = IDSeed++;
        public int Height { get; init; }
        public int Width { get; init; }
        public int Length { get; init; }
        public int Size => Height * Width * Length;

        public Box(int w, int h, int l)
        {
            if (w < 0 || h < 0 || l < 0)
            {
                string? m = "Size of the box cannot be 0 or negative.";
                throw new ArgumentOutOfRangeException(m);
            }
            Width = w;
            Height = h;
            Length = l;
        }

        public bool Equals(Box other) => other.ID == ID;

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode() => ID;

        public static bool operator ==(Box b1, Box b2) => b1.Equals(b2);

        public static bool operator !=(Box b1, Box b2) => !(b1 == b2);

        public int CompareTo(Box other) => Size.CompareTo(other.Size);

        public static bool operator >(Box x, Box y) => x.Size > y.Size;

        public static bool operator <(Box x, Box y) => x.Size < y.Size;

        public static bool operator >=(Box x, Box y) => x.Size >= y.Size;

        public static bool operator <=(Box x, Box y) => x.Size <= y.Size;

        public string ToString(int colID, int colS, int colW, int colH, int colL)
        {
            string format = "{0, -" + colID + "} | {1, -" + colS + "} | {2, " + colW +
                "} | {3, " + colH + "} | {4, " + colL + "}";
            return string.Format(format, ID, Size, Width, Height, Length);
        }

        static void PrintBoxes(List<Box> boxes, string title)
        {
            Console.WriteLine(title);
            (string b, string s, string w, string h, string l) c = ("Box ID:", "Size:  ", "Width:", "Height:", "Length:");
            string x = " | ";
            string header = c.b + x + c.s + x + c.w + x + c.h + x + c.l;
            Console.WriteLine(new string('-', header.Length));
            Console.WriteLine(header);
            Console.WriteLine(new string('-', header.Length));
            foreach (var b in boxes)
            {
                Console.WriteLine(b.ToString(c.b.Length, c.s.Length, c.w.Length, c.h.Length, c.l.Length));
            }
        }

        public static void Test()
        {
            List<Box> boxes = new();
            var rand = new Random();
            for (int i = 0, count = rand.Next(10, 30); i < count; i++)
            {
                boxes.Add(new Box(rand.Next(5, 100), rand.Next(5, 100), rand.Next(5, 100)));
            }

            PrintBoxes(boxes, $"Total of {boxes.Count} boxes in default order: ");
            Console.WriteLine();
            boxes.Sort();
            PrintBoxes(boxes, "After sorting: ");

            int[] arr = { 2, 6, 3, 1, 7, 2, 4 };
            Array.Sort(arr);
            foreach (int x in arr)
            {
                Console.Write(x + ", ");
            }
        }
    }
}

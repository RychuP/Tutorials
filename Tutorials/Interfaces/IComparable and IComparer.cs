using ConsoleTable;
using System.Collections.Generic;
using System.Collections;
using RandomNameGenerator;

namespace Tutorials.Interfaces;

static class IComparable_and_IComparer
{
    public static void Test()
    {
        Person[] people = RandomNameGenerator.Program.GeneratePeople(12);

        // show unsorted
        Console.WriteLine("People in random order:");
        Display(people);

        // sort by first name
        Array.Sort(people);
        Console.WriteLine("Sort people by first name:");
        Display(people);

        // sort by second name
        Array.Sort(people, new LastNameComparer());
        Console.WriteLine("Sort people by last name:");
        Display(people);

        // sort by gender
        Array.Sort(people, new GenderComparer());
        Console.WriteLine("Sort people by gender:");
        Display(people);

        // other sorting tests
        var icomparer = new CaseInsensitiveComparer();
        string x = "Some text";
        string y = "some text";
        Console.WriteLine(x.CompareTo(y));
        Console.WriteLine(icomparer.Compare(x, y));
    }

    class LastNameComparer : Comparer<Person>
    {
        public override int Compare(Person? p1, Person? p2)
        {
            if (p1 is null && p2 is null) return 0;
            else if (p1 is null) return -1;
            else if (p2 is null) return 1;
            else return p1.Name.Last.CompareTo(p2.Name.Last);
        }
    }

    class GenderComparer : Comparer<Person>
    {
        public override int Compare(Person? p1, Person? p2)
        {
            if (p1 is null && p2 is null) return 0;
            else if (p1 is null) return -1;
            else if (p2 is null) return 1;
            else
            {
                // compare first by sex then by first name
                int sexComparison = p1.Sex.CompareTo(p2.Sex);
                if (sexComparison != 0) return sexComparison;
                else return p1.CompareTo(p2);
            }
        }
    }

    static void Display(Person[] people)
    {
        string[] headers = { "First Name:", "Last Name:", "Sex:" };
        string[,] data = new string[people.Length, 3];
        for (int i = 0, count = people.Length; i < count; i++)
        {
            data[i, 0] = people[i].Name.First;
            data[i, 1] = people[i].Name.Last;
            data[i, 2] = people[i].Sex.ToString();
        }
        Table t = new(headers, data);
        t.SetColWidth(12);
        Console.WriteLine(t);
    }
}
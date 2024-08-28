using System;
using System.IO;
using System.Collections.Generic;

namespace RandomNameGenerator;

public class Program
{
    static void Main()
    {
        Person[] people = GeneratePeople(20);
    }

    public static Person[] GeneratePeople(int count)
    {
        List<string> boyNames = GetNamesFromFile(@"Boy Names.txt");
        List<string> girlNames = GetNamesFromFile(@"Girl Names.txt");
        List<string> lastNames = GetNamesFromFile(@"Surnames.txt");

        count = count < 0 ? 0 : count;
        var people = new Person[count];
        Random rand = new();

        for (int i = 0; i < count; i++)
        {
            var sex = (Gender)rand.Next(2);
            string fname = sex switch
            {
                Gender.Male => boyNames[rand.Next(boyNames.Count)],
                Gender.Female => girlNames[rand.Next(girlNames.Count)],
                _ => throw new NotImplementedException()
            };
            Name name = new(fname, lastNames[rand.Next(lastNames.Count)]);
            people[i] = new Person(name, sex, rand.Next(99));
        }

        return people;
    }

    static List<string> GetNamesFromFile(string path)
    {
        path = $"Resources\\{path}";
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"\"{path}\" doesn't exist.");
        }

        var names = new List<string>();
        using StreamReader sr = File.OpenText(path);
        string name;
        while ((name = sr.ReadLine()) != null)
            names.Add(name);
        return names;
    }
}
    
public record Person(Name Name, Gender Sex, int Age) : IComparable<Person>
{
    public int CompareTo(Person other)
    {
        int firstNameComparison = Name.First.CompareTo(other.Name.First);
        if (firstNameComparison != 0) return firstNameComparison;
        else
        {
            int secondNameComparison = Name.Last.CompareTo(other.Name.Last);
            if (secondNameComparison != 0) return secondNameComparison;
            else
            {
                int ageComparison = Age.CompareTo(other.Age);
                if (ageComparison != 0) return ageComparison;
            }
        }
        return 0;
    }
};

public record Name(string First, string Last);

public enum Gender
{
    Male,
    Female
}
using System;
using System.IO;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main()
        {
            Random random = new();
            List<string> boyNames = GetNamesFromFile(@"Boy Names.txt");
            List<string> girlNames = GetNamesFromFile(@"Girl Names.txt");
            List<string> lastNames = GetNamesFromFile(@"Surnames.txt");
            
            for (int i = 0; i < 20; i++)
            {
                var gender = (Gender) random.Next(2);
                string fname = gender switch
                {
                    Gender.Male => boyNames[random.Next(boyNames.Count)],
                    Gender.Female => girlNames[random.Next(girlNames.Count)],
                    _ => throw new NotImplementedException()
                };
                Console.WriteLine("{0} {1}", fname, lastNames[random.Next(lastNames.Count)]);
            }
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

    enum Gender
    {
        Male,
        Female
    }
}
namespace LinqJoins
{
    // linq joins https://docs.microsoft.com/en-gb/dotnet/csharp/linq/perform-inner-joins

    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            DrawLine("Inner Join", true);
            InnerJoinExample();

            DrawLine("Composite Key Join");
            CompositeKeyJoinExample();

            DrawLine("Multiple Join");
            MultipleJoinExample();

            DrawLine("Inner Group Join");
            InnerGroupJoinExample();
        }

        static void DrawLine(string title, bool firstLine = false)
        {
            Console.WriteLine("{0}--- {1} ---{2}", 
                firstLine ? "" : "\n\n", 
                title,
                "\n");
        }

        /// <summary>
        /// Simple inner join.
        /// </summary>
        public static void InnerJoinExample()
        {


            // Create two lists.
            List<Person> people = Data.people;
            List<Cat> pets = Data.cats;

            // Create a collection of person-pet pairs. Each element in the collection
            // is an anonymous type containing both the person's name and their pet's name.
            var query = from person in people
                        join pet in pets on person equals pet.Owner
                        select new { OwnerName = person.FirstName, PetName = pet.Name };

            foreach (var ownerAndPet in query)
            {
                Console.WriteLine($"\"{ownerAndPet.PetName}\" is owned by {ownerAndPet.OwnerName}");
            }
        }

        /// <summary>
        /// Performs a join operation using a composite key.
        /// </summary>
        public static void CompositeKeyJoinExample()
        {
            List<Employee> employees = Data.employees;
            List<Student> students = Data.students;

            // Join the two data sources based on a composite key consisting of first and last name,
            // to determine which employees are also students.
            IEnumerable<string> query = from employee in employees
                                        join student in students
                                        on new { employee.FirstName, employee.LastName }
                                        equals new { student.FirstName, student.LastName }
                                        select employee.FirstName + " " + employee.LastName;

            Console.WriteLine("The following people are both employees and students:");
            foreach (string name in query)
                Console.WriteLine(name);
        }

        public static void MultipleJoinExample()
        {
            // Create three lists.
            List<Person> people = Data.people;
            List<Cat> cats = Data.cats;
            List<Dog> dogs = Data.dogs;

            // The first join matches Person and Cat.Owner from the list of people and
            // cats, based on a common Person. The second join matches dogs whose names start
            // with the same letter as the cats that have the same owner.
            var query = from person in people
                        join cat in cats on person equals cat.Owner
                        join dog in dogs on
                        new { Owner = person, Letter = cat.Name.Substring(0, 1) }
                        equals new { dog.Owner, Letter = dog.Name.Substring(0, 1) }
                        select new { CatName = cat.Name, DogName = dog.Name, Owner = person };

            foreach (var obj in query)
            {
                Console.WriteLine(
                    $"The cat \"{obj.CatName}\" shares a house owned by {obj.Owner.FirstName}, " +
                    $"and the first letter of their name, with \"{obj.DogName}\".");
            }
        }

        /// <summary>
        /// Performs an inner join by using GroupJoin().
        /// </summary>
        public static void InnerGroupJoinExample()
        {
            // Create two lists.
            List<Person> people = Data.people;
            List<Cat> pets = Data.cats;

            var query1 = from person in people
                         join pet in pets on person equals pet.Owner into gj
                         from subpet in gj
                         select new { OwnerName = person.FirstName, PetName = subpet.Name };

            Console.WriteLine("Inner join using GroupJoin():");
            foreach (var v in query1)
            {
                Console.WriteLine($"{v.OwnerName} - {v.PetName}");
            }

            var query2 = from person in people
                         join pet in pets on person equals pet.Owner
                         select new { OwnerName = person.FirstName, PetName = pet.Name };

            Console.WriteLine("\nThe equivalent operation using Join():");
            foreach (var v in query2)
                Console.WriteLine($"{v.OwnerName} - {v.PetName}");
        }
    }

    static class Data
    {
        
        static Employee terry = new Employee { FirstName = "Terry", LastName = "Adams", EmployeeID = 522459 };
        static Employee charlotte = new Employee { FirstName = "Charlotte", LastName = "Weiss", EmployeeID = 204467 };
        static Employee vernette = new Employee { FirstName = "Vernette", LastName = "Price", EmployeeID = 437139 };
        static Employee magnus = new Employee { FirstName = "Magnus", LastName = "Hedland", EmployeeID = 866200 };

        static Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };
        static Person rui = new Person { FirstName = "Rui", LastName = "Raposo" };
        static Person phyllis = new Person { FirstName = "Phyllis", LastName = "Harris" };
        static Person rychu = new Person { FirstName = "Ryszard", LastName = "Pyka" };

        static Cat barley = new Cat { Name = "Barley", Owner = terry };
        static Cat boots = new Cat { Name = "Boots", Owner = terry };
        static Cat whiskers = new Cat { Name = "Whiskers", Owner = charlotte };
        static Cat bluemoon = new Cat { Name = "Blue Moon", Owner = rui };
        static Cat daisy = new Cat { Name = "Daisy", Owner = magnus };
        static Cat rupert = new Cat { Name = "Rupert", Owner = rychu };

        static Dog fourwheeldrive = new Dog { Name = "Four Wheel Drive", Owner = phyllis };
        static Dog duke = new Dog { Name = "Duke", Owner = magnus };
        static Dog denim = new Dog { Name = "Denim", Owner = terry };
        static Dog wiley = new Dog { Name = "Wiley", Owner = charlotte };
        static Dog snoopy = new Dog { Name = "Snoopy", Owner = rui };
        static Dog snickers = new Dog { Name = "Snickers", Owner = arlene };

        // Create a list of students.
        public static List<Student> students = new List<Student> {
            new Student { FirstName = "Vernette", LastName = "Price", StudentID = 9562 },
            new Student { FirstName = "Terry", LastName = "Earls", StudentID = 9870 },
            new Student { FirstName = "Terry", LastName = "Adams", StudentID = 9913 }
        };

        public static List<Employee> employees = new List<Employee> { terry, charlotte, vernette, magnus };
        public static List<Person> people = new List<Person> { magnus, terry, charlotte, arlene, rui, phyllis, vernette, rychu };
        public static List<Cat> cats = new List<Cat> { barley, boots, whiskers, bluemoon, daisy, rupert };
        public static List<Dog> dogs = new List<Dog> { fourwheeldrive, duke, denim, wiley, snoopy, snickers };
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }

    class Cat : Pet
    { }

    class Dog : Pet
    { }

    class Employee : Person
    {
        public int EmployeeID { get; set; }
    }

    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentID { get; set; }
    }
}
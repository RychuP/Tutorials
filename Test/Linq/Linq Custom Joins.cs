namespace CustomJoinOperations
{
    // https://docs.microsoft.com/en-gb/dotnet/csharp/linq/perform-custom-join-operations
    // various joins

    using System;
    using System.Linq;
    using System.Collections.Generic;

    class CustomJoins
    {
        static void Main()
        {
            CustomJoins app = new CustomJoins();
            // app.CrossJoin();
            app.NonEquijoin();

            Console.WriteLine("\nNormal Join:");
            app.NormalJoin();

            Console.WriteLine("\nGroup Join:");
            app.GroupJoin();
        }

        #region Data

        class Product
        {
            public string Name { get; set; }
            public int CategoryID { get; set; }
        }

        class Category
        {
            public string Name { get; set; }
            public int ID { get; set; }
        }

        // Specify the first data source.
        List<Category> categories = new List<Category>()
        {
            new Category(){Name="Beverages", ID=001},
            new Category(){ Name="Condiments", ID=002},
            new Category(){ Name="Vegetables", ID=003},
        };

        // Specify the second data source.
        List<Product> products = new List<Product>()
        {
            new Product{Name="Tea",  CategoryID=001},
            new Product{Name="Mustard", CategoryID=002},
            new Product{Name="Pickles", CategoryID=002},
            new Product{Name="Carrots", CategoryID=003},
            new Product{Name="Bok Choy", CategoryID=003},
            new Product{Name="Peaches", CategoryID=005},
            new Product{Name="Melons", CategoryID=005},
            new Product{Name="Ice Cream", CategoryID=007},
            new Product{Name="Mackerel", CategoryID=012},
        };
        #endregion

        void NormalJoin()
        {
            var query =
                from c in categories
                join p in products on c.ID equals p.CategoryID
                select new { CatID = c.ID, Category = c.Name, Product = p.Name };

            foreach (var item in query)
            {
                Console.WriteLine("{0, -5}{1, -12}{2}", item.CatID, item.Category, item.Product);
            }
        }

        void GroupJoin()
        {
            var query =
                from c in categories
                join p in products on c.ID equals p.CategoryID into prodGroup
                select new { Category = c, Products = prodGroup };

            foreach (var item in query)
            {
                Console.WriteLine("{0} {1}", item.Category.ID, item.Category.Name);
                foreach (Product p in item.Products)
                {
                    Console.WriteLine("   {0}", p.Name);
                }
            }
        }

        void CrossJoin()
        {
            var crossJoinQuery =
                from c in categories
                from p in products
                select new { c.ID, p.Name };

            Console.WriteLine("Cross Join Query:");
            foreach (var v in crossJoinQuery)
            {
                Console.WriteLine($"{v.ID,-5}{v.Name}");
            }
        }

        void NonEquijoin()
        {
            var nonEquijoinQuery =
                from p in products
                let catIds = from c in categories
                             select c.ID
                where catIds.Contains(p.CategoryID) == true
                select new { Product = p.Name, CategoryID = p.CategoryID };

            Console.WriteLine("Non-equijoin query:");
            foreach (var v in nonEquijoinQuery)
            {
                Console.WriteLine($"{v.CategoryID,-5}{v.Product}");
            }
        }
    }
    /* Output:
    Cross Join Query:
    1    Tea
    1    Mustard
    1    Pickles
    1    Carrots
    1    Bok Choy
    1    Peaches
    1    Melons
    1    Ice Cream
    1    Mackerel
    2    Tea
    2    Mustard
    2    Pickles
    2    Carrots
    2    Bok Choy
    2    Peaches
    2    Melons
    2    Ice Cream
    2    Mackerel
    3    Tea
    3    Mustard
    3    Pickles
    3    Carrots
    3    Bok Choy
    3    Peaches
    3    Melons
    3    Ice Cream
    3    Mackerel
    Non-equijoin query:
    1    Tea
    2    Mustard
    2    Pickles
    3    Carrots
    3    Bok Choy
    Press any key to exit.
     */
}
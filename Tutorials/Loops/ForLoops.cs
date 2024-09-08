namespace Tutorials.Loops;

// small assignment found at http://rbwhitaker.wikidot.com/c-sharp-looping

static class ForLoops
{
    public static void Test()
    {
        while (true)
        {
            // get user input
            Console.WriteLine("How many rows of dots for the triangle?");
            string? input = Console.ReadLine();
            input = string.IsNullOrEmpty(input) ? "0" : input;
            int rowCount = Convert.ToInt32(input);
            rowCount = rowCount < 0 ? 0 :
                rowCount > 20 ? 20 : rowCount;
            Console.WriteLine();

            // draw output
            DrawTriangle(rowCount);

            // read key and reset
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void DrawTriangle(int rowCount)
    {
        int colCount = rowCount * 2 - 1;
        int middle = colCount / 2;
        
        // draw top border
        string horizontalBorder = $"+{new string('-', colCount + 2)}+";
        Console.WriteLine(horizontalBorder);

        for (int row = 0; row < rowCount; row++)
        {
            int dotCount = row * 2 + 1;
            int start = middle - dotCount / 2;
            int end = start + dotCount;

            // draw left border
            char borderChar = row % 5 == 0 ? 'S' : '|';
            Console.Write($"{borderChar} ");

            for (int col = 0; col < colCount; col++)
            {
                char dot = col >= start && col < end ? '*' : ' ';
                Console.Write(dot);
            }

            Console.Write($" {borderChar}\n");
        }

        // draw bottom border
        Console.WriteLine(horizontalBorder);
    }
}
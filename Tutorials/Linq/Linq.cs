using System.Collections.Generic;
using System.Linq;

namespace Tutorials.Linq;

static class QueryExamples
{
    public static void Test()
    {
        int[] nums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        IEnumerable<IGrouping<int, int>> numsQuery =
            from num in nums
            orderby num descending
            group num by TestKey(num) into nums2
            orderby nums2.Key
            select nums2;

        foreach (IGrouping<int, int> nGroup in numsQuery)
        {
            Console.WriteLine(nGroup.Key);
            foreach (int num in nGroup)
            {
                Console.WriteLine("{0, 5}", num);
            }
        }

        Console.WriteLine("\n---\n");

        numsQuery =
            from num in nums
            group num by (int)Math.Round((double)(num / 2));

        string output;
        foreach (IGrouping<int, int> nGroup in numsQuery)
        {
            output = string.Empty;
            foreach (int num in nGroup)
            {
                output += num;
            }
            Console.WriteLine(output);
        }
    }

    static int TestKey(int i)
    {
        double tempKey = (i + 2) / 3;
        int key = (int)Math.Round(tempKey);
        return key;
    }
}
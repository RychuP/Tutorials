namespace DirectoryTreeIteration
{
    // https://docs.microsoft.com/en-gb/dotnet/csharp/programming-guide/file-system/how-to-iterate-through-a-directory-tree

    using System;

    public class RecursiveFileSearch
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        static void Main1()
        {
            // Start with drives if you have to search the entire computer.
            string[] drives = System.Environment.GetLogicalDrives();

            foreach (string dr in drives)
            {
                System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

                // Here we skip the drive if it is not ready to be read. This
                // is not necessarily the appropriate action in all scenarios.
                if (!di.IsReady)
                {
                    Console.WriteLine("\nThe drive {0} could not be read", di.Name);
                    continue;
                }
                System.IO.DirectoryInfo rootDir = di.RootDirectory;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(rootDir.FullName);
                Console.ResetColor();
                WalkDirectoryTree(rootDir);
            }

            // Write out all the files that could not be processed.
            if (log.Count > 0)
            {
                Console.WriteLine("Files with restricted access:");
                foreach (string s in log)
                {
                    Console.WriteLine(s);
                }
            }
        }

        static void WalkDirectoryTree(System.IO.DirectoryInfo root, int depth = 1)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            string tab = new String(' ', depth);

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    Console.WriteLine($"{tab}{dirInfo.Name}");

                    // just pick one subdir as an example
                    if (dirInfo.Name == "Windows")
                    {
                        WalkDirectoryTree(dirInfo, depth + 1);
                    }

                    // Resursive call for each subdirectory.
                    //WalkDirectoryTree(dirInfo);
                }
                Console.ResetColor();

                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    Console.WriteLine($"{tab}{fi.Name}");
                }
            }
        }
    }
}
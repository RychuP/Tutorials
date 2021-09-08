namespace IO
{
    // https://docs.microsoft.com/en-gb/dotnet/csharp/programming-guide/file-system/how-to-get-information-about-files-folders-and-drives

    using System;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            // drive info
            DriveInfo di = new DriveInfo(@"C:\");
            Console.WriteLine(di.Name);
            Console.WriteLine(di.TotalSize);
            Console.WriteLine(di.AvailableFreeSpace + "\n");

            // get root dir
            DirectoryInfo root = di.RootDirectory;

            // all dir info
            DirectoryInfo[] dirs = root.GetDirectories();
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach(DirectoryInfo d in dirs)
            {
                if (d.Name.Length > 15) continue;
                Console.WriteLine("{0, -15} {1}", d.Name, d.CreationTime);
            }
            Console.ResetColor();

            // all file info
            FileInfo[] files = root.GetFiles();
            foreach(FileInfo f in files)
            {
                Console.WriteLine("{0, -15} {1, -22} {2}", f.Name, f.LastAccessTime, f.Length);
            }

            // current dir
            string currentDir = Directory.GetCurrentDirectory();
            Console.WriteLine("\nCurrent dir:\n" + currentDir);

            // files in current dir
            string[] localFiles = Directory.GetFiles(currentDir);
            Console.WriteLine("\nCurrent dir files:");
            FileInfo file;
            foreach (string f in localFiles)
            {
                try
                {
                    file = new FileInfo(f);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine(file.Name);
            }

            // change or create dir
            string dirPath = @"C:\Test";
            if (!Directory.Exists(dirPath))
            {
                try
                {
                    Directory.CreateDirectory(dirPath);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            // display dir info
            if (Directory.Exists(dirPath))
            {
                Directory.SetCurrentDirectory(dirPath);
                currentDir = Directory.GetCurrentDirectory();
                DirectoryInfo dirInfo = new DirectoryInfo(currentDir);
                Console.WriteLine("\n{0, -15}{1}", currentDir, dirInfo.CreationTime);
            }
        }
    }
}
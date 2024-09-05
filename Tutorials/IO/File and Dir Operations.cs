// https://docs.microsoft.com/en-gb/dotnet/csharp/programming-guide/file-system/how-to-get-information-about-files-folders-and-drives

using System.IO;

namespace Tutorials.IO;

static class Program
{
    public static void Test(string[] args)
    {
        // drive info
        DriveInfo di = new(@"C:\");
        Console.WriteLine(di.Name);
        Console.WriteLine(di.TotalSize);
        Console.WriteLine(di.AvailableFreeSpace + "\n");

        // get root dir
        DirectoryInfo root = di.RootDirectory;

        // all dir info
        DirectoryInfo[] dirs = root.GetDirectories();
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (DirectoryInfo d in dirs)
        {
            if (d.Name.Length > 15) continue;
            Console.WriteLine("{0, -15} {1}", d.Name, d.CreationTime);
        }
        Console.ResetColor();

        // all file info
        FileInfo[] files = root.GetFiles();
        foreach (FileInfo f in files)
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

        // create a dir if doesn't already exist
        string dirPath = @"C:\Test";
        if (!Directory.Exists(dirPath))
        {
            try
            {
                Directory.CreateDirectory(dirPath);
                Console.WriteLine("\nDirectory \"{0}\" has been created.", dirPath);
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

        // create a new file if it doesn't already exist
        string fileName = "mynewfile.txt";
        string filePath = Path.Combine(dirPath, fileName);
        if (!File.Exists(filePath))
        {
            using (FileStream fs = File.Create(filePath))
            {
                for (byte x = 0; x < 100; x++)
                {
                    fs.WriteByte(x);
                }
                Console.WriteLine("\nFile \"{0}\" has been created and populated with numbers:", fileName);
            }
        }
        else
        {
            Console.WriteLine("\nFile \"{0}\" already exists:", fileName);
        }

        // read the above file
        try
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            foreach (byte b in bytes)
            {
                Console.Write("{0:00} ", b);
            }
            Console.WriteLine();
        }
        catch (IOException e)
        {
            Console.WriteLine("\n" + e.Message);
        }

        // copy file
        string targetPath = Path.Combine(dirPath, "subfolder");
        string fileTargetPath = Path.Combine(targetPath, fileName);
        Directory.CreateDirectory(targetPath);


        if (File.Exists(fileTargetPath))
        {
            Console.WriteLine("\nFile \"{0}\" appears to have already been copied to \"{1}\".", fileName, targetPath);
        }
        else
        {
            File.Copy(filePath, fileTargetPath);
            if (File.Exists(fileTargetPath))
            {
                Console.WriteLine("\nOperation \"Copying file\" successful!");
            }
        }

        Console.WriteLine();
    }
}
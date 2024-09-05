using System;
using System.IO;

namespace Tutorials.Delegates;
// common patterns for delegates https://docs.microsoft.com/en-gb/dotnet/csharp/delegates-patterns
// a sample logger class

static class DelegatePatters
{
    public static void Test()
    {
        // test for for null WriteMessage delegate
        Logger.LogMessage(Severity.Critical, "foo", "bar");

        // attach handlers
        var file = new FileLogger("log.txt");
        Logger.WriteMessage += LoggingMethods.LogToConsole;

        // mid severity
        Logger.LogMessage(Severity.Information, "trutu", "tutu");
        Logger.LogMessage(Severity.Critical, "foo", "bar");

        // low severity
        Logger.LogLevel = Severity.Trace;
        Logger.LogMessage(Severity.Information, "trutu", "tutu");
        Logger.LogMessage(Severity.Critical, "wel", "come");
    }

    static class Logger
    {
        public static Action<string>? WriteMessage;

        public static Severity LogLevel { get; set; } = Severity.Warning;

        public static void LogMessage(Severity s, string component, string msg)
        {
            if (s < LogLevel)
            {
                Console.WriteLine("Unimportant!");
                return;
            }

            var outputMsg = $"{DateTime.Now}\t{s}\t{component}\t{msg}";
            WriteMessage?.Invoke(outputMsg);
        }
    }

    static class LoggingMethods
    {
        public static void LogToConsole(string message)
        {
            Console.Error.WriteLine(message);
        }
    }

    class FileLogger
    {
        private readonly string logPath;
        public FileLogger(string path)
        {
            logPath = path;
            Logger.WriteMessage += LogMessage;
            File.WriteAllText(path, String.Empty);
        }

        public void DetachLog() => Logger.WriteMessage -= LogMessage;
        // make sure this can't throw.
        private void LogMessage(string msg)
        {
            try
            {
                using var log = File.AppendText(logPath);
                log.WriteLine(msg);
                log.Flush();
            }
            catch (Exception)
            {
                // Hmm. We caught an exception while
                // logging. We can't really log the
                // problem (since it's the log that's failing).
                // So, while normally, catching an exception
                // and doing nothing isn't wise, it's really the
                // only reasonable option here.
            }
        }
    }

    enum Severity
    {
        Verbose,
        Trace,
        Information,
        Warning,
        Error,
        Critical
    }
}
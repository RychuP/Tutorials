﻿// Taken from https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-teleprompter
// Basics of the Task-based Asynchronous Programming in .NET

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Tutorials.Threading;

static class AsyncAwait
{
    public static void Test()
    {
        RunTeleprompter().Wait();
    }

    static async Task RunTeleprompter()
    {
        var config = new TelePrompterConfig();
        var displayTask = ShowTeleprompter(config);

        var speedTask = GetInput(config);
        await Task.WhenAny(displayTask, speedTask);
    }

    static async Task ShowTeleprompter(TelePrompterConfig config)
    {
        var words = ReadFrom("Resources\\sampleQuotes.txt");
        foreach (var word in words)
        {
            Console.Write(word);
            if (!string.IsNullOrWhiteSpace(word))
            {
                await Task.Delay(config.DelayInMilliseconds);
            }
        }
        config.SetDone();
    }

    static async Task GetInput(TelePrompterConfig config)
    {
        Action work = () =>
        {
            do
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == '>')
                    config.UpdateDelay(-10);
                else if (key.KeyChar == '<')
                    config.UpdateDelay(10);
                else if (key.KeyChar == 'X' || key.KeyChar == 'x')
                    config.SetDone();
            } while (!config.Done);
        };
        await Task.Run(work);
    }

    static IEnumerable<string> ReadFrom(string file)
    {
        string line;
        using (var reader = File.OpenText(file))
        {
            while ((line = reader.ReadLine()) != null)
            {
                var words = line.Split(' ');
                var lineLength = 0;
                foreach (var word in words)
                {
                    yield return word + " ";
                    lineLength += word.Length + 1;
                    if (lineLength > 70)
                    {
                        yield return Environment.NewLine;
                        lineLength = 0;
                    }
                }
                yield return Environment.NewLine;
            }
        }
    }
}

class TelePrompterConfig
{
    public int DelayInMilliseconds { get; private set; } = 250;

    public void UpdateDelay(int increment) // negative to speed up
    {
        var newDelay = Math.Min(DelayInMilliseconds + increment, 1000);
        newDelay = Math.Max(newDelay, 20);
        DelayInMilliseconds = newDelay;
    }

    public bool Done { get; private set; }

    public void SetDone()
    {
        Done = true;
    }
}
using System;
using BashDotNet;
using System.Diagnostics;

namespace BashDemo
{
    internal static class MainClass
    {
        private static Library library;

        static MainClass()
        {
            library = new Library(1,
                new Command(
                    "echo", new[] { "text" },
                    new[] { 
                        new Option("color", "color", 'c'),
                        new Option("header", "header", 'h'),
                    },
                    (args, opts) =>
                    {
                        switch (opts["color"])
                        {
                            case "red":
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;

                            case "green":
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;

                            case "blue":
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                        }

                        Console.WriteLine(args["text"]);
                        if (opts["header"] == "true")
                        {
                            Console.WriteLine(new string('=', args["text"].Length) + '\n');
                        }

                        // TODO types
                        if (opts["color"] != "false")
                        {
                            Console.ResetColor();
                        }
                    }));
        }

        private static void Main(string[] _args)
        {
            while (true)
            {
                if (!library.TryExecute(Console.ReadLine()))
                {
                    Console.WriteLine("wrong command");
                }
            }
        }
    }
}

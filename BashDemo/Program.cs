using System;
using BashDotNet;

namespace BashDemo
{
    internal static class MainClass
    {
        private static Interpreter interpreter;

        static MainClass()
        {
            interpreter = new Interpreter(2,
                new Command(
                    "echo write", new[] { "text" },
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
                if (!interpreter.TryExecute(Console.ReadLine()))
                {
                    Console.WriteLine("wrong command");
                }
            }
        }
    }
}

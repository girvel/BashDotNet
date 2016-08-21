using System;
using BashDotNet;

namespace BashDemo
{
    internal static class MainClass
    {
        private static Interpreter interpreter;

        static MainClass()
        {
            interpreter = new Interpreter(
                new Command(
                    "echo", new[] { "text" },
                    new[] { new Option("red", "red", 'r') },
                    (args, opts) =>
                    {
                        if (opts["red"] == "true")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        Console.WriteLine(args["text"]);

                        if (opts["red"] == "true")
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

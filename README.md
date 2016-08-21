# Bash.NET

Allows to create commands:

```C#
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
```

And execute them:

```C#
if (!interpreter.TryExecute(Console.ReadLine()))
{
    Console.WriteLine("wrong command");
}
```

Output:

```bash
$ echo "hello world" -r
hello world
```

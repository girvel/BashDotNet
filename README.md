# Bash.NET

![Screenshot](screenshot.png)

[Download](https://github.com/girvel/BashDotNet/raw/master/BashDotNet/bin/Release/BashDotNet.dll)

Allows to create commands:

```C#
interpreter = new Interpreter(
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
$ echo "hello world" -hc=red
hello world
==========
```

Also you can create commands with longer names:

```C#
interpreter = new Interpreter(2,
    new Command(
        "echo write", new[] { "text" },
// ...
```

![Screenshot 2](screenshot_longnames.png)


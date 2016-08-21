using System;
using System.Collections.Generic;
using System.Linq;

namespace BashDotNet
{
    public class Interpreter
    {
        // TODO name: 1 or 2 words : bool
        public List<Command> Commands { get; set; }

        public Interpreter(params Command[] commands)
        {
            Commands = new List<Command>(commands);
        }

        public bool TryExecute(string stringCommand)
        {
            var elements = stringCommand.ParseCommand();

            var command = Commands.Find(c => c.Name == elements[0]);
            if (command == null)
            {
                return false;
            }

            var options = command.GenerateDefaultOptions();
            var positionalArgs = new Dictionary<string, string>();

            if (elements.Length < command.PositionalArguments.Length)
            {
                return false;
            }

            for (var i = 1; i < elements.Length; i++)
            {
                var e = elements[i];
                if (i <= command.PositionalArguments.Length)
                {
                    positionalArgs[command.PositionalArguments[i - 1]] = e;
                }
                else if (e.StartsWith("-"))
                {
                    e = e.Substring(1);
                    if (e.StartsWith("-"))
                    {
                        e = e.Substring(1);
                        var option = command.Options.FirstOrDefault(o => o.LongName == e);

                        if (option == null)
                        {
                            return false;
                        }

                        options[option.Name] = "true";
                    }
                    else
                    {
                        foreach (var c in e)
                        {
                            var option = command.Options.FirstOrDefault(o => o.ShortName == c);

                            if (option == null)
                            {
                                return false;
                            }

                            options[option.Name] = "true";
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            command.Execute(positionalArgs, options);

            return true;
        }
    }
}


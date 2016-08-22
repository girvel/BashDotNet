using System;
using System.Collections.Generic;
using System.Linq;
using EnumerableExtensions;

namespace BashDotNet
{
    public class Library
    {
        /// <summary>
        /// How many words it must interprete as a name
        /// </summary>
        public int NameLength { get; set; }

        /// <summary>
        /// List of all possible commands, which can be used from console
        /// </summary>
        /// <value>The commands.</value>
        public List<Command> Commands { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BashDotNet.Interpreter"/> class.
        /// </summary>
        public Library(int nameLength, params Command[] commands)
        {
            Commands = new List<Command>(commands);
            NameLength = nameLength;
        }

        /// <summary>
        /// Tries to execute command
        /// </summary>
        public bool TryExecute(string stringCommand)
        {
            // TODO change algorythm
            var elements = stringCommand.ParseCommand(' ');

            if (elements.Length < NameLength)
            {
                return false;
            }

            var name = elements.Slice(0, NameLength - 1)
                .Aggregate((sum, e) => sum + " " + e);

            var command = Commands.Find(c => c.Name == name);

            if (command == null
                || elements.Length < NameLength + command.PositionalArguments.Length)
            {
                return false;
            }

            var options = command.GenerateDefaultOptions();
            var positionalArgs = new Dictionary<string, string>();

            for (var i = 0; i < command.PositionalArguments.Length; i++)
            {
                positionalArgs[command.PositionalArguments[i]] = elements[i + NameLength];
            }

            for (var i = NameLength + positionalArgs.Count; i < elements.Length; i++)
            {
                var element = elements[i];

                Option option = null;
                string value = "";
                if (element.StartsWith("--"))
                {
                    element = element.Substring(2);

                    var parts = element.ParseCommand('=');
                    switch (parts.Length)
                    {
                        case 1:
                            option = command.Options.FirstOrDefault(o => o.LongName == element);
                            break;

                        case 2:
                            option = command.Options.FirstOrDefault(
                                o => o.LongName == parts[0]);
                            value = parts[1];
                            break;

                        default:
                            return false;
                    }
                }
                else if (element.StartsWith("-"))
                {
                    element = element.Substring(1);

                    if (element.Length == 0)
                    {
                        return false;
                    }

                    var parts = element.ParseCommand('=');

                    if (parts.Length > 2)
                    {
                        return false;
                    }

                    var chars = parts[0];

                    for (var i2 = 0; i2 < chars.Length - 1; i2++)
                    {
                        var character = chars[i2];
                        option = command.Options.FirstOrDefault(o => o.ShortName == character);

                        // TODO return position with error
                        if (option == null)
                        {
                            return false;
                        }

                        // TODO true to const
                        options[option.Name] = "true";
                    }

                    switch (parts.Length)
                    {
                        case 1:
                            option = command.Options.FirstOrDefault(o => o.ShortName == chars.Last());
                            break;

                        case 2:
                            option = command.Options.FirstOrDefault(
                                o => o.ShortName == chars.Last());
                            value = parts[1];
                            break;
                    }

                }
                else
                {
                    return false;
                }

                string optionName;
                if (option == null)
                {
                    return false;
                }
                else
                {
                    optionName = option.Name;
                }

                if (value == "")
                {
                    if (i + 1 < elements.Length && !elements[i + 1].StartsWith("-"))
                    {
                        options[optionName] = elements[++i];
                    }
                    else
                    {
                        options[optionName] = "true";
                    }
                }
                else
                {
                    options[optionName] = value;
                }
            }
            

            command.Execute(positionalArgs, options);

            return true;
        }
    }
}


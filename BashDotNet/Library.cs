﻿using System;
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

            var name = elements.Slice(0, NameLength).Aggregate(
                (sum, e) => sum + " " + e);

            name = name.Substring(0, name.Length - 1);

            var command = Commands.Find(c => c.Name == name);
            if (command == null)
            {
                return false;
            }

            var options = command.GenerateDefaultOptions();
            var positionalArgs = new Dictionary<string, string>();

            if (elements.Length - NameLength < command.PositionalArguments.Length)
            {
                return false;
            }

            for (var i = NameLength; i < elements.Length; i++)
            {
                var e = elements[i];
                if (i - NameLength < command.PositionalArguments.Length)
                {
                    positionalArgs[command.PositionalArguments[i - NameLength]] = e;
                }
                else if (e.StartsWith("-"))
                {
                    e = e.Substring(1);
                    string value;

                    if (e.StartsWith("-"))
                    {
                        e = e.Substring(1);

                        if (!_getOption(elements, ref i, ref e, out value))
                        {
                            return false;
                        }

                        var option = command.Options.FirstOrDefault(o => o.LongName == e);

                        if (option == null)
                        {
                            return false;
                        }

                        options[option.Name] = value;
                    }
                    else
                    {
                        for (var i2 = 0; i2 < e.Length; i2++)
                        {
                            string c;
                            if (i2 + 1 < e.Length && e[i2 + 1] == '=')
                            {
                                c = e.Substring(i2);
                                i2 = e.Length;
                            }
                            else
                            {
                                c = e[i2].ToString();
                            }

                            if (!_getOption(elements, ref i, ref c, out value))
                            {
                                return false;
                            }

                            var option = command.Options.FirstOrDefault(o => o.ShortName.ToString() == c);

                            if (option == null)
                            {
                                return false;
                            }

                            options[option.Name] = value;
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

        private bool _getOption(string[] elements, ref int i, ref string option, out string value)
        {
            if (elements[i][elements[i].Length - 1] == option[option.Length - 1]
                && i + 1 < elements.Length 
                && !elements[i + 1].StartsWith("-"))
            {
                value = elements[i + 1];
                i++;
            }
            else
            {
                var parts = option.ParseCommand('=');

                if (parts.Length == 1)
                {
                    value = "true";
                }
                else
                {
                    if (parts.Length > 2)
                    {
                        value = "";
                        return false;
                    }
                    else
                    {
                        option = parts[0];
                        value = parts[1];
                    }
                }
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace BashDotNet
{
    public class Command
    {
        public string Name { get; }

        public string[] PositionalArguments { get; }

        public Option[] Options { get; }

        public Executor Execute { get; }



        public delegate void Executor(
            Dictionary<string, string> positionalArguments, 
            Dictionary<string, string> options);



        public Command(
            string name, string[] positionalArguments, Option[] options, Executor execute)
        {
            Name = name;
            PositionalArguments = positionalArguments;
            Options = options;
            Execute = execute;
        }



        public Dictionary<string, string> GenerateDefaultOptions()
        {
            var result = new Dictionary<string, string>();

            foreach (var option in Options)
            {
                result[option.Name] = "false";
            }

            return result;
        }
    }
}


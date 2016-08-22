using System;
using System.Collections.Generic;
using System.Linq;

namespace BashDotNet
{
    public class Command
    {
        /// <summary>
        /// Unical command's name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Array of command's positional arguments
        /// </summary>
        public string[] PositionalArguments { get; }

        /// <summary>
        /// Array of all possible options
        /// </summary>
        public Option[] Options { get; }

        /// <summary>
        /// Execute command with specified arguments and options
        /// </summary>
        public Executor Execute { get; }



        /// <summary>
        /// Command execution delegate
        /// </summary>
        public delegate void Executor(
            Dictionary<string, string> positionalArguments, 
            Dictionary<string, string> options);



        /// <summary>
        /// Initializes a new instance of the <see cref="BashDotNet.Command"/> class.
        /// </summary>
        public Command(
            string name, string[] positionalArguments, Option[] options, Executor execute)
        {
            Name = name;
            PositionalArguments = positionalArguments;
            Options = options;
            Execute = execute;
        }



        internal Dictionary<string, string> GenerateDefaultOptions()
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


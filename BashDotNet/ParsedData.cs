using System;
using System.Collections.Generic;

namespace BashDotNet
{
    public class ParsedData
    {
        public Dictionary<string, string> PositionalArguments { get; }

        public Dictionary<string, string> Options { get; }

        public ParsedData(
            Dictionary<string, string> positionalArguments, 
            Dictionary<string, string> options)
        {
            PositionalArguments = positionalArguments;
            Options = options;
        }

        public ParsedData() 
        {
            PositionalArguments = new Dictionary<string, string>();
            Options = new Dictionary<string, string>();
        }
    }
}


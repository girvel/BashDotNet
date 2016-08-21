using System;

namespace BashDotNet
{
    public class Option
    {
        public string Name { get; }

        public string LongName { get; }

        public char ShortName { get; }



        public Option(string name, string longName, char shortName)
        {
            Name = name;
            LongName = longName;
            ShortName = shortName;
        }
    }
}


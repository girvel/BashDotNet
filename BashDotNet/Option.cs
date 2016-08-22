using System;

namespace BashDotNet
{
    /// <summary>
    /// Represents option, which uses in command definition
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Unical option name for using as a key in option dictionary
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Long option name (--&lt;LongName&gt;)
        /// </summary>
        public string LongName { get; }

        /// <summary>
        /// Short option name (-&lt;ShortName&gt;)
        /// </summary>
        public char ShortName { get; }



        public Option(string name, string longName, char shortName)
        {
            Name = name;
            LongName = longName;
            ShortName = shortName;
        }
    }
}


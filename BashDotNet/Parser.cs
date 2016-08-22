using System;
using System.Collections.Generic;

namespace BashDotNet
{
    internal static class Parser
    {
        internal static string[] ParseCommand(this string str, char separator)
        {
            var result = new List<string>();

            var currentElement = "";

            var quotes = false;
            var doubleQuote = false;
            for (var i = 0; i < str.Length; i++)
            {
                var prevCharacter = i == 0 ? str[0] : str[i - 1];
                var character = str[i];

                if (prevCharacter != '\\')
                {
                    if (character == separator && !quotes)
                    {
                        result.Add(currentElement);
                        currentElement = "";
                    }
                    // TODO quotetype
                    else if ((!quotes || doubleQuote) && character == '"')
                    {
                        quotes ^= true;
                        doubleQuote = true;
                    }
                    else if ((!quotes || !doubleQuote) && character == '\'')
                    {
                        quotes ^= true;
                        doubleQuote = false;
                    }
                    else if (character != '\\')
                    {
                        currentElement += character;
                    }
                }
                else
                {
                    currentElement += character;
                }
            }

            result.Add(currentElement);

            return result.ToArray();
        }   
    }
}


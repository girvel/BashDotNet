using System;
using System.Collections.Generic;

namespace BashDotNet
{
    internal static class Parser
    {
        internal static string[] ParseCommand(this string str)
        {
            var result = new List<string>();

            var currentElement = "";

            var quotes = false;
            for (var i = 0; i < str.Length; i++)
            {
                var prevCharacter = i == 0 ? str[0] : str[i - 1];
                var character = str[i];

                switch (character)
                {
                    case ' ':
                        if (prevCharacter == '\\' || quotes)
                        {
                            goto default;
                        }
                        result.Add(currentElement);
                        currentElement = "";
                        break;

                    case '"':
                    case '\'':
                        if (prevCharacter == '\\')
                        {
                            goto default;
                        }
                        quotes ^= true;
                        break;

                    case '\\':
                        if (prevCharacter == '\\')
                        {
                            goto default;
                        }
                        break;

                    default:
                        currentElement += character;
                        break;
                }
            }

            result.Add(currentElement);

            return result.ToArray();
        }   
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Dto
{
    public class DefinitionBaseClass
    {
        // Convert the string to camel case.
        public static string ToCamelCase(string input)
        {
            // If there are 0 or 1 characters, just return the string.
            if (input == null || input.Length < 2)
                return input;

            input = input.Replace("_", " ");

            // Split the string into words.
            string[] words = input.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            var result = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                result.Append(words[i].Substring(0, 1).ToUpper());
                result.Append(words[i].Substring(1));
            }
            
            return result.ToString();
        }

        public static string ToCamelCaseStartLower(string input)
        {
            string result = ToCamelCase(input);
            return $"{result.Substring(0, 1).ToLower()}{result.Substring(1)}";
        }

    }
}

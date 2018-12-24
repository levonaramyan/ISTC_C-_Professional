using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Given a string input: return true, if the set of brackets are written in correct form and order.

namespace CheckBracketSets
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing and printing the result
            string test = "abcde(f<asa{f}sf>)";
            Console.WriteLine(CheckAllBrackets(test));
            Console.ReadKey();
        }

        // Checks whether the set of brackets in the input string is in correct order
        public static bool CheckAllBrackets(string input)
        {
            // Initializing a jagged array of bracket sets 
            string[][] bracks = new string[4][];
            bracks[0] = new string[] { "(", ")" };
            bracks[1] = new string[] { "[", "]" };
            bracks[2] = new string[] { "<", ">" };
            bracks[3] = new string[] { "{", "}" };

            // Constructing a Regex pattern, which will check if the inner text doesn't contain any bracket
            string noInnerBracks = "";
            foreach (string[] b in bracks) noInnerBracks += @"^\" + b[0] + @"^\" + b[1];
            noInnerBracks = $@"[{noInnerBracks}]*";

            // Constructing 4 types of Regex patterns for all of types of brackets
            List<string> patterns = new List<string>();
            foreach (string[] b in bracks) patterns.Add($@"\{b[0]}{noInnerBracks}\{b[1]}");

            // iteratively removing <open-bracket><non-brackets><close-bracket> cases, until no matches found
            // or until no brackets left
            int len;
            while (ContainsBracket(input))
            {
                len = input.Length;
                foreach (string pat in patterns) input = Regex.Replace(input, pat, "");
                if (len == input.Length) break;
            }

            // If after the procedure, the remaining string contains brackets, then return false, else return true
            return ContainsBracket(input)? false: true;
        }

        // Checks whether the input string contains any bracket
        private static bool ContainsBracket(string input)
        {
            bool contains = false;
            string bracks = "()[]{}<>";
            foreach(char c in bracks)
                if (input.Contains(c))
                {
                    contains = true;
                    break;
                }
            return contains;
        }
    }
}

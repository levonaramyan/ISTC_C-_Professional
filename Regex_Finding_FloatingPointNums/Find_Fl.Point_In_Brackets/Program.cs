using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Given a string input: Return a List of floating point numbers, which occur in any type of brackets.

namespace Find_Fl.Point_In_Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "asfvc(2.1) dsgdsg [4.78] {9...4}  (0.15) <0.00>{5.6} ";
            List<double> res = FindFlPointInBracks(test);

            foreach (double a in res) Console.WriteLine(a);
            Console.ReadKey();
        }

        // Returns a List of all of the floating point numbers, which occur in any type of brackets
        public static List<double> FindFlPointInBracks(string input)
        {
            string[] bracks = new string[] { "()", "[]", "<>", "{}" };
            List<double> res = new List<double>();

            foreach(string br in bracks)
            {
                string pattern = $@"\{br[0]}(\d+[.]\d+)\{br[1]}";
                MatchCollection matches = Regex.Matches(input, pattern);
                foreach (Match m in matches)
                {
                    GroupCollection data = m.Groups;
                    for (int i = 1; i < data.Count; i++)
                        res.Add(double.Parse(data[i].Value));
                }
            }

            return res;
        }
    }
}

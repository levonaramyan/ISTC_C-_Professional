using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ParseCompaniesAndJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            string testUrl = "https://staff.am/en/company/betconstruct";
            string testJub = "https://staff.am/en/senior-quality-engineer";

            Company betconstruct = Parser.GetCompanyWithJobs(testUrl);
            betconstruct.PrintCompanyInfo();

            foreach (ActiveJob job in betconstruct.ActiveJobs) Console.WriteLine(job.Title);
            Console.ReadKey();
        }
    }
}

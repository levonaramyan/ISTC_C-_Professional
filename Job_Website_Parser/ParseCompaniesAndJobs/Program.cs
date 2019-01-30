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
            string testUrl = "https://staff.am/en/companies";
            string testJub = "https://staff.am/en/senior-quality-engineer";
            string sep = new string('*', 75);

            List<Company> companies = Parser.GetAllCompaniesWithTheirJobs(testUrl);
            foreach (Company company in companies)
            {
                Console.WriteLine(sep);
                Console.WriteLine(company.Name);
                Console.WriteLine();
                foreach (ActiveJob job in company.ActiveJobs)
                {
                    Console.WriteLine($"\t\t{job.Title}");
                }
            }

            Console.ReadKey();
        }
    }
}

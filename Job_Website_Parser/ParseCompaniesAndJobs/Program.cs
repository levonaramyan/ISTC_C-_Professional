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
            string testUrl = "https://staff.am/en/company/synopsys-armenia";
            string testJub = "https://staff.am/en/senior-quality-engineer";
            //Company company = Parser.GetCompanyInfo(testUrl);
            //Console.WriteLine(company.Industry);
            //Console.WriteLine(company.DateOfFoundation);
            //Console.WriteLine(company.Type);
            //Console.WriteLine(Parser.GetContentCollectionFromURL(testUrl,"//*[@class=\"company-job-history\"]/span").InnerText);

            ActiveJob job = Parser.GetJobInfo(testJub);
            job.PrintJobInfo();

            Company company = Parser.GetCompanyInfo(testUrl);
            company.PrintCompanyInfo();
            Console.ReadKey();
        }
    }
}

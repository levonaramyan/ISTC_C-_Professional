using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ParseCompaniesAndJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            string testUrl = "https://staff.am/en/sales-and-service-specialist-5";
            //string xpath = "/html/body/div[2]/div[3]/div/div[4]";
            //string xpath = "//*[@class=\"additional-information information_application_block\"]/p";
            //HtmlNodeCollection nodesInfo = Parser.GetContentCollection(testUrl,xpath);
            //foreach (var a in nodesInfo) Console.WriteLine(a.InnerText);

            ActiveJob job1 = Parser.GetJobInfo(testUrl);

            Console.WriteLine(job1.Title);
            Console.WriteLine(job1.RequiredQualifications);
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ParseCompaniesAndJobs
{
    static class Parser
    {
        public static string GetHTMLCode(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }

        public static HtmlNode GetContentCollection(string url, string xPath)
        {
            HtmlDocument code = new HtmlDocument();
            code.LoadHtml(GetHTMLCode(url));
            return code.DocumentNode.SelectSingleNode(xPath);
        }

        public static ActiveJob GetJobInfo(string url)
        {            
            string pathTitle = "//*[@class=\"col-lg-8\"]/h2";
            string pathEmploymentTerm = "//*[@class=\"col-lg-6 job-info\"][1]/p[1]";
            string regexTerms = "[^:]*: (.*)";
            string pathJobType = "//*[@class=\"col-lg-6 job-info\"][2]/p[1]";
            string pathCategory = "//*[@class=\"col-lg-6 job-info\"][1]/p[2]";
            string pathLocation = "//*[@class=\"col-lg-6 job-info\"][2]/p[2]";
            string pathJobDescription = "//*[@class=\"job-list-content-desc\"]/p[1]";
            string pathJobResponsibilities = "//*[@class=\"job-list-content-desc\"]/p[2]";
            string pathRequiredQualifications = "//*[@class=\"job-list-content-desc\"]/p[3]";
            string pathAdditionalInformation = "//*[@class=\"additional-information information_application_block\"]/p[1]";
            ActiveJob job = new ActiveJob();
            job.Url = url;

            job.Title = GetContentCollection(url,pathTitle).InnerText;
            job.EmploymentTerm = GetContentCollection(url,pathEmploymentTerm).InnerText;
            job.JobType = GetContentCollection(url,pathJobType).InnerText;
            job.Category = GetContentCollection(url,pathCategory).InnerText;
            job.Location = GetContentCollection(url,pathLocation).InnerText;
            job.JobDescription = GetContentCollection(url,pathJobDescription).InnerText;
            job.JobResponsibilities = GetContentCollection(url,pathJobResponsibilities).InnerText;
            job.RequiredQualifications = GetContentCollection(url,pathRequiredQualifications).InnerText;
            job.AdditionalInformation = GetContentCollection(url,pathAdditionalInformation).InnerText;

            return job;
        }
    }
}

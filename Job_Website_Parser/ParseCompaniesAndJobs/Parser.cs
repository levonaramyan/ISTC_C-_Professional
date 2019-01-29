using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ParseCompaniesAndJobs
{
    static class Parser
    {
        private static string GetHTMLCode(string Url)
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

        public static HtmlNode GetContentFromURL(string url, string xPath)
        {
            HtmlDocument code = new HtmlDocument();
            code.LoadHtml(GetHTMLCode(url));
            return code.DocumentNode.SelectSingleNode(xPath);
        }

        private static string GetContentFromHtml(string html, string xPath)
        {
            HtmlDocument code = new HtmlDocument();
            code.LoadHtml(html);
            try
            {
                return code.DocumentNode.SelectSingleNode(xPath).InnerText;
            }
            catch
            {
                return null;
            }
        }

        public static ActiveJob GetJobInfo(string url)
        {            
            string pathTitle = "//*[@class=\"col-lg-8\"]/h2";
            string pathEmploymentTerm = "//*[@class=\"col-lg-6 job-info\"][1]/p[1]";
            string regexTerms = @"[^:]*:\s+(.*)";
            string pathJobType = "//*[@class=\"col-lg-6 job-info\"][2]/p[1]";
            string pathCategory = "//*[@class=\"col-lg-6 job-info\"][1]/p[2]";
            string pathLocation = "//*[@class=\"col-lg-6 job-info\"][2]/p[2]";
            string pathJobDescription = "//*[@class=\"job-list-content-desc\"]//p[2]";
            string pathJobResponsibilities = "//*[@class=\"job-list-content-desc\"]//ul[1]";
            string pathRequiredQualifications = "//*[@class=\"job-list-content-desc\"]//ul[2]";
            string pathAdditionalInformation = "//*[@class=\"additional-information information_application_block\"]/p[1]";
            ActiveJob job = new ActiveJob();
            job.Url = url;

            try
            {
                string html = GetHTMLCode(url);
                job.Title = GetContentFromHtml(html, pathTitle);

                job.EmploymentTerm = GetContentFromHtml(html, pathEmploymentTerm);
                job.EmploymentTerm = job.EmploymentTerm != null ?
                    Regex.Matches(job.EmploymentTerm, regexTerms)[0].Groups[1].Value : null;

                job.JobType = GetContentFromHtml(html, pathJobType);
                job.JobType = job.JobType != null?
                    Regex.Matches(job.JobType, regexTerms)[0].Groups[1].Value : null;

                job.Category = GetContentFromHtml(html, pathCategory);
                job.Category = job.Category != null?
                    Regex.Matches(job.Category, regexTerms)[0].Groups[1].Value : null;

                job.Location = GetContentFromHtml(html, pathLocation);
                job.Location = job.Location != null?
                    Regex.Matches(job.Location, regexTerms)[0].Groups[1].Value : null;

                job.JobDescription = GetContentFromHtml(html, pathJobDescription);

                job.JobResponsibilities = GetContentFromHtml(html, pathJobResponsibilities);

                job.RequiredQualifications = GetContentFromHtml(html, pathRequiredQualifications);
                job.AdditionalInformation = GetContentFromHtml(html, pathAdditionalInformation);
            }
            catch (Exception)
            {
                Console.WriteLine("The mentioned url is not found!");
            }

            return job;
        }

        public static Company GetCompanyInfo(string url)
        {
            Company company = new Company();
            string pathName = "//*[@class=\"company-title-views col-lg-10 col-md-10 col-sm-10\"]/h1";
            string pathAbout = "//*[@class=\"col-lg-8 col-md-8 about-text\"]";
            string pathIndustry = "//*[@class=\"professional-skills-description\"][1]";
            string pathType = "//*[@class=\"professional-skills-description\"][2]";
            string pathNumberOfEmployees = "//*[@class=\"professional-skills-description\"][3]";
            string pathDateOfFoundation = "//*[@class=\"professional-skills-description\"][4]";
            string regexTerms = @"[^:]*:\s+(.*)";
            string regexDateOfFoundation = @"\D*(\d+)\D*";
            List<string> ActiveJobsUrl;
            string pathJobsCount = "//*[@class=\"company-active-job  margin-r-2\"]/span";
            string pathJobsHistory = "//*[@class=\"company-job-history\"]/span";

            string html = GetHTMLCode(url);
            company.Name = GetContentFromHtml(html, pathName);
            company.About = GetContentFromHtml(html, pathAbout);

            company.Industry = GetContentFromHtml(html, pathIndustry);
            company.Industry = company.Industry != null?
                Regex.Matches(company.Industry, regexTerms)[0].Groups[1].Value : null;

            company.Type = GetContentFromHtml(html, pathType);
            company.Type = company.Type != null?
                Regex.Matches(company.Type, regexTerms)[0].Groups[1].Value : null;

            string num = GetContentFromHtml(html, pathNumberOfEmployees);
            company.NumberOfEmployees = num != null?
                int.Parse(Regex.Matches(num, regexDateOfFoundation)[0].Groups[1].Value) : 0;

            string date = GetContentFromHtml(html, pathDateOfFoundation);
            company.DateOfFoundation = date != null?
                int.Parse(Regex.Matches(date,regexDateOfFoundation)[0].Groups[1].Value) : 0;

            string jobHist = GetContentFromHtml(html, pathJobsHistory);
            company.JobsHistory = jobHist != null? int.Parse(jobHist) : 0;

            return company;
        }
    }
}

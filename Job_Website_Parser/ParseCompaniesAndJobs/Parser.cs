using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Threading.Tasks;

namespace ParseCompaniesAndJobs
{
    static class Parser
    {
        public static string LoadingStatus = "Is Completed";

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

        private static HtmlNode GetContentFromURL(string url, string xPath)
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
            ActiveJob job = new ActiveJob();
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

        private static List<string> GetJobsURLs(string url)
        {
            List<string> jobURLs = new List<string>(0);
            string html = GetHTMLCode(url);
            HtmlDocument code = new HtmlDocument();
            code.LoadHtml(html);
            string xPath = "//*[@class=\"col-sm-6 pl5\"]/a";
            string urlLeft = url.Substring(0, url.IndexOf("/en/company"));

            try
            {
                HtmlNodeCollection urlNodes = code.DocumentNode.SelectNodes(xPath);
                foreach (HtmlNode jobNode in urlNodes)
                    jobURLs.Add(urlLeft + jobNode.GetAttributeValue("href", ""));
            }
            catch { }

            return jobURLs;
        }

        public static Company GetCompanyWithJobs(string url)
        {
            Company company = GetCompanyInfo(url);
            List<string> jobUrls = GetJobsURLs(url);

            foreach (string jobLink in jobUrls)
                company.ActiveJobs.Add(GetJobInfo(jobLink));

            return company;
        }

        private static string Scroll(string url)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-images");
            ChromeDriver chromeDriver = new ChromeDriver(chromeOptions);
            chromeDriver.Navigate().GoToUrl(url);
            long scrollHeight = 0;
            do
            {
                IJavaScriptExecutor js = chromeDriver;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight) break;
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(2000);
                }
            } while (true);

            string pageSource = chromeDriver.PageSource;

            chromeDriver.Close();

            return pageSource;
        }

        private static List<string> GetAllCompanyURLs(string url)
        {
            List<string> companyURLs = new List<string>(0);
            string html = Scroll(url);
            HtmlDocument code = new HtmlDocument();
            code.LoadHtml(html);
            string xPath = "//*[@class=\"company-action company_inner_right\"]/a";
            string urlLeft = url.Substring(0, url.IndexOf("/en/companies"));

            HtmlNodeCollection urlNodes = code.DocumentNode.SelectNodes(xPath);
            foreach (HtmlNode companyNode in urlNodes)
                companyURLs.Add(urlLeft + companyNode.GetAttributeValue("href", ""));

            return companyURLs;
        }

        public static List<Company> GetAllCompaniesWithTheirJobs(string url)
        {
            List<Company> companies = new List<Company>(0);
            List<string> compURLs = GetAllCompanyURLs(url);
            foreach (string companyURL in compURLs)
                companies.Add(GetCompanyWithJobs(companyURL));

            LoadingStatus = "Is completed!!!";

            return companies;
        }

        public static async Task<List<Company>> GetAllCompaniesWithTheirJobsAsync(string url)
        {
            return await Task.Run(() => GetAllCompaniesWithTheirJobs(url));
        }

        public static void Loading()
        {

        }
    }
}

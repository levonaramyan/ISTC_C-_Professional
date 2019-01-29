using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCompaniesAndJobs
{
    class ActiveJob
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public CompanyInfo JobCompany {get; set; }
        public string EmploymentTerm { get; set; }
        public string JobType { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string JobDescription { get; set; }
        public string JobResponsibilities { get; set; }
        public string RequiredQualifications { get; set; }
        public string AdditionalInformation { get; set; }

        public ActiveJob()
        {
            
        }

        public ActiveJob(string url, CompanyInfo company)
        {
            JobCompany = company;
            ParseJobInfo(url);
        }

        protected void ParseJobInfo(string url)
        {
            
        }
    }
}

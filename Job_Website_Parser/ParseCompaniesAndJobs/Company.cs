using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCompaniesAndJobs
{
    class CompanyInfo
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string Industry { get; set; }
        public string Type { get; set; }
        public int NumberOfEmployees { get; set; }
        public int DateOfFoundation { get; set; }
    }

    class Company: CompanyInfo
    {
        public List<ActiveJob> ActiveJobs { get; set; }
        public int JobsCount { get => ActiveJobs.Count; }
        public int JobsHistory { get; set; }

        public Company()
        {
            ActiveJobs = new List<ActiveJob>(0);
        }
    }
}

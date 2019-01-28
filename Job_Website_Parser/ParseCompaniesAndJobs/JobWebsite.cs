using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCompaniesAndJobs
{
    class JobWebsite
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Company> Companies { get; set; }
        public int NumOfCompanies { get; set; }
        public List<string> Categories { get; set; }
    }
}

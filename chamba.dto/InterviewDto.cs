using System;
using System.Collections.Generic;
using System.Text;

namespace chambapp.dto
{
    public class InterviewDto
    {
        public int IdInterview { get; set; }
        public RecruiterDto Recruiter { get; set; } = new RecruiterDto();
        public CompanyDto Company { get; set; } = new CompanyDto();
        public int IdStatus { get; set; }
        public string EconomicExpectations { get; set; }
        public string EconomicExpectationsOffered { get; set; }
        public int InterviewDate { get; set; }
        public int ShipDate { get; set; }
        public int ReplyDate { get; set; }
        public string Provider { get; set; }
    }
}

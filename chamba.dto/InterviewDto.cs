using System;
using System.Collections.Generic;
using System.Text;

namespace chamba.dto
{
    public class InterviewDto
    {
        public int Idinterview { get; set; }
        public RecruiterDto Recruiter { get; set; }
        public CompanyDto Company { get; set; }
        public int Idstatus { get; set; }
        public string EconomicExpectations { get; set; }
        public string EconomicExpectationsOffered { get; set; }
        public int InterviewDate { get; set; }
        public int ShipDate { get; set; }
        public int ReplyDate { get; set; }
        public string Provider { get; set; }
    }
}

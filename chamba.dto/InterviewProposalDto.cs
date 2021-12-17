using System;
using System.Collections.Generic;
using System.Text;

namespace chambapp.dto
{
    public class InterviewProposalDto
    {
        public int IdCandidate { get; set; }
        public string Email { get; set; }
        public string RecruiterName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string EconomicExpectations { get; set; }
        public string EconomicExpectationsOffered { get; set; }
        public string Provider { get; set; }
    }
}

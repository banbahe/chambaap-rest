using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace chambapp.dto
{
    public class InterviewProposalDto
    {
        [Required]
        public int IdCandidate { get; set; }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value.ToLower(); }
        }
        public string RecruiterName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string EconomicExpectations { get; set; }
        public string EconomicExpectationsOffered { get; set; }
        public string Provider { get; set; }
    }
}

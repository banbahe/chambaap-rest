using System;
using System.Collections.Generic;
using System.Text;

namespace chambapp.dto
{
    public class CandidateDto
    {
        public int IdCandidate { get; set; }
        public string Name { get; set; }
        
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value.ToLower(); }
        }
        public string Phone { get; set; }
        public string Pwd { get; set; }
        public int CurrentState { get;  } = 200;
        public string  EmailSubject { get; set; }
        public string EmailConfiguration { get; set; }
        public string EmailReply { get; set; }
        public string EmailTemplate { get; set; }
        public string EmailKeyword { get; set; }
    }
}

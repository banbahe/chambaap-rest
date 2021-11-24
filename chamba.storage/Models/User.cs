using System;
using System.Collections.Generic;

#nullable disable

namespace chambapp.storage.Models
{
    public partial class User
    {
        public User()
        {
            InterviewIdcandidateNavigations = new HashSet<Interview>();
            InterviewIdrecruiterNavigations = new HashSet<Interview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Pwd { get; set; }
        public int IdstatusCatalog { get; set; }
        public string Subject { get; set; }
        public string ConfigurationEmail { get; set; }
        public string ReplyEmail { get; set; }
        public string TemplateEmail { get; set; }
        public string KeywordsEmail { get; set; }

        public virtual ICollection<Interview> InterviewIdcandidateNavigations { get; set; }
        public virtual ICollection<Interview> InterviewIdrecruiterNavigations { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace chamba.storage.Models
{
    public partial class Recruiter
    {
        public Recruiter()
        {
            Interviews = new HashSet<Interview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ReplyEmail { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
    }
}

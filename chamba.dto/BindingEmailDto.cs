using System;
using System.Collections.Generic;
using System.Text;

namespace chambapp.dto
{
    public class BindingEmailDto
    {
        public string binding_imghead { get; set; }
        public string binding_title { get; set; }
        public string binding_recruitername { get; set; }
        public string binding_candidatename { get; set; }
        public string binding_companyname { get; set; }
        public string binding_imgbody { get; set; }
        public string binding_caption { get; set; }
        public IEnumerable<string> binding_cvuri { get; set; } = new List<string>();
        public IEnumerable<string> binding_socialmedia { get; set; } = new List<string>();
        public string binding_phone { get; set; }
        public string binding_email { get; set; }
        public string binding_phrase { get; set; }
        public string binding_candidateemail { get; set; }

    }
}

using System;

namespace chambapp.dto
{
    public class RecruiterDto
    {
        public int IdRecruiter { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ReplyEmail { get; set; }
        public object TempSalary { get; set; }
    }
}

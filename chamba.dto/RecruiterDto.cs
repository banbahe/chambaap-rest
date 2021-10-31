using System;

namespace chamba.dto
{
    public class RecruiterDto
    {
        public int Idrecruiter { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ReplyEmail { get; set; }
        public object TempSalary { get; set; }
    }
}

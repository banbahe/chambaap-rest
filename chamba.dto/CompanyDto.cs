using System;
using System.Collections.Generic;
using System.Text;

namespace chamba.dto
{
    public class CompanyDto
    {
        public int Idcompany { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string MapRawJson { get; set; }
    }
}

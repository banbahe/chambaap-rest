using System;
using System.Collections.Generic;

#nullable disable

namespace chambapp.storage.Models
{
    public partial class Company
    {
        public Company()
        {
            Interviews = new HashSet<Interview>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string MapLat { get; set; }
        public string MapLong { get; set; }
        public string Name { get; set; }
        public string MapRawJson { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
    }
}

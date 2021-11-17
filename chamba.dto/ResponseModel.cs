using System;
using System.Collections.Generic;
using System.Text;

namespace chambapp.dto
{
    public class ResponseModel
    {
        public dynamic Datums { get; set; } = null;
        public int Flag { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        //public string Href { get; set; } = string.Empty;
    }
}

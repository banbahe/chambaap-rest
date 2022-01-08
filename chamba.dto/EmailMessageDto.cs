using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace chambapp.dto
{
    public class EmailMessageDto
    {
        public EmailMessageDto()
        {
            ToAddresses = new List<EmailAddressDto>();
            FromAddresses = new List<EmailAddressDto>();
        }

        public List<EmailAddressDto> ToAddresses { get; set; }
        public List<EmailAddressDto> FromAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    public class EmailAddressDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class EmailSettingsDto
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string pwd { get; set; }
        [Required]
        public string smtp { get; set; }
        [Required]
        public int port { get; set; }
        [Required]
        public string imap { get; set; }
        [Required]
        public int imap_port { get; set; }
        [Required]
        public bool useSsl { get; set; }

    }
}

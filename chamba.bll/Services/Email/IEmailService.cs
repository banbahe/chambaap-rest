using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using chambapp.dto;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System;
// using WebApi.Helpers;

namespace chambapp.bll.Services.Email
{
    public interface IEmailService
    {
        InterviewDto interview { get; set; }
        bool Send(string from, string to, string subject, string body, string configuration);
        bool SendPlainText(string from, string to, string subject, string body, string configutations, string path);
    }



}

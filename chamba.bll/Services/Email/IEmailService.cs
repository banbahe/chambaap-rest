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

    }
    public class EmailService : IEmailService
    {
        //          private readonly AppSettings _appSettings;

        //public EmailService(IOptions<AppSettings> appSettings)
        //{
        //    _appSettings = appSettings.Value;
        //}
        public InterviewDto interview { get; set; }
        private (string email, string pwd, string smtp, int port) _emailSettings(string configurations)
        {
            string email, pwd, smtp;
            int port;

            //using (StreamReader r = new StreamReader(configurations))
            //{

            //    string json = r.ReadToEnd();
            using (var jsonDoc = JsonDocument.Parse(configurations))
            {
                var root = jsonDoc.RootElement;

                email = root.GetProperty("MailProvider").GetProperty("email").GetString();
                pwd = root.GetProperty("MailProvider").GetProperty("pwd").GetString();
                smtp = root.GetProperty("MailProvider").GetProperty("smtp").GetString();
                port = root.GetProperty("MailProvider").GetProperty("port").GetInt32();
            }
            //}
            //using (StreamReader r = new StreamReader(configurations))
            //{

            //    string json = r.ReadToEnd();
            //    using (var jsonDoc = JsonDocument.Parse(json))
            //    {
            //        var root = jsonDoc.RootElement;

            //        email = root.GetProperty("MailProvider").GetProperty("email").GetString();
            //        pwd = root.GetProperty("MailProvider").GetProperty("pwd").GetString();
            //        smtp = root.GetProperty("MailProvider").GetProperty("smtp").GetString();
            //        port = root.GetProperty("MailProvider").GetProperty("port").GetInt32();
            //    }
            //}
            return (email: email, pwd: pwd, smtp: smtp, port: port);
        }

        // Profesional del Marketing Online interesada en Inside Sales

        public bool Send(string from, string to, string subject, string body, string configutations)
        {
            try
            {
                var t = Task.Run(() => _emailSettings(configutations));

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = body };


                t.Wait();
                var resultEmailSettings = t.Result;

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(resultEmailSettings.smtp, resultEmailSettings.port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(resultEmailSettings.email, resultEmailSettings.pwd);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }


}

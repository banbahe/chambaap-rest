// using MimeKit.MailboxAddress;
using chambapp.dto;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
// using WebApi.Helpers;

namespace chambapp.bll.Services.Email
{
    public class EmailService : IEmailService
    {
        //private InterviewDto _interviewDto { get; set; }
        public List<InterviewDto> ListInterviewDto { get; set; } = new List<InterviewDto>();
        public bool Send(string from, string to, string subject, string body, string configutations, ref string messageexception)
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
                messageexception = (ex.Message + " " + ex.InnerException.Message);
                return false;
            }
        }
        public bool SendPlainText(string from, string to, string subject, string body, string configutations, ref string messageexception)
        {
            try
            {
                var t = Task.Run(() => _emailSettings(configutations));

                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(from));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;

                // create our message text, just like before (except don't set it as the message.Body)
                var body_plain = new TextPart("plain")
                {
                    Text = body
                };

                //create an image attachment for the file located at path

                //var attachment = new MimePart("application/pdf")
                //{
                //    Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
                //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                //    ContentTransferEncoding = ContentEncoding.Base64,
                //    FileName = Path.GetFileName(path)
                //};

                // now create the multipart/mixed container to hold the message text and the
                // image attachment
                var multipart = new Multipart("mixed");
                multipart.Add(body_plain);
                //multipart.Add(attachment);

                // now set the multipart/mixed as the message body
                message.Body = multipart;
                // end 
                t.Wait();
                var resultEmailSettings = t.Result;

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(resultEmailSettings.smtp, resultEmailSettings.port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(resultEmailSettings.email, resultEmailSettings.pwd);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                messageexception = (ex.Message + " " + ex.InnerException.Message);
                return false;
            }
        }
        public async Task<string> ReadInbox(string configurations, string[] listTo)
        {
            string response = "1|";
            try
            {
                EmailSettingsDto emailSettings = _getEmailSettings(configurations);

                using (ImapClient client = new ImapClient())
                {
                    client.Connect(host: emailSettings.imap, port: emailSettings.imap_port, useSsl: emailSettings.useSsl);
                    client.Authenticate(new NetworkCredential(emailSettings.email, emailSettings.pwd));

                    // start 
                    var all = StatusItems.Count | StatusItems.HighestModSeq | StatusItems.Recent | StatusItems.UidNext | StatusItems.UidValidity | StatusItems.Unread;
                    var folders = (await client.GetFoldersAsync(client.PersonalNamespaces[0], all, true)).ToList();

                    //create searchquery filter
                    SearchQuery query = SearchQuery.FromContains(listTo[0]);
                    for (int i = 1; i < listTo.Length; i++)
                        query = query.Or(SearchQuery.FromContains(listTo[i]));

                    foreach (var itemCurrentFolder in folders)
                    {
                        if (itemCurrentFolder.FullName.ToUpper() == "INBOX" ||
                            itemCurrentFolder.FullName.ToUpper() == "[GMAIL]/SPAM" ||
                            itemCurrentFolder.FullName.ToUpper() == "SPAM" ||
                            itemCurrentFolder.FullName.ToUpper() == "ANOTHER")
                        {
                            itemCurrentFolder.Open(FolderAccess.ReadWrite);

                            if (!itemCurrentFolder.IsOpen == true)
                                throw new Exception($"{itemCurrentFolder.FullName} is not open");

                            var uids = itemCurrentFolder.Search(query);
                            var messages = itemCurrentFolder.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure);
                            foreach (var itemMessage in messages)
                            {
                                try
                                {
                                    var matchFolder = client.GetFolder("chambapp");

                                    itemCurrentFolder.Open(MailKit.FolderAccess.ReadWrite);

                                    var message = itemCurrentFolder.GetMessage(itemMessage.UniqueId);

                                    InterviewDto interviewDto = new InterviewDto();
                                    DateTime tmpReplyDate = DateTime.Parse(message.Date.ToString());
                                    interviewDto.ReplyDate = Helpers.DateTimeHelper.ConvertDatetimeToUnixTimeStamp(tmpReplyDate);
                                    interviewDto.Recruiter.Email = ((MimeKit.MailboxAddress)message.From[0]).Address;
                                    interviewDto.Recruiter.EmailReply = message.TextBody;
                                    
                                    // move email another folder 
                                    itemCurrentFolder.MoveTo(itemMessage.UniqueId, matchFolder);

                                    ListInterviewDto.Add(interviewDto);
                                }
                                catch (Exception ex)
                                {
                                    response = $"{response}, {ex.Message}, {itemMessage.EmailId}";
                                }
                            }

                        }
                    }
                    //end
                }
            }
            catch (Exception ex)
            {
                response = $"-1|Incidence {ex.Message}, {ex.InnerException.Message}, {response}";
            }
            return $"{response }, {ListInterviewDto.Count()} were registered";
        }
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
        private EmailSettingsDto _getEmailSettings(string configurations)
        {
            EmailSettingsDto settings = new EmailSettingsDto();
            using (var jsonDoc = JsonDocument.Parse(configurations))
            {
                var root = jsonDoc.RootElement;
                settings.email = root.GetProperty("MailProvider").GetProperty("email").GetString();
                settings.pwd = root.GetProperty("MailProvider").GetProperty("pwd").GetString();
                settings.smtp = root.GetProperty("MailProvider").GetProperty("smtp").GetString();
                settings.port = root.GetProperty("MailProvider").GetProperty("port").GetInt32();
                settings.imap = root.GetProperty("MailProvider").GetProperty("imap").GetString();
                settings.imap_port = root.GetProperty("MailProvider").GetProperty("imap_port").GetInt32();
                settings.useSsl = root.GetProperty("MailProvider").GetProperty("useSsl").GetBoolean();
            }
            return settings;
        }


    }
}

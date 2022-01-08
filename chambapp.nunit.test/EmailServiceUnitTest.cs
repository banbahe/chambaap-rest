using chambapp.bll.Services.Email;
using NUnit.Framework;
using System.Threading.Tasks;

namespace chambapp.nunit.test
{
    public class EmailServiceUnitTest
    {

        private IEmailService _emailService;

        [SetUp]
        public void Setup()
        {
            _emailService = new EmailService();
        }
        //[Test]
        //public void TestReadInbox()
        //{
        //    var t = Task.Run(() => _emailService.ReadInbox(""));
        //    //t.Start();
        //    t.Wait();
        //    var r = t.Result;
        //    // var t = _emailService.ReadInbox("");
        //    Assert.IsNull(null, "method ok"); 
        //}

        //[Test]
        //public async Task TestTextsearch()
        //{

        //    // var resultSendEmail = _emailService.Send("eban.blanquel@gmail.com", "edelruhe@gmail.com", "Developer", "<h1> evoca </h1>");
        //    _emailService.interview = new dto.InterviewDto();
        //    _emailService.interview.Recruiter.Name = "Franco Genel";
        //    _emailService.interview.Company.Name = "Sonido Liquido";
        //    _emailService.Send("eban.blanquel@gmail.com", "edelruhe@gmail.com", "Developer Full Stack");
        //    Assert.IsTrue(1 > 0, "Send email");
        //}
    }
}



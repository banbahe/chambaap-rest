using chambapp.bll.Helpers;
using chambapp.dto;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using chambapp.bll.Companies;
using chambapp.dal.Companies;
using chambapp.bll.AutoMapper;
using System;
using chambapp.bll.Services.Email;
using chambapp.bll.Services;
using chambapp.dal.Interviews;
using chambapp.bll.Users;
using chambapp.dal.Users;
using chambapp.bll.Interviews;

namespace chambapp.nunit.test
{
    public class InterviewUnitTest
    {
        private IUserBll _userBll;
        private IUserDal _userDal;
        private IEmailService _emailService;
        private IGoogleMaps _googleMaps;
        private IInterviewBll _interviewsBll;
        private ICompanyBll _companyBll;
        private ICompanyDal _companyDal;
        private InterviewDal _interviewDal;
        private MainMapper _mainMapper;
        private ResponseModel _response;

        [SetUp]
        public void Setup()
        {
            
            _emailService = new EmailService();
            _googleMaps = new GoogleMaps();
            _response = new ResponseModel();
            _mainMapper = new MainMapper();
            _interviewDal = new InterviewDal();
            _companyDal = new CompanyDal();
            _companyBll = new CompanyBll(_companyDal, _mainMapper);
            _userDal = new UserDal();
            _userBll = new UserBll(_userDal,_mainMapper, _response);

            _interviewsBll = new InterviewBll(
                                              _emailService,
                                              _companyBll,
                                              _googleMaps,
                                              _interviewDal,
                                              _response,
                                              _mainMapper);
        }

        [Test]
        public async Task ReadInboxTest() 
        {
            
            var result = await _interviewsBll.ReadMail(1);
            Assert.IsTrue(result.Flag == 1, "success");
            //t.Result;
            //Task<ResponseModel> ReadInbox(int idCandidate)
        }

        //[Test]
        //public void TestComposeEmail() 
        //{
        //    _interviewsBll.ComposeEmail(3);
        //    Assert.IsTrue(1 > 0, "function success");
        //}

        //[Test]
        //public void TestInitProcess() 
        //{
        //    var t = Task.Run(() => _interviewsBll.InitProcess());
        //    t.Wait();
        //    var result = t.Result;
        //    Assert.IsTrue(1 > 0, "function success");

        //}

        //[Test]
        //public void TestCreateInterview()
        //{
        //    CandidateDto candidateDto = new CandidateDto()
        //    {
        //        IdCandidate = 12
        //    };
        //    RecruiterDto recruiterDto = new RecruiterDto
        //    {
        //        IdRecruiter = 0,
        //        Name = "Pathfinder",
        //        Email = "pathfinder@gmail.com",
        //        Phone = "+1 5219191936",
        //        ReplyEmail = "",
        //        TempSalary = ""
        //    };

        //    CompanyDto companyDto = new CompanyDto
        //    {
        //        IdCompany = 0,
        //        Name = "companynull",
        //        Address = "addressnull",
        //        Lat = "0123",
        //        Lon = "1234",
        //        MapRawJson = "xxyyxx"
        //    };

        //    InterviewDto interviewDto = new InterviewDto
        //    {
        //        IdInterview = 0,
        //        Recruiter = recruiterDto,
        //        Candidate = candidateDto,
        //        Company = companyDto,
        //        CurrentState = (int)EnumStatusCatalog.SentFirstTime,
        //        EconomicExpectations = "5000",
        //        EconomicExpectationsOffered = "5001",
        //        InterviewDate = 0,
        //        ShipDate = DateTimeHelper.CurrentTimestamp(),
        //        ReplyDate = 0,
        //        Provider = "OCC"
        //    };

        //    var result = Task.Run(() => _interviewsBll.CreateAsync(interviewDto));
        //    result.Wait();

        //    //var result = await _interviewsBll.CreateAsync(interviewDto);
        //    interviewDto = result.Result.Datums;
        //    Assert.IsTrue(interviewDto.IdInterview > 0, "Success");
        //}

    }
}

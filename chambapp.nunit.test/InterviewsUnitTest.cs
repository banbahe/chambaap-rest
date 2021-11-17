using System.Collections.Generic;
using System.Threading.Tasks;
using chambapp.bll.AutoMapper;
using chambapp.bll.Helpers;
using chambapp.bll.Services;
using chambapp.bll.Interviews;
using chambapp.bll.Companies;
using chambapp.dal.Interviews;
using chambapp.dal.Companies;
using chambapp.dto;
using chambapp.storage.Models;
using NUnit.Framework;

namespace chambapp.nunit.test
{
    public class Tests
    {
        public class InterviewsUnitTest
        {
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
                _googleMaps = new GoogleMaps();
                _response = new ResponseModel();
                _mainMapper = new MainMapper();
                _interviewDal = new InterviewDal();
                _companyDal = new CompanyDal();
                _companyBll = new CompanyBll(_companyDal, _mainMapper);
                _interviewsBll = new InterviewBll(_companyBll,
                                                  _googleMaps,
                                                  _interviewDal,
                                                  _response,
                                                  _mainMapper);
            }

            //[Test]
            //public async Task TestInitProcess()
            //{
            //    string fileRoot = "./assets/chambas.txt";
            //    var linesInFile = System.IO.File.ReadLines(fileRoot);
            //    var result = await _interviewsBll.InitProcess(linesInFile);
            //    // var list = result.Datums.Count as List<InterviewDto>;
            //    // Assert.IsTrue(list.Count > 0, "Sucess");
            //    //Assert.Pass();
            //}
            //[Test]
            //public void TestGetAllInterviews()
            //{
            //    var result = _interviewsBll.GetAll();
            //    var list = result.Datums.Count as List<InterviewDto>;
            //    Assert.IsTrue(list.Count > 0, "Sucess");
            //    Assert.Pass();
            //}

            //[Test]
            //public async Task TestCreateInterview()
            //{
            //    RecruiterDto recruiterDto = new RecruiterDto
            //    {
            //        IdRecruiter = 0,
            //        Name = "Dummy",
            //        Email = "dummy@gmail.com",
            //        Phone = "5532048",
            //        ReplyEmail = "ReplyEmail",
            //        TempSalary = "TempSalary"
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
            //        Company = companyDto,
            //        IdStatus = (int)EnumStatusCatalog.SentFirstTime,
            //        EconomicExpectations = "5000",
            //        EconomicExpectationsOffered = "5001",
            //        InterviewDate = 123,
            //        ShipDate = 123,
            //        ReplyDate = 123,
            //        Provider = "OCC"
            //    };

            //    var result = await _interviewsBll.Create(interviewDto);
            //    interviewDto = result.Datums;
            //    Assert.IsTrue(interviewDto.IdInterview > 0, "Success");
            //}
        }
    }
}
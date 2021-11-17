using chambapp.bll.Helpers;
using chambapp.dto;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using chambapp.bll.Companies;
using chambapp.dal.Companies;
using chambapp.bll.AutoMapper;
using System;

namespace chambapp.nunit.test
{
    internal class CompanyUnitTest
    {
        private ICompanyBll _CompanyBll;
        private CompanyDal _companyDal;
        private MainMapper _mainMapper;
        private ResponseModel _response;
        private CompanyDto _companyDto;

        [SetUp]
        public void Setup()
        {
            //_response = new ResponseModel();
            _companyDto = new CompanyDto();
            _mainMapper = new MainMapper();
            _companyDal = new CompanyDal();
            _CompanyBll = new CompanyBll(_companyDal,_mainMapper);
        }

        [Test]
        public async Task TestSetAsync()
        {
            Guid g = Guid.NewGuid();

            _companyDto.IdCompany = 4;
            _companyDto.Address = "C17 178";
            _companyDto.Lat = "100";
            _companyDto.Lon = "-100";
            _companyDto.Name = "Company Dummy";
            _companyDto.MapRawJson = g.ToString();
            var resultSet = await _CompanyBll.SetAsync(_companyDto);
            Assert.IsTrue(!string.IsNullOrEmpty(resultSet.MapRawJson.ToString()), "OK TestSetAsync()");
        }
    }
}

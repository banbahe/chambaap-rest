using chambapp.bll.AutoMapper;
using chambapp.bll.Helpers;
using chambapp.dal.Companies;
using chambapp.dto;
using chambapp.storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace chambapp.bll.Companies
{
    public class CompanyBll : ICompanyBll
    {
        private readonly ICompanyDal _companyDal;
        private readonly MainMapper _mainMapper;

        public CompanyBll(ICompanyDal companyDal, MainMapper mainMapper)
        {
            _companyDal = companyDal;
            _mainMapper = mainMapper;
        }
        public async Task<CompanyDto> SetAsync(CompanyDto companyDto)
        {

            try
            {
                var mapResult = _mainMapper.Mapper.Map<Company>(companyDto);
                var setResult = await _companyDal.SetAsync(mapResult);
                return companyDto;
            }
            catch (Exception ex)
            {
                // TODO 
                // nlog register pendient
                return null;
            }
        }
    }
}

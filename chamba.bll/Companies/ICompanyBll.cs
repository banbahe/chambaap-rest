using chambapp.dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using chambapp.storage.Models;

namespace chambapp.bll.Companies
{
    public interface ICompanyBll
    {
        Task<CompanyDto>SetAsync(CompanyDto company);

    }
}

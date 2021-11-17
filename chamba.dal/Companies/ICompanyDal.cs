using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using chambapp.storage.Models;

namespace chambapp.dal.Companies
{
    public  interface ICompanyDal
    {
        Task<Company> SetAsync(Company company);

    }
}

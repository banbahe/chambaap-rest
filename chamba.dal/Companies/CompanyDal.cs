using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlTypes;
using chambapp.storage.Models;
using Microsoft.EntityFrameworkCore;

namespace chambapp.dal.Companies
{
    public class CompanyDal : ICompanyDal
    {
        private IEnumerable<Company> _listCompany { get; set; }
        private Company _Company { get; set; }

        public async Task<Company> SetAsync(Company company)
        {

            using (chamba_storageContext context = new chamba_storageContext())
            {
                context.Entry(company).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return _Company = company;
         
        }
    }
}

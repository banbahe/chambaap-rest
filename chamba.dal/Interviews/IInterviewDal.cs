using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chambapp.storage.Models;

namespace chambapp.dal.Interviews
{
    public interface IInterviewDal
    {
        IQueryable<Interview> GetAllFilter(int idstatus = 0);
        IEnumerable<Interview> GetAll();
        Task<Interview> CreateAsync(Interview interview);
    }
}

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
        IEnumerable<Interview> GetAllFilter(int all = 0,int id = 0, int idstatus = 0, int idcandidate = 0, int idcompany = 0);
        Task<Interview> CreateAsync(Interview interview);
        Task<Interview> SetAsync(Interview interview);
        // Interview Set(Interview interview);
    }
}

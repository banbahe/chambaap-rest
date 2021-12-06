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
<<<<<<< HEAD
        IEnumerable<Interview> GetAllFilter(int all = 0,int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0);
=======
        IQueryable<Interview> GetAllFilter(int idstatus = 0);
        IEnumerable<Interview> GetAll();
>>>>>>> bb87007c9d35a0e01c209e330c7cb5dbb2983320
        Task<Interview> CreateAsync(Interview interview);
        Task<Interview> SetAsync(Interview interview);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using chambapp.storage.Models;

namespace chambapp.dal.Interviews
{
    public interface IInterviewDal
    {
        IEnumerable<Interview> GetAll();
        Task<Interview> CreateAsync(Interview interview);
    }
}

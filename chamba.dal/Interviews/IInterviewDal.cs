using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using chamba.storage.Models;

namespace chamba.dal.Interviews
{
    public interface IInterviewDal
    {
        List<Interview> GetAll();
        Task<Interview> CreateAsync(Interview interview);
    }
}

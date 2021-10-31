
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlTypes;
using chamba.storage.Models;
using Microsoft.EntityFrameworkCore;

//using Microsoft.EntityFrameworkCore;

namespace chamba.dal.Interviews
{
    public class InterviewDal : IInterviewDal
    {
        private List<Interview> _listInterview { get; set; }
        private Interview _interview { get; set; }
        public InterviewDal()
        {
            _listInterview = new List<Interview>();
            _interview = new Interview();
        }

        public List<Interview> GetAll()
        {
            using (chamba_storageContext context = new chamba_storageContext())
            {
                // Eager Loading in Entity Framework
                _listInterview = context.Interviews.Include(t => t.IdcompanyNavigation)
                    .Include(t => t.IdrecruiterNavigation).ToList();

                //var list2 = context.Interviews.ToList();
                //var list3 = context.Interviews.Include(fk => fk.IdcompanyNavigation).ToList();
                //var list4 = context.Interviews.Include("companies").ToList();
            }
            return _listInterview;
        }

        public Task<Interview> CreateAsync(Interview interview)
        {
            throw new NotImplementedException();
        }
    }
}

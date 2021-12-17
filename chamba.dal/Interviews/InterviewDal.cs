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

//using Microsoft.EntityFrameworkCore;

namespace chambapp.dal.Interviews
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
        public async Task<Interview> SetAsync(Interview interview)
        {
            using (chamba_storageContext context = new chamba_storageContext())
            {
                context.Attach(interview);
                context.Entry(interview).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return interview;
        }


        public Interview Set(Interview item) 
        {
            Interview result;
            using (chamba_storageContext context = new chamba_storageContext())
            {
                context.Attach(item);
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                result = item;
                //context.Entry(interview).State = EntityState.Modified;
                //await context.SaveChangesAsync();
            }
            return result;
            
        }

        public async Task<Interview> CreateAsync(Interview interview)
        {
            using (chamba_storageContext context = new chamba_storageContext())
            {
                // context.Interviews.Add(interview);
                context.Entry(interview.IdcompanyNavigation).State = EntityState.Added;
                context.Entry(interview.IdrecruiterNavigation).State = EntityState.Added;
                // context.Entry(interview.IdcandidateNavigation).State = EntityState.Added;
                context.Entry(interview).State = EntityState.Added;
                await context.SaveChangesAsync();
                return interview;
            }
        }

        public IEnumerable<Interview> GetAllFilter(int all = 0, int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0)
        {

            //

            using (chamba_storageContext context = new chamba_storageContext())
            {
                if (all > 0)
                {
                    IQueryable<Interview> data2 = context.Interviews.Include(t => t.IdcompanyNavigation)
                                                            .Include(t => t.IdrecruiterNavigation)
                                                            .Include(t => t.IdcandidateNavigation);
                    _listInterview = data2.ToList();
                }
                if (id > 0)
                {

                    IQueryable<Interview> data2 = context.Interviews.Include(t => t.IdcompanyNavigation)
                                                            .Include(t => t.IdrecruiterNavigation)
                                                            .Include(t => t.IdcandidateNavigation)
                                                            .Where(t => t.Id == id);
                    _listInterview = data2.ToList();

                }
                if (idstatus > 0 && iduser > 0)
                {
                    
                    IQueryable<Interview> data = context.Interviews.Include(t => t.IdcompanyNavigation)
                                                            .Include(t => t.IdrecruiterNavigation)
                                                            .Include(t => t.IdcandidateNavigation)
                                                            .Where(t => t.IdstatusCatalog == idstatus &&
                                                                        t.Idcandidate == iduser);
                    //var data = context.Interviews.Include(t => t.IdcompanyNavigation)
                    //                                        .Include(t => t.IdrecruiterNavigation)
                    //                                        .Include(t => t.IdcandidateNavigation)
                    //                                        .Where(t => t.IdstatusCatalog == idstatus &&
                    //                                                    t.Idcandidate == iduser);
                    
                    return data.ToList();
                }

                if (idstatus > 0)
                {
                    IQueryable<Interview> data = context.Interviews.Include(t => t.IdcompanyNavigation)
                                                            .Include(t => t.IdrecruiterNavigation)
                                                            .Include(t => t.IdcandidateNavigation)
                                                            .Where(t => t.IdstatusCatalog == idstatus);
                    return data.ToList();
                }

                if (iduser > 0)
                {
                    IQueryable<Interview> data = context.Interviews.Include(t => t.IdcompanyNavigation)
                                                            .Include(t => t.IdrecruiterNavigation)
                                                            .Include(t => t.IdcandidateNavigation)
                                                            .Where(t => t.Idcandidate == iduser || t.Idrecruiter == iduser);
                    return data.ToList();
                }
                if (idcompany > 0)
                {
                    IQueryable<Interview> data = context.Interviews.Include(t => t.IdcompanyNavigation)
                                                            .Include(t => t.IdrecruiterNavigation)
                                                            .Include(t => t.IdcandidateNavigation)
                                                            .Where(t => t.Idcompany == idcompany);
                    _listInterview = data.ToList();
                }
            }
            return _listInterview;
        }
    }
}

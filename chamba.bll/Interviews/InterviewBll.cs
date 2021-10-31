using chamba.dto;
using chamba.dal.Interviews;
using chamba.bll.AutoMapper;
using System;
using System.Collections.Generic;
using chamba.storage.Models;

namespace chamba.bll.Interviews
{
    public class InterviewBll : IInterviewBll
    {
        private  IInterviewDal _interviewDal;
     

        public InterviewBll(IInterviewDal interviewDal) => _interviewDal = interviewDal;

        

        public List<InterviewDto> GetAll()
        {
            try
            {
                var dbInterviews = _interviewDal.GetAll();
                List<InterviewDto> list = MapperHelper.MapList<Interview, InterviewDto>(MapperHelper.StaticMapper, dbInterviews);
                return list;
            }
            catch (Exception ex)
            {
                // TODO
                // Add NLOG setup
                return null;
            }
        }

        public InterviewDto Create()
        {
            throw new NotImplementedException();
        }
    }
}

using chamba.dto;
using System.Collections.Generic;

namespace chamba.bll.Interviews
{
    public interface IInterviewBll
    {
        List<InterviewDto> GetAll();
        InterviewDto Create();
    }
}
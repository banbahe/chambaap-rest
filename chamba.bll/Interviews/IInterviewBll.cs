using chambapp.dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using chambapp.dto;
using chambapp.storage.Models;

namespace chambapp.bll.Interviews
{
    public interface IInterviewBll
    {
        Task<ResponseModel> InitProcess(IEnumerable<string> storageChambas);
        ResponseModel GetAll();
        //List<InterviewDto> GetAll();
        Task<ResponseModel>Create(InterviewDto interview);
    }
}
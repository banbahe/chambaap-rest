using chambapp.dto;
using chambapp.storage.Models;
using System.Threading.Tasks;

namespace chambapp.bll.Interviews
{
    public interface IInterviewBll
    {
        Task<ResponseModel> DeleteAsync(int userId);
        Task<ResponseModel> ReadMail(int idCandidate);
        Task<ResponseModel> InitProcess(int idCandidate = -1);
        Task<ResponseModel> CreateProposalAsync(InterviewProposalDto interview);
        Task<ResponseModel> CreateAsync(InterviewDto interview);
        ResponseModel GetPerFilter(int all = 0, int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0);
    }
}
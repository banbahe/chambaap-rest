using chambapp.dto;
using chambapp.storage.Models;
using System.Threading.Tasks;

namespace chambapp.bll.Interviews
{
    public interface IInterviewBll
    {
        Task<ResponseModel> InitProcess();

        Task<ResponseModel> CreateAsync(InterviewDto interview);
        string ComposeEmail(int idinterview, Interview paramInterview = null);

        ResponseModel GetPerFilter(int all = 0,int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0);
    }
}
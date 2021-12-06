using chambapp.dto;
using chambapp.storage.Models;
using System.Threading.Tasks;

namespace chambapp.bll.Interviews
{
    public interface IInterviewBll
    {
<<<<<<< HEAD
        Task<ResponseModel> InitProcess();

        Task<ResponseModel> CreateAsync(InterviewDto interview);
        string ComposeEmail(int idinterview, Interview paramInterview = null);

        ResponseModel GetPerFilter(int all = 0,int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0);

=======
        string AddRowFormat(InterviewRowDto item);

        Task<ResponseModel> AddInterviewTemp(InterviewRowDto item);

        Task<ResponseModel> InitProcess();
        ResponseModel GetAll();
        //List<InterviewDto> GetAll();
        Task<ResponseModel> Create(InterviewDto interview);
>>>>>>> bb87007c9d35a0e01c209e330c7cb5dbb2983320
    }
}
using chambapp.dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using chambapp.bll.Helpers;
using chambapp.dal.Users;
using chambapp.bll.AutoMapper;
using chambapp.storage.Models;

namespace chambapp.bll.Users
{
    public interface IUserBll
    {
        Task<ResponseModel> SetAsync(RecruiterDto recruiterDto = null, CandidateDto candidateDto = null);
        Task<ResponseModel> CreateRecruiterAsync(RecruiterDto recruiterDto);
        Task<ResponseModel> CreateCandidateAsync(CandidateDto candidateDto);
        T GetUser<T>(int idcandidate);
    }
    
}

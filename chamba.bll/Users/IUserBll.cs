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
        Task<ResponseModel> CreateRecruiterAsync(RecruiterDto recruiterDto);
        Task<ResponseModel> CreateCandidateAsync(CandidateDto candidateDto);
        T GetUser<T>(int idcandidate);

    }
    public class UserBll : IUserBll
    {
        private MainMapper _mainMapper;
        private ResponseModel _responseModel;
        
        private IUserDal _userDal;
        public UserBll(IUserDal userDal,  MainMapper mainMapper,ResponseModel responseModel) 
        {
            _responseModel = responseModel;
            _mainMapper = mainMapper;
            _userDal = userDal;
            
        }
        
        public async Task<ResponseModel> CreateRecruiterAsync(RecruiterDto recruiterDto)
        {

            try
            {
                // MAPPER 
                var mapperUser = _mainMapper.Mapper.Map<User>(recruiterDto);

                // DAL
                var result = await _userDal.CreateUserAsync(mapperUser);
                _responseModel.Datums = result ;
            }
            catch (Exception ex)
            {
                _responseModel.Flag = (int)EnumStatusCatalog.Error;
                _responseModel.Message = $"*Incidence {ex.Message}, {ex.InnerException.Message}";
            }
            return _responseModel;
        }
        public async Task<ResponseModel> CreateCandidateAsync(CandidateDto candidateDto)
        {
            try
            {
                // MAPPER 
                var mapperUser = _mainMapper.Mapper.Map<User>(candidateDto);

                // DAL
                var result = await _userDal.CreateUserAsync(mapperUser);
                _responseModel.Datums = result;
            }
            catch (Exception ex)
            {
                _responseModel.Flag = (int)EnumStatusCatalog.Error;
                _responseModel.Message = $"*Incidence {ex.Message}, {ex.InnerException.Message}";
            }
            return _responseModel;
        }

        public T GetUser<T>(int iduser)
        {
            try
            {
                var result = _userDal.Get(iduser);
                var mapperUser = _mainMapper.Mapper.Map<T>(result);
                return mapperUser;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

    }
}

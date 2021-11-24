using chambapp.bll.AutoMapper;
using chambapp.bll.Helpers;
using chambapp.dal.Interviews;
using chambapp.dto;
using chambapp.storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using chambapp.bll.Services;
using chambapp.dal.Companies;
using System.Text.Json;
using System.Text.Json.Serialization;
using chambapp.bll.Companies;

namespace chambapp.bll.Interviews
{
    public class InterviewBll : IInterviewBll
    {

        private IGoogleMaps _googleMaps;
        private IInterviewDal _interviewDal;
        private ResponseModel _responseModel;
        private MainMapper _mainMapper;
        private ICompanyBll _companyBll;

        //public InterviewBll(IInterviewDal interviewDal) => _interviewDal = interviewDal;
        public InterviewBll(ICompanyBll companyBll,
                            IGoogleMaps googleMaps,
                            IInterviewDal interviewDal,
                            ResponseModel responseModel,
                            MainMapper mainMapper

            )
        {
            _companyBll = companyBll;
            _interviewDal = interviewDal;
            _responseModel = responseModel;
            _mainMapper = mainMapper;
            _googleMaps = googleMaps;
        }

        public string AddRowFormat(InterviewRowDto item)
        {
            return $"{item.Email}|{item.RecruiterName}|{item.Company}|{item.Phone}|{item.EconomyExpectation}|{item.EconomyExpectationOffered}|{item.Provider}|;";
        }
        public async Task<ResponseModel> InitProcess(IEnumerable<string> storageChambas)
        {
            int counter = 0;
            foreach (var interview in _getInterviewsFromTxt(storageChambas))
            {
                try
                {
                    var addInterview = await _interviewDal.CreateAsync(interview);
                    if (addInterview.Id > 0)
                    {
                        counter++;
                        // call service Google Maps Plataform
                        string companyName = addInterview.IdcompanyNavigation.Name;
                        RootGoogleMapsDto resultGmp = await _googleMaps.TextSearchAsync(companyName);
                        var resultCompany = resultGmp.Results.FirstOrDefault();

                        // Company 
                        string jsonString = JsonSerializer.Serialize(resultGmp);

                        addInterview.IdcompanyNavigation.Address = resultCompany.formatted_address;
                        addInterview.IdcompanyNavigation.MapLat = resultCompany.geometry.location.lat.ToString();
                        addInterview.IdcompanyNavigation.MapLong = resultCompany.geometry.location.lng.ToString();
                        addInterview.IdcompanyNavigation.MapRawJson = jsonString;

                        var mapCompany = _mainMapper.Mapper.Map<CompanyDto>(addInterview.IdcompanyNavigation);

                        await _companyBll.SetAsync(mapCompany);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} ; {e.InnerException}");
                }
            }
            _responseModel.Message = $"{storageChambas.Count()}  lines in file; {counter} new records were added";
            return _responseModel;
        }
        public async Task<ResponseModel> InitProcess()
        {
            var getInterviewsProposal = _interviewDal.GetAllFilter((int)EnumStatusCatalog.InterviewProposal);
            int counter = 0;
            foreach (var interview in getInterviewsProposal)
            {
                try
                {
                    var addInterview = await _interviewDal.CreateAsync(interview);
                    if (addInterview.Id > 0)
                    {
                        counter++;
                        // call service Google Maps Plataform
                        string companyName = addInterview.IdcompanyNavigation.Name;
                        RootGoogleMapsDto resultGmp = await _googleMaps.TextSearchAsync(companyName);
                        var resultCompany = resultGmp.Results.FirstOrDefault();

                        // Company 
                        string jsonString = JsonSerializer.Serialize(resultGmp);

                        addInterview.IdcompanyNavigation.Address = resultCompany.formatted_address;
                        addInterview.IdcompanyNavigation.MapLat = resultCompany.geometry.location.lat.ToString();
                        addInterview.IdcompanyNavigation.MapLong = resultCompany.geometry.location.lng.ToString();
                        addInterview.IdcompanyNavigation.MapRawJson = jsonString;

                        var mapCompany = _mainMapper.Mapper.Map<CompanyDto>(addInterview.IdcompanyNavigation);

                        await _companyBll.SetAsync(mapCompany);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} ; {e.InnerException}");
                }
            }
            _responseModel.Message = $"{storageChambas.Count()}  lines in file; {counter} new records were added";
            return _responseModel;
        }

        private IEnumerable<Interview> _getInterviewsFromTxt(IEnumerable<string> storageChambas)
        {
            foreach (string item in storageChambas)
            {
                Interview interview = new Interview();
                Recruiter recruiter = new Recruiter();
                Company company = new Company();

                string[] data = item.Split("|", StringSplitOptions.RemoveEmptyEntries);
                // data = item.Split('|');

                if (data.Length > 0)
                {
                    recruiter.Name = data[1];
                    recruiter.Email = data[0];
                    recruiter.PhoneNumber = data[3];

                    company.Name = data[2];

                    interview.EconomicExpectations = data[4];
                    interview.EconomicExpectationsOffered = data[5];
                    interview.ShipDate = DateTimeHelper.CurrentTimestamp();
                    interview.Provider = data[6];

                    interview.IdcompanyNavigation = company;
                    interview.IdrecruiterNavigation = recruiter;

                    yield return interview;
                }

            }
        }

        private IQueryable<Interview> _getInterviewsProposal()
        {
               
        }
        public ResponseModel GetAll()
        {
            try
            {
                var t = Task.Run(() => _interviewDal.GetAll());
                t.Wait();

                var listInterviews = t.Result;

                if (listInterviews.Count() > 0)
                {
                    var listDtos = _mainMapper.Mapper.Map<IEnumerable<Interview>, IEnumerable<InterviewDto>>(listInterviews);
                    _responseModel.Flag = (int)EnumStatusCatalog.Ok;
                    _responseModel.Datums = listDtos;
                }
            }
            catch (Exception ex)
            {
                _responseModel.Flag = (int)EnumStatusCatalog.Error;
                _responseModel.Message = $"Incidence: ${ex.Message} ; ${ex.InnerException}";
            }

            return _responseModel;
        }

        public async Task<ResponseModel> Create(InterviewDto interview)
        {
            try
            {
                var tmpInterview = _mainMapper.Mapper.Map<Interview>(interview);
                var result = await _interviewDal.CreateAsync(tmpInterview);
                if (result.Id > 0)
                {
                    //InterviewDto interviewDto = MapperHelper.Map<InterviewDto,Interview>(result);
                    InterviewDto interviewDto = _mainMapper.Mapper.Map<InterviewDto>(result);
                    _responseModel.Datums = interviewDto;
                    _responseModel.Flag
                        = (int)EnumStatusCatalog.Ok;
                }
            }
            catch (Exception ex)
            {
                _responseModel.Flag = (int)EnumStatusCatalog.Error;
                _responseModel.Message = $"* Incidence ${ex.Message}; ${ex.InnerException}";
            }

            return _responseModel;
        }
    }
}

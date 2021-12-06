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
using chambapp.bll.Services.Email;
using chambapp.bll.Users;
using System.IO;

namespace chambapp.bll.Interviews
{
    public class InterviewBll : IInterviewBll
    {
        private IGoogleMaps _googleMaps;
        private IInterviewDal _interviewDal;
        private ResponseModel _responseModel;
        private MainMapper _mainMapper;
        private ICompanyBll _companyBll;
        private IEmailService _emailService;


        public InterviewBll(IEmailService emailService,
                            ICompanyBll companyBll,
                            IGoogleMaps googleMaps,
                            IInterviewDal interviewDal,
                            ResponseModel responseModel,
                            MainMapper mainMapper

            )
        {

            _emailService = emailService;
            _companyBll = companyBll;
            _interviewDal = interviewDal;
            _responseModel = responseModel;
            _mainMapper = mainMapper;
            _googleMaps = googleMaps;
        }
        public ResponseModel GetPerFilter(int all = 0, int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0)
        {
            try
            {
                var resultInterviews = _interviewDal.GetAllFilter(all: all, id: id, idstatus: idstatus, iduser: iduser, idcompany: idcompany);
                if (resultInterviews.Count() > 0)
                {
                    //var mapInterview = _mainMapper.Mapper.Map<InterviewDto>(resultInterviews);
                    var mapInterview = _mainMapper.Mapper.Map<IEnumerable<Interview>, IEnumerable<InterviewDto>>(resultInterviews);
                    _responseModel.Flag = (int)EnumStatusCatalog.Ok;
                    _responseModel.Datums = mapInterview;
                }
                else
                {
                    _responseModel.Flag = (int)EnumStatusCatalog.Ok;
                    _responseModel.Message = "* No Data";
                }
            }
            catch (Exception ex)
            {
                _responseModel.Flag = (int)EnumStatusCatalog.Error;
                _responseModel.Message = $"* Incidence ${ex.Message}; ${ex.InnerException}";
            }
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
                catch (Exception ex)
                {
                    _responseModel.Flag = (int)EnumStatusCatalog.Error;
                    _responseModel.Message = $"* Incidence ${ex.Message}; ${ex.InnerException}";
                }
            }
            return _responseModel;
        }

        public string ComposeEmail(int idinterview, Interview paramInterview = null)
        {
            string htmltemplate = string.Empty;
            Interview interviewData;
            User candidateData;
            User recruiterData;

            try
            {
                if (paramInterview != null)
                    interviewData = paramInterview;

                else
                    interviewData = _interviewDal.GetAllFilter(id: idinterview).FirstOrDefault<Interview>();


                candidateData = interviewData.IdcandidateNavigation;
                recruiterData = interviewData.IdrecruiterNavigation;


                htmltemplate = candidateData.TemplateEmail;
                string keywordsjson = candidateData.KeywordsEmail;
                BindingEmailDto keywords = JsonSerializer.Deserialize<BindingEmailDto>(keywordsjson);

                // build template email
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_imghead), keywords.binding_imghead);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_title), keywords.binding_title);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_recruitername), keywords.binding_recruitername);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_candidatename), keywords.binding_candidatename);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_companyname), keywords.binding_companyname);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_imgbody), keywords.binding_imgbody);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_caption), keywords.binding_caption);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_phone), keywords.binding_phone);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_email), keywords.binding_email);
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_phrase), keywords.binding_phrase);

                // cv attachment
                string tempAttach = string.Empty;

                foreach (string itemAttach in keywords.binding_cvuri)
                {
                    string[] btndata = itemAttach.Split('|');
                    tempAttach += @$"<a href = ""{btndata[1]}"" target = ""_blank"" class=""btn"" > {btndata[0]}</a>";
                }
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_cvuri), tempAttach);

                // social media
                string tempSocialMedia = string.Empty;
                foreach (string itemAttach in keywords.binding_socialmedia)
                {
                    string[] temparray = itemAttach.Split('|');
                    tempSocialMedia += @$"<a href=""{temparray[1]}"" class=""soc-btn gh"">{temparray[0]}</a>";
                }
                htmltemplate = htmltemplate.Replace(nameof(keywords.binding_socialmedia), tempSocialMedia);
            }
            catch (Exception ex)
            {
                htmltemplate = $"{ex.Message } ; {ex.InnerException.Message}";
            }
            return htmltemplate;
        }

        public async Task<ResponseModel> CreateAsync(InterviewDto interview)
        {
            try
            {
                if (interview.Candidate.IdCandidate <= 0)
                {
                    _responseModel.Flag = (int)EnumStatusCatalog.Error;
                    _responseModel.Message = "* Incidence Id Candidate is required";
                }
                else
                {
                    interview.CurrentState = (int)EnumStatusCatalog.InterviewProposal;
                    interview.InterviewDate = Helpers.DateTimeHelper.CurrentTimestamp();
                    var tmpInterview = _mainMapper.Mapper.Map<Interview>(interview);
                    var result = await _interviewDal.CreateAsync(tmpInterview);
                    if (result.Id > 0)
                    {
                        //InterviewDto interviewDto = MapperHelper.Map<InterviewDto,Interview>(result);
                        InterviewDto interviewDto = _mainMapper.Mapper.Map<InterviewDto>(result);
                        _responseModel.Flag = (int)EnumStatusCatalog.Ok;
                        _responseModel.Datums = interviewDto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responseModel.Flag = (int)EnumStatusCatalog.Error;
                _responseModel.Message = $"* Incidence ${ex.Message}; ${ex.InnerException}";
            }

            return _responseModel;
        }
      
        private async Task<bool> _getJobOffers()
        {
            try
            {
                foreach (var interview in _interviewDal.GetAllFilter(idstatus: (int)EnumStatusCatalog.InterviewProposal))
                {


                    string htmlTemplate = ComposeEmail(interview.Id, interview);
                    bool flagEmail = _emailService.Send(from: interview.IdcandidateNavigation.Email,
                                       to: interview.IdrecruiterNavigation.Email,
                                       subject: interview.IdcandidateNavigation.Subject,
                                       body: htmlTemplate,
                                       configuration: interview.IdcandidateNavigation.ConfigurationEmail);

                    if (flagEmail)
                    {
                        interview.IdstatusCatalog = (int)EnumStatusCatalog.Email_SentFirstTime;
                        interview.ShipDate = Helpers.DateTimeHelper.CurrentTimestamp();
                        await _interviewDal.SetAsync(interview);
                    }

                    //    // call service Google Maps Plataform
                    //    string companyName = addInterview.IdcompanyNavigation.Name;
                    //    RootGoogleMapsDto resultGmp = await _googleMaps.TextSearchAsync(companyName);
                    //    var resultCompany = resultGmp.Results.FirstOrDefault();

                    //    // Company 
                    //    string jsonString = JsonSerializer.Serialize(resultGmp);

                    //    addInterview.IdcompanyNavigation.Address = resultCompany.formatted_address;
                    //    addInterview.IdcompanyNavigation.MapLat = resultCompany.geometry.location.lat.ToString();
                    //    addInterview.IdcompanyNavigation.MapLong = resultCompany.geometry.location.lng.ToString();
                    //    addInterview.IdcompanyNavigation.MapRawJson = jsonString;

                    //    var mapCompany = _mainMapper.Mapper.Map<CompanyDto>(addInterview.IdcompanyNavigation);



                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} ; {e.InnerException}");
                return false;
            }
        }
        private async Task<bool> _readInbox()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

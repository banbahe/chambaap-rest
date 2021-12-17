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
using System.Threading;

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

        public async Task<ResponseModel> InitProcess(int idCandidate = -1)
        {
            var getInterviewsProposal = _interviewDal.GetAllFilter(iduser:idCandidate, idstatus: (int)EnumStatusCatalog.InterviewProposal);
            string path = $"{Directory.GetCurrentDirectory()}\\assets\\cv.esteban.blanquel.pdf";
            int count = 0;
            foreach (var item in getInterviewsProposal)
            {
                try
                {
                    // get template for email
                    var templates = _composeEmail(item.Id, item);
                    string getHtmlEmail = templates.mailHtmlTemplate;
                    string getPlainTextEmail = templates.mailPlainTextTemplate;

                    bool flag0 = _emailService.Send(item.IdcandidateNavigation.Email,
                                       item.IdrecruiterNavigation.Email,
                                       item.IdcandidateNavigation.Subject,
                                       getHtmlEmail,
                                       item.IdcandidateNavigation.ConfigurationEmail);

                    Thread.Sleep(3333);

                    bool flag1 = _emailService.SendPlainText(item.IdcandidateNavigation.Email,
                                       item.IdrecruiterNavigation.Email,
                                       item.IdcandidateNavigation.Subject + ".",
                                       getPlainTextEmail,
                                       item.IdcandidateNavigation.ConfigurationEmail, path);

                    if (flag0 && flag1)
                    {
                        //set values
                        item.IdstatusCatalog = (int)EnumStatusCatalog.Email_SentFirstTime;
                        item.ShipDate = Helpers.DateTimeHelper.CurrentTimestamp();
                        await _interviewDal.SetAsync(item);
                        count++;
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

                    //    await _companyBll.SetAsync(mapCompany);

                }
                catch (Exception ex)
                {
                    _responseModel.Message = $"* Incidence {item.IdrecruiterNavigation.Email} {ex.Message}; {ex.InnerException}";
                }
            }
            _responseModel.Flag = (int)EnumStatusCatalog.Ok;
            _responseModel.Message = $"{_responseModel.Message} ; Send {count} emails ";
            return _responseModel;
        }


        private (string mailHtmlTemplate, string mailPlainTextTemplate) _composeEmail(int idinterview, Interview paramInterview = null)
        {
            string mailHtmlTemplate = string.Empty;
            string mailPlainTextTemplate = string.Empty;
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

                mailHtmlTemplate = candidateData.TemplateEmail;
                mailPlainTextTemplate = candidateData.TemplatePlainText;

                string keywordsjson = candidateData.KeywordsEmail;
                BindingEmailDto keywords = JsonSerializer.Deserialize<BindingEmailDto>(keywordsjson);

                // build email html template
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_imghead), keywords.binding_imghead);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_title), keywords.binding_title);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_recruitername), paramInterview.IdrecruiterNavigation.Name);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_candidatename), paramInterview.IdcandidateNavigation.Name);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_candidateemail), paramInterview.IdcandidateNavigation.Email);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_companyname), paramInterview.IdcompanyNavigation.Name);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_imgbody), keywords.binding_imgbody);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_caption), keywords.binding_caption);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_phone), paramInterview.IdcandidateNavigation.PhoneNumber);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_email), paramInterview.IdcandidateNavigation.Email);
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_phrase), keywords.binding_phrase);

                // build email plain text
                mailPlainTextTemplate = mailPlainTextTemplate.Replace(nameof(keywords.binding_recruitername), paramInterview.IdrecruiterNavigation.Name);
                mailPlainTextTemplate = mailPlainTextTemplate.Replace(nameof(keywords.binding_candidatename), paramInterview.IdcandidateNavigation.Name);
                mailPlainTextTemplate = mailPlainTextTemplate.Replace(nameof(keywords.binding_companyname), paramInterview.IdcompanyNavigation.Name);

                // cv attachment
                string tempAttach = string.Empty;

                foreach (string itemAttach in keywords.binding_cvuri)
                {
                    string[] btndata = itemAttach.Split('|');
                    tempAttach += @$"<a href = ""{btndata[1]}"" target = ""_blank"" class=""btn"" > {btndata[0]}</a>";
                }
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_cvuri), tempAttach);

                // social media
                string tempSocialMedia = string.Empty;
                foreach (string itemAttach in keywords.binding_socialmedia)
                {
                    string[] temparray = itemAttach.Split('|');
                    tempSocialMedia += @$"<a href=""{temparray[1]}"" class=""soc-btn gh"">{temparray[0]}</a>";
                }
                mailHtmlTemplate = mailHtmlTemplate.Replace(nameof(keywords.binding_socialmedia), tempSocialMedia);
            }
            catch (Exception ex)
            {
                mailHtmlTemplate = $"{ex.Message } ; {ex.InnerException.Message}";
            }
            return (mailHtmlTemplate: mailHtmlTemplate, mailPlainTextTemplate: mailPlainTextTemplate);
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
        public async Task<ResponseModel> CreateProposalAsync(InterviewProposalDto interview)
        {
            try
            {
                if (interview.IdCandidate <= 0)
                {
                    _responseModel.Flag = (int)EnumStatusCatalog.Error;
                    _responseModel.Message = "* Incidence Id Candidate is required";
                }
                else
                {
                    InterviewDto item = new InterviewDto();
                    item.Candidate.IdCandidate = interview.IdCandidate;
                    item.Company.Name = interview.Company;
                    item.Provider = interview.Provider;
                    item.EconomicExpectations = interview.EconomicExpectations;
                    item.EconomicExpectationsOffered = interview.EconomicExpectationsOffered;
                    item.Recruiter.Email = interview.Email;
                    item.Recruiter.Phone = interview.Phone;
                    item.Recruiter.Name = interview.RecruiterName;
                    var result0 = await this.CreateAsync(item);
                    return result0;
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

                    var templates = _composeEmail(interview.Id, interview);
                    string htmlTemplate = templates.mailHtmlTemplate;
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

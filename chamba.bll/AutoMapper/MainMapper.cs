using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using chambapp.storage.Models;
using chambapp.dto;
namespace chambapp.bll.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Add as many of these lines as you need to map your objects

            CreateMap<RecruiterDto, User>()
                    .ForMember(d => d.Id, m => m.MapFrom(s => s.IdRecruiter))
                    .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                    .ForMember(d => d.Email, m => m.MapFrom(s => s.Email))
                    .ForMember(d => d.PhoneNumber, m => m.MapFrom(s => s.Phone))
                    .ForMember(d => d.IdstatusCatalog, m => m.MapFrom(s => s.CurrentState))
                    .ForMember(d => d.ReplyEmail, m => m.MapFrom(s => s.ReplyEmail));

            CreateMap<User, RecruiterDto> ()
                    .ForMember(d => d.IdRecruiter, m => m.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                    .ForMember(d => d.Email, m => m.MapFrom(s => s.Email))
                    .ForMember(d => d.Phone, m => m.MapFrom(s => s.PhoneNumber))
                    .ForMember(d => d.CurrentState, m => m.MapFrom(s => s.IdstatusCatalog))
                    .ForMember(d => d.ReplyEmail, m => m.MapFrom(s => s.ReplyEmail));

            CreateMap<CandidateDto, User>()
                    .ForMember(d => d.Id, m => m.MapFrom(s => s.IdCandidate))
                    .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                    .ForMember(d => d.Email, m => m.MapFrom(s => s.Email))
                    .ForMember(d => d.PhoneNumber, m => m.MapFrom(s => s.Phone))
                    .ForMember(d => d.Pwd, m => m.MapFrom(s => s.Pwd))
                    .ForMember(d => d.IdstatusCatalog, m => m.MapFrom(s => s.CurrentState))
                    .ForMember(d => d.Subject, m => m.MapFrom(s => s.EmailSubject))
                    .ForMember(d => d.ConfigurationEmail, m => m.MapFrom(s => s.EmailConfiguration))
                    .ForMember(d => d.ReplyEmail, m => m.MapFrom(s => s.EmailReply))
                    .ForMember(d => d.TemplateEmail, m => m.MapFrom(s => s.EmailTemplate))
                    .ForMember(d => d.KeywordsEmail, m => m.MapFrom(s => s.EmailKeyword));
            CreateMap<User, CandidateDto> ()
                    .ForMember(d => d.IdCandidate, m => m.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                    .ForMember(d => d.Email, m => m.MapFrom(s => s.Email))
                    .ForMember(d => d.Phone, m => m.MapFrom(s => s.PhoneNumber))
                    .ForMember(d => d.Pwd, m => m.MapFrom(s => s.Pwd))
                    .ForMember(d => d.CurrentState, m => m.MapFrom(s => s.IdstatusCatalog))
                    .ForMember(d => d.EmailSubject, m => m.MapFrom(s => s.Subject))
                    .ForMember(d => d.EmailConfiguration, m => m.MapFrom(s => s.ConfigurationEmail))
                    .ForMember(d => d.EmailReply, m => m.MapFrom(s => s.ReplyEmail))
                    .ForMember(d => d.EmailTemplate, m => m.MapFrom(s => s.TemplateEmail))
                    .ForMember(d => d.EmailKeyword, m => m.MapFrom(s => s.KeywordsEmail));

            //CreateMap<RecruiterDto, Recruiter>()
            //    .ForMember(d => d.Id, m => m.MapFrom(s => s.IdRecruiter))
            //    .ForMember(d => d.PhoneNumber, m => m.MapFrom(s => s.Phone));
            //CreateMap<Recruiter, RecruiterDto>()
            //    .ForMember(d => d.IdRecruiter, m => m.MapFrom(s => s.Id))
            //    .ForMember(d => d.Phone, m => m.MapFrom(s => s.PhoneNumber));

            CreateMap<CompanyDto, Company>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdCompany))
                .ForMember(d => d.MapLat, m => m.MapFrom(s => s.Lat))
                .ForMember(d => d.MapLong, m => m.MapFrom(s => s.Lon))
                .ForMember(d => d.Address, m => m.MapFrom(s => s.Address));
            CreateMap<Company, CompanyDto>().ForMember(d => d.IdCompany, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Lat, m => m.MapFrom(s => s.MapLat))
                .ForMember(d => d.Lon, m => m.MapFrom(s => s.MapLong))
                .ForMember(d => d.Address, m => m.MapFrom(s => s.Address));

            CreateMap<Interview, InterviewDto>()
                .ForMember(d => d.IdInterview, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.CurrentState, m => m.MapFrom(s => s.IdstatusCatalog))
                .ForMember(d => d.Company, m => m.MapFrom(s => s.IdcompanyNavigation))
                .ForMember(d => d.Recruiter, m => m.MapFrom(s => s.IdrecruiterNavigation))
                .ForMember(d => d.Candidate, m => m.MapFrom(s => s.IdcandidateNavigation));
            
            CreateMap<InterviewDto, Interview>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdInterview))
                .ForMember(d => d.IdstatusCatalog, m => m.MapFrom(s => s.CurrentState))
                .ForMember(d => d.IdcompanyNavigation, m => m.MapFrom(s => s.Company))
                .ForMember(d => d.IdrecruiterNavigation, m => m.MapFrom(s => s.Recruiter))
                .ForMember(d => d.Idcandidate, m => m.MapFrom(s => s.Candidate.IdCandidate))
                .ForMember(d => d.IdcandidateNavigation, m => m.MapFrom(s => s.Candidate));
        }
    }

    public class MainMapper : IMainMapper
    {

        public MainMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            Mapper = mappingConfig.CreateMapper();
        }

        public IMapper Mapper { get; set; }
    }

    public interface IMainMapper
    {
        IMapper Mapper { get; set; }
    }


}

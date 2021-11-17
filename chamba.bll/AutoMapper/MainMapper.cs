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
            CreateMap<RecruiterDto, Recruiter>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdRecruiter))
                .ForMember(d => d.PhoneNumber, m => m.MapFrom(s => s.Phone));
            CreateMap<Recruiter, RecruiterDto>()
                .ForMember(d => d.IdRecruiter, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Phone, m => m.MapFrom(s => s.PhoneNumber));

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
                .ForMember(d => d.Company, m => m.MapFrom(s => s.IdcompanyNavigation))
                .ForMember(d => d.Recruiter, m => m.MapFrom(s => s.IdrecruiterNavigation));
            CreateMap<InterviewDto, Interview>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdInterview))
                .ForMember(d => d.IdcompanyNavigation, m => m.MapFrom(s => s.Company))
                .ForMember(d => d.IdrecruiterNavigation, m => m.MapFrom(s => s.Recruiter));
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

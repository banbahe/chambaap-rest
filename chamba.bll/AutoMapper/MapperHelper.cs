using AutoMapper;
using chamba.dto;
using chamba.storage.Models;
using System.Collections.Generic;
using System.Linq;

namespace chamba.bll.AutoMapper
{
    public static class MapperHelper
    {
        public static IMapper StaticMapper;
        //static IMapper staticMapper;
        static MapperHelper()
        {
            /*
             *  CreateMap<Azmoon, AzmoonViewModel>()
            .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => 
 s.CreatedBy.UserName))
            .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => 
s.ModifiedBy.UserName)).IgnoreAllNonExisting();
             */
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<List<Interview>, List<InterviewDto>>().ReverseMap();

                cfg.CreateMap<Interview, InterviewDto>()
                  .ForMember(d => d.Idinterview, m => m.MapFrom(s => s.Id))
                  .ForMember(d => d.Company, m => m.MapFrom(s => s.IdcompanyNavigation))
                  .ForMember(d => d.Recruiter, m => m.MapFrom(s => s.IdrecruiterNavigation));
                // .PreserveReferences();
                cfg.CreateMap<InterviewDto, Interview>()
                   .ForMember(d => d.Id, m => m.MapFrom(s => s.Idinterview))
                   .ForMember(d => d.IdrecruiterNavigation, m => m.MapFrom(s => s.Recruiter));
                   //.PreserveReferences(); ;
                cfg.CreateMap<Recruiter, RecruiterDto>()
                   .ForMember(d => d.Idrecruiter, m => m.MapFrom(s => s.Id))
                   .PreserveReferences();
                cfg.CreateMap<RecruiterDto, Recruiter>()
                   .ForMember(d => d.Id, m => m.MapFrom(s => s.Idrecruiter))
                   .PreserveReferences();

                cfg.CreateMap<Company, CompanyDto>()
                   .ForMember(d => d.Idcompany, m => m.MapFrom(s => s.Id));
                  // .PreserveReferences();
                cfg.CreateMap<CompanyDto, Recruiter>()
                   .ForMember(d => d.Id, m => m.MapFrom(s => s.Idcompany));
                   //.PreserveReferences();


            });

            StaticMapper = config.CreateMapper();
        }

        public static List<TDestination> MapList<TSource, TDestination>(this IMapper mapper, List<TSource> source)
        {
            return source.Select(x => mapper.Map<TDestination>(x)).ToList();
        }
        //First approach, create a mapper and use it from a static method
        public static T MapFrom<T>(object entity)
        {
            return StaticMapper.Map<T>(entity);
        }

        //Second approach (if users need to use their own types which are not known by this project)
        //Create you own mapper interface ans return it
        public static IMyMapper GetMapperFor<TSource, TDestination>()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = config.CreateMapper();

            return new MyMapper(mapper);
        }

        //Third sample, create and use mapper inside a static helper method
        //This is for mapping foreign types that this project does not 
        //include (e.g POCO or model types in other projects)
        public static TDestination Map<TDestination, TSource>(TSource entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = config.CreateMapper();

            return mapper.Map<TDestination>(entity);
        }
    }
    public interface IMyMapper
    {
        T MapFrom<T>(object entity);
    }

    class MyMapper : IMyMapper
    {
        IMapper mapper;

        public MyMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public T MapFrom<T>(object entity)
        {
            return mapper.Map<T>(entity);
        }
    }
}

//public static class Mapping
//{
//    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
//    {
//        var config = new MapperConfiguration(cfg =>
//        {
//            // This line ensures that internal properties are also mapped over.
//            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
//            cfg.AddProfile<MappingProfile>();
//        });
//        var mapper = config.CreateMapper();
//        return mapper;
//    });

//    public static IMapper Mapper => Lazy.Value;
//}
//public class MappingProfile : Profile
//{
//    public MappingProfile()
//    {

//        CreateMap<Interview, InterviewDto>()
//                 .ForMember(d => d.idinterview, m => m.MapFrom(s => s.Id))
//                 .ForMember(d => d.recruiter, m => m.MapFrom(s => s.IdrecruiterNavigation))
//                 .PreserveReferences();
//        CreateMap<InterviewDTO, Interview>()
//                 .ForMember(d => d.Id, m => m.MapFrom(s => s.idinterview))
//                 .ForMember(d => d.IdrecruiterNavigation, m => m.MapFrom(s => s.recruiter))
//                 .PreserveReferences();
//        CreateMap<List<InterviewDTO>, List<Interview>>().PreserveReferences();
//        CreateMap<List<Interview>, List<InterviewDTO>>().PreserveReferences();

//        CreateMap<Recruiter, RecruiterDTO>()
//                 .ForMember(d => d.idrecruiter, m => m.MapFrom(s => s.Id))
//                 .PreserveReferences();
//        CreateMap<RecruiterDTO, Recruiter>()
//                 .ForMember(d => d.Id, m => m.MapFrom(s => s.idrecruiter))
//                 .PreserveReferences();

//        // Additional mappings here...
//    }
//}
//public static class ObjectMapper
//{
//    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
//    {
//        var config = new MapperConfiguration(cfg =>
//        {
//            // This line ensures that internal properties are also mapped over.
//            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
//            cfg.AddProfile<CustomDtoMapper>();
//        });
//        var mapper = config.CreateMapper();
//        return mapper;
//    });

//    public static IMapper Mapper => Lazy.Value;
//}
//public class CustomDtoMapper : Profile
//{
//    public CustomDtoMapper()
//    {
//        //CreateMap<Entity, EntityDto>().ForMember(dto => dto.Currency, map => map.MapFrom(source => new Currency
//        //{
//        //    Code = source.CurrencyCode,
//        //    Value = source.CurrencyValue.ToString("0.00")
//        //}));

//        // All other mappings goes here

//        CreateMap<Interview, InterviewDTO>()
//             .ForMember(d => d.idinterview, m => m.MapFrom(s => s.Id))
//             .ForMember(d => d.recruiter, m => m.MapFrom(s => s.IdrecruiterNavigation))
//             .PreserveReferences();
//        CreateMap<InterviewDTO, Interview>()
//         .ForMember(d => d.Id, m => m.MapFrom(s => s.idinterview))
//         .ForMember(d => d.IdrecruiterNavigation, m => m.MapFrom(s => s.recruiter))
//         .PreserveReferences();

//        CreateMap<List<Interview>, List<InterviewDTO>>().PreserveReferences();
//        CreateMap<List<InterviewDTO>, List<Interview>>().PreserveReferences();

//        CreateMap<Recruiter, RecruiterDTO>()
//                 .ForMember(d => d.idrecruiter, m => m.MapFrom(s => s.Id))
//                 .PreserveReferences();
//        CreateMap<RecruiterDTO, Recruiter>()
//                 .ForMember(d => d.Id, m => m.MapFrom(s => s.idrecruiter))
//                 .PreserveReferences();
//    }
//}

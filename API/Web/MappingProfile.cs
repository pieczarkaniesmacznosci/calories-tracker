using API.Web.Entities;
using API.Web.Dtos;
using AutoMapper;
using System;

namespace API.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
            .ReverseMap();

            CreateMap<Meal, MealDto>();

            CreateMap<MealDto, Meal>()
            .ForMember(dest=>dest.MealName,
            opt =>{
                opt.MapFrom<DefaultMealNameResolver>();
            })
            .ForMember(x=>x.MealLogs, opt=>opt.Ignore());

            CreateMap<MealLog, MealLogDto>()
            .MaxDepth(1)
            .PreserveReferences()
            .ReverseMap();

            CreateMap<MealProduct, MealProductDto>()
            .ReverseMap();

            CreateMap<UserNutrition, UserNutritionDto>()
            .ReverseMap();

            CreateMap<UserWeight, UserWeightDto>()
            .ReverseMap();
        }
    }
    
    public class DefaultMealNameResolver : IValueResolver<MealDto, Meal, string>
    {
        public string Resolve(MealDto source, Meal destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrWhiteSpace(source.MealName))
            {
                return source.MealName;
            }
            else
            {
                return string.Format("Meal@{0:HH:mm-ddMMyyyy}", DateTime.Now);
            }
        }
    }
}
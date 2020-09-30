using API.Web.Entities;
using API.Web.Dtos;
using AutoMapper;

namespace API.Web
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() {
            CreateMap<Product, ProductDto>()
            .ForMember(x=>x.Id,opt =>opt.Ignore())
            .ReverseMap();
            CreateMap<Meal, MealDto>()
                .ForMember(
                    dest=>dest.MealProducts, 
                    opt=>opt.MapFrom(src=>src.MealProducts))
            .ForMember(x=>x.Id,opt =>opt.Ignore())
            .ReverseMap();
            CreateMap<MealProduct, MealProductDto>()
            .ForMember(x=>x.Id,opt =>opt.Ignore())
            .ReverseMap();

            CreateMap<UserNutrition, UserNutritionDto>()
            .ReverseMap();

            CreateMap<UserWeight, UserWeightDto>()
            .ReverseMap();
        }
    }
}
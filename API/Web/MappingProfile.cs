using API.Web.Entities;
using API.Web.Dtos;
using AutoMapper;

namespace API.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
            .ReverseMap();
            CreateMap<Meal, MealDto>()
                .ForMember(
                    dest => dest.MealProducts,
                    opt => opt.MapFrom(src => src.MealProducts))
            .ReverseMap();
            CreateMap<MealProduct, MealProductDto>()
            .ReverseMap();

            CreateMap<UserNutrition, UserNutritionDto>()
            .ReverseMap();

            CreateMap<UserWeight, UserWeightDto>()
            .ReverseMap();
        }
    }
}
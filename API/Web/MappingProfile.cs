using API.Web.Entities;
using API.Web.Dtos;
using AutoMapper;

namespace API.Web
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Meal, MealDto>();
            CreateMap<MealDto, Meal>();
            // CreateMap<MealProduct, MealProductDto>();
            // CreateMap<MealProductDto, MealProduct>();
        }
    }
}
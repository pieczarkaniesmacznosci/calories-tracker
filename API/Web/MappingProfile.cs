using API.Web.Entities;
using API.Web.Dtos;
using AutoMapper;

namespace API.Web
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() {
            CreateMap<Product, ProductDto>()
            .ForMember(x=>x.Id,opt =>opt.Ignore());
            CreateMap<ProductDto, Product>()
            .ForMember(x=>x.Id,opt =>opt.Ignore());
            CreateMap<Meal, MealDto>()
                .ForMember(
                    dest=>dest.MealProducts, 
                    opt=>opt.MapFrom(src=>src.MealProducts))
            .ForMember(x=>x.Id,opt =>opt.Ignore());
            CreateMap<MealDto, Meal>()
                .ForMember(
                    dest=>dest.MealProducts, 
                    opt=>opt.MapFrom(src=>src.MealProducts));
            CreateMap<MealProduct, MealProductDto>()
            .ForMember(x=>x.Id,opt =>opt.Ignore());
            CreateMap<MealProductDto, MealProduct>();
        }
    }
}
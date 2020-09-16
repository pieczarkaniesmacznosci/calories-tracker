using API.Web.Entities;
using API.Web.Models;
using AutoMapper;

namespace API.Web
{
     public class MappingProfile : Profile {
     public MappingProfile() {
         CreateMap<Product, ProductDto>();
         CreateMap<ProductDto, Product>();
     }
 }
}
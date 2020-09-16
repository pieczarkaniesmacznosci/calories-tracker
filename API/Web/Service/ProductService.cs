using System.Collections.Generic;
using System.Linq;
using API.Web.Entities;
using API.Web.Models;
using API.Web.Repositories;
using AutoMapper;

namespace API.Web.Service
{
    public class ProductService : IService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.All();
        }

        public Product GetProduct(int id)
        {
            return _productRepository.Get(id);
        }

        public IEnumerable<Product> GetProducts(string productName)
        {
            return _productRepository.All().Where(x=>x.Name.StartsWith(productName,System.StringComparison.InvariantCulture));
        }

        public Product AddProduct(ProductModel product)
        {
            var entity = _mapper.Map<Product>(product);
            var result = _productRepository.Add(entity);
            _productRepository.SaveChanges();
            return result;
        }

        public Product EditProduct(ProductModel product)
        {
            var entity = _mapper.Map<Product>(product);
            var result = _productRepository.Update(entity);
            _productRepository.SaveChanges();
            return result;
        }

        public Product DeleteProduct(int id)
        {
            var productToDelete = _productRepository.Find(x=>x.Id == id).SingleOrDefault();
            var result = _productRepository.Delete(productToDelete);
            _productRepository.SaveChanges();
            return result;
        }
    }
}
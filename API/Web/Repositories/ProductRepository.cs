using System.Linq;
using API.Web.DbContexts;
using API.Web.Entities;

namespace API.Web.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>
    {
        public ProductRepository(CaloriesLibraryContext context) : base(context)
        {
        }

        public override ProductEntity Update(ProductEntity entity)
        {
            var product = _context.Products.Single(p => p.Id == entity.Id);
            
            product.Name = entity.Name;
            product.Kcal = entity.Kcal;
            product.Carbohydrates = entity.Carbohydrates;
            product.Protein = entity.Protein;
            product.Fat = entity.Fat;

            return base.Update(product);
        } 
    }
}
using Data.DbContexts;
using Data.Entities;

namespace Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(CaloriesLibraryContext context) : base(context)
        {
        }

        public override Product Update(Product entity)
        {
            var product = _context.Products.Single(p => p.Id == entity.Id);

            product.Name = entity.Name;
            product.Kcal = entity.Kcal;
            product.Carbohydrates = entity.Carbohydrates;
            product.Protein = entity.Protein;
            product.Fat = entity.Fat;

            return base.Update(product);
        }

        public override Product Delete(Product entity)
        {
            var product = _context.Products.Single(p => p.Id == entity.Id);

            return base.Delete(product);
        }
    }
}

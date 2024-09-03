using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCRepoPatternDemo.Models;
using MVCRepoPatternDemo.Repository;

namespace MVCRepoPatternDemo.Service
{
    public class ProductService : IProduct ,ICategory
    {
        private readonly ProductContext _contxt;

        public ProductService(ProductContext contxt)
        {
            _contxt = contxt; 
        }
        public IEnumerable<ProductCl> GetAll()
        {
            return _contxt.Products.ToList();
        }
        public ProductCl? GetProduct(int id)
        {
           ProductCl? pro = _contxt.Products.Include(c=>c.categories).FirstOrDefault(p => p.ProId == id);
           
            return pro;
        }
        public void AddProduct(ProductCl pro)
        {
            _contxt.Products.Add(pro);
            _contxt.SaveChanges();
        }

        public void DeleteProduct(ProductCl pro)
        {
            ProductCl?  pr = _contxt.Products.FirstOrDefault(p =>p.ProId == pro.ProId);
            _contxt.Products.Remove(pr);
            _contxt.SaveChanges();
        }

        public ProductCl UpdateProduct(int id,ProductCl pro)
        {
            ProductCl? prod = _contxt.Products.FirstOrDefault(p => p.ProId == id);
            if (prod == null)
            {
                throw new Exception("Product not found");
            }

            var existingCategory = _contxt.Categories.FirstOrDefault(c => c.CategoryId == pro.CatId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }

            // Assign values from the input product object to the existing product entity
            prod.ProName = pro.ProName;
            prod.Price = pro.Price;
            prod.WarrantyinYears = pro.WarrantyinYears;
            prod.CatId = pro.CatId; // Ensure the CatId is updated correctly

            _contxt.SaveChanges();
            return prod;
        }

        public IEnumerable<ProductCl> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _contxt.Products.ToList();
            }

            searchTerm = searchTerm.ToLower();
            return _contxt.Products
                          .Where(p => p.ProName.ToLower().Contains(searchTerm) ||
                                      p.categories.CatName.ToLower().Contains(searchTerm))
                          .Include(p => p.categories)
                          .ToList();
        }

        public IEnumerable<ProductCl> FilterProducts(decimal? minPrice, decimal? maxPrice)
        {
            var query = _contxt.Products.AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return query.Include(p => p.categories).ToList();
        }


        public IEnumerable<Category> GetAllCategories()
            {
                return _contxt.Categories.ToList();
            }
       
    }
}

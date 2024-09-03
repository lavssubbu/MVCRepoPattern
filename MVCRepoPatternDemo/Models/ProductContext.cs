
using Microsoft.EntityFrameworkCore;

namespace MVCRepoPatternDemo.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductCl> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public ProductContext(DbContextOptions<ProductContext> opt) : base(opt) { }

        //Seeding of Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new Category() { CategoryId = 1, CatName = "Electronics" },
                new Category() { CategoryId = 2, CatName = "Clothing" });
            modelBuilder.Entity<ProductCl>()
                .HasData(new ProductCl() { ProId = 101, ProName = "Refrigerator", Price = 42000 });

            
            modelBuilder.Entity<ProductCl>()
                .Property(p => p.Price)
                  .HasColumnType("decimal(18,2)");

           
        }
    }
}

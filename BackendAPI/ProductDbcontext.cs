using Microsoft.EntityFrameworkCore;

namespace BackendAPI
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
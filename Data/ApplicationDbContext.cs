using Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Product>  product { get; set; }
        public DbSet<ProductDetails> productDetails { get; set; }
        public DbSet<Customers> customers { get; set; }
        public DbSet<Invoice> invoice { get; set; }
        public DbSet<PaymentAccept> paymentAccept { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<Payment> payment { get; set; }


    }
}

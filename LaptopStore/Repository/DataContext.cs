using LaptopStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
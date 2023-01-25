using CustomersApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomersApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

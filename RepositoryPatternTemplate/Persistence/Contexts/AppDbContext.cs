using Microsoft.EntityFrameworkCore;
using PikiouAPI.Domain.Models;

namespace PikiouAPI.Persistence.Contexts
{
    /// <summary>
    /// Application database context (ORM). Represation of all database tables as DbSets are found here. 
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Courier>   Couriers { get; set; }
        public DbSet<Order>     Orders { get; set; }
        public DbSet<Package>   Packages { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}

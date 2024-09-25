using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Artwork> Artwork { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Workshop> Workshop { get; set; }

        public DatabaseContext(DbContextOptions option)
            : base(option) { }
    }
}

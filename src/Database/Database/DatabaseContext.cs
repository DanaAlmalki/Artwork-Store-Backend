using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;
using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Artwork> Artwork { get; set; }
        public DbSet<Artwork> ArtworkCategory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetail { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Workshop> Workshop { get; set; }
        public DbSet<User> User { get; set; }

        public DatabaseContext(DbContextOptions option)
            : base(option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<UserRole>();
        }
    }
}

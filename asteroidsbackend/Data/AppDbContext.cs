using Microsoft.EntityFrameworkCore;
using asteroidsbackend.Models;

namespace asteroidsbackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<BaseItem> Items { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<PowerUp> PowerUps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<BaseItem>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<Weapon>("Weapon")
                .HasValue<PowerUp>("PowerUp");
        }
    }
}

namespace WebOdevi.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<Kullanici>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        // Add DbSet properties for your models
        public DbSet<Berber> Berberler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }  
    }
}
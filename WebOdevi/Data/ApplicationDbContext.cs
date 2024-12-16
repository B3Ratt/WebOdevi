using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebOdevi.Models;

public class ApplicationDbContext : IdentityDbContext<Kullanici>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Berber> Berberler { get; set; }
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Randevu> Randevular { get; set; }
}

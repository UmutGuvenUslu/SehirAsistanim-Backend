using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Enums;

public class SehirAsistaniDbContext : DbContext
{

    public SehirAsistaniDbContext(DbContextOptions<SehirAsistaniDbContext> options)
        : base(options)
    {
    }

    #region DbSets
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<BelediyeBirimi> BelediyeBirimleri { get; set; }
    public DbSet<SikayetTuru> SikayetTurleri { get; set; }
    public DbSet<Sikayet> Sikayetler { get; set; }
    public DbSet<SikayetDogrulama> SikayetDogrulamalari { get; set; }
    public DbSet<SikayetLog> SikayetLoglari { get; set; }
    public DbSet<SikayetCozum> SikayetCozumleri { get; set; }
    public DbSet<Bildirim> Bildirimler { get; set; }

    #endregion

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Kullanici>()
            .Property(e => e.Rol)
            .HasConversion(
                v => v.ToString(),  // Enum'u string'e çevirirken
                v => (rolturu)Enum.Parse(typeof(rolturu), v) // String'den enum'a dönüşüm
            );

    }
    #endregion

}

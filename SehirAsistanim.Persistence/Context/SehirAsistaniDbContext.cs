using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        // Enum converters
        var rolConverter = new EnumToStringConverter<rolturu>();
        var durumConverter = new EnumToStringConverter<sikayetdurumu>();

        modelBuilder.Entity<Kullanici>()
            .Property(e => e.Rol)
            .HasConversion(rolConverter);

        modelBuilder.Entity<Sikayet>()
            .Property(e => e.Durum)
            .HasConversion(durumConverter);
    }
    #endregion
}

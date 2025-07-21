using System.ComponentModel.DataAnnotations;
using SehirAsistanim.Domain.Enums;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistanim.Domain.Entities
{
    public class Sikayet : IEntitiy
    {
     
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int SikayetTuruId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FotoUrl { get; set; }
        public DateTime GonderilmeTarihi { get; set; } = DateTime.UtcNow;
        public DateTime CozulmeTarihi { get; set; }
        public sikayetdurumu Durum { get; set; }
        public int DogrulanmaSayisi { get; set; } = 1;
        public double DuyguPuani { get; set; } = 0.0;
        public bool Silindimi { get; set; } = false;
        public int CozenBirimId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SehirAsistanim.Domain.Enums;

namespace SehirAsistanim.Domain.Entities
{
    public class Sikayet
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Baslik { get; set; }
        public int Aciklama { get; set; }
        public int SikayetTuruId { get; set; }
        public Geometry Konum { get; set; }
        public string FotoUrl { get; set; }
        public DateTime GonderilmeTarihi { get; set; }
        public DateTime CozulmeTarihi { get; set; }
        public sikayetdurumu Durum {  get; set; }
        public int DogrulanmaSayisi { get; set; }
        public double DuyguPuani {  get; set; }
        public bool Silindimi { get; set; }
        public int CozenBirimId { get; set; }

     
    }
}

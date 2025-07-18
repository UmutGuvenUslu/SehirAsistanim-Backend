using System.ComponentModel.DataAnnotations;

namespace SehirAsistanim.Domain.Entities
{
    public class Bildirim
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public DateTime GonderimTarihi { get; set; }
        public bool OkunduMu { get; set; } = false;
    }
}

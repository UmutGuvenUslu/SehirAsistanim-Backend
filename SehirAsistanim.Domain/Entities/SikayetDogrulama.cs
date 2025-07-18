using System.ComponentModel.DataAnnotations;

namespace SehirAsistanim.Domain.Entities
{
    public class SikayetDogrulama
    {
        [Key]
        public int Id { get; set; }
        public int SikayetId { get; set; }
        public int KullaniciId { get; set; }       
        public DateTime DogrulamaTarihi { get; set; }
    }
}

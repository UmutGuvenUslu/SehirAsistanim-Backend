using System.ComponentModel.DataAnnotations;


namespace SehirAsistanim.Domain.Entities
{
    public class SikayetCozum
    {
        [Key]
        public int Id { get; set; }
        public int SikayetId { get; set; }            
        public int CozenKullaniciId { get; set; }   
        public string? CozumAciklamasi { get; set; }
        public string? CozumFotoUrl { get; set; }
        public DateTime CozumeTarihi { get; set; }
    }
}

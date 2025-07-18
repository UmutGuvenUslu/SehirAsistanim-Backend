using System.ComponentModel.DataAnnotations;


namespace SehirAsistanim.Domain.Entities
{
    public class SikayetLog
    {
        [Key]
        public int Id { get; set; }
        public int SikayetId { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}

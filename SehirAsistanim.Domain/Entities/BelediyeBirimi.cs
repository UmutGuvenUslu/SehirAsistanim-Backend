using System.ComponentModel.DataAnnotations;

namespace SehirAsistanim.Domain.Entities
{
    public class BelediyeBirimi
    {
        [Key]
        public int Id { get; set; }
        public string BirimAdi { get; set; } = null!;
        public string? EmailAdresi { get; set; }
    }
}

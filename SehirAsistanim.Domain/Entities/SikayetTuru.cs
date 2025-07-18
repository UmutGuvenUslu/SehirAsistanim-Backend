using System.ComponentModel.DataAnnotations;


namespace SehirAsistanim.Domain.Entities
{
    public class SikayetTuru
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int VarsayilanBirimId { get; set; }
    }
}

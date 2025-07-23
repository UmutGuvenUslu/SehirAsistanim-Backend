using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SehirAsistanim.Domain.Entities
{
    [Table("sikayetloglari")]
    public class SikayetLog
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("sikayetid")]
        public int SikayetId { get; set; }
        [Column("aciklama")]
        public string Aciklama { get; set; }
        [Column("tarih")]
        public DateTime Tarih { get; set; }


    }
}

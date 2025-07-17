using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

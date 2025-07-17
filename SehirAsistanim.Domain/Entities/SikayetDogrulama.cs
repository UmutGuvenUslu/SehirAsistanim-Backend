using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

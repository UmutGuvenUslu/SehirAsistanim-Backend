using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

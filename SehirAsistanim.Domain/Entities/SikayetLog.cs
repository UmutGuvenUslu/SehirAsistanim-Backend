using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

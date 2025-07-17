using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

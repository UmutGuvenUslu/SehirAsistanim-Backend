using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirAsistanim.Domain.Dto_s
{
    public class AddCozumFormRequestDto
    {
        public int SikayetId { get; set; }
        public int CozenKullaniciId { get; set; }
        public string Aciklama { get; set; }
        public string? FotoUrl { get; set; }

    }
}

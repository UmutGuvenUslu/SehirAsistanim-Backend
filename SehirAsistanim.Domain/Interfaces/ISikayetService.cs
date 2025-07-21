using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Entities;

namespace SehirAsistanim.Domain.Interfaces
{
    public interface ISikayetService
    {
        
        Task<List<Sikayet>> GetAll();
        Task<Sikayet> GetById(int sikayetId);
        Task<Sikayet> AddSikayet(Sikayet sikayet);

    }
}

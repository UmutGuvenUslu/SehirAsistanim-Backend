

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Dto_s;
using SehirAsistanim.Domain.Entities;

namespace SehirAsistanim.Domain.Interfaces
{
    public interface ISikayetService
    {

        Task<List<SikayetDetayDto>> GetAll();
        Task<Sikayet> GetById(int sikayetId);
        Task<Sikayet> AddSikayet(Sikayet sikayet);

        Task<bool> UpdateDurumAsCozuldu(int sikayetId, int cozenBirimId);
        //Task<bool> SoftDelete(int sikayetId);
        Task<bool> IncrementDogrulama(int sikayetId);
        Task<int> TotalSikayetSayisi();
        Task<int> CozulenSikayetSayisi();
        Task<int> BekleyenSikayetSayisi();

    }
}
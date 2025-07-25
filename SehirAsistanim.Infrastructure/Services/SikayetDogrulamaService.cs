using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistanim.Infrastructure.Services
{
    public class SikayetDogrulamaService:ISikayetDogrulamaService
    {
        public readonly IUnitOfWork _unitofWork;

        public SikayetDogrulamaService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        #region IncrementDogrulama
        public async Task<bool> IncrementDogrulama(int sikayetId,int kullaniciId)
        {
            var sikayetdogrulamatablosu =  _unitofWork.Repository<SikayetDogrulama>().GetAll().Result.Where(s=>s.SikayetId==sikayetId).ToList();
            foreach(var k in sikayetdogrulamatablosu)
            {
                if (k.KullaniciId == kullaniciId)
                {
                    return false; 
                }
            }
            var sikayet = await _unitofWork.Repository<Sikayet>().GetById(sikayetId);
            sikayet.DogrulanmaSayisi++;
            await _unitofWork.Repository<Sikayet>().Update(sikayet);
            await _unitofWork.CommitAsync();
            return true;
        }
        #endregion
    }
}

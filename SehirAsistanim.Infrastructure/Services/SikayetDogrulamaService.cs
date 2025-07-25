using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistanim.Infrastructure.Services
{
    public class SikayetDogrulamaService : ISikayetDogrulamaService
    {
        public readonly IUnitOfWork _unitofWork;

        public SikayetDogrulamaService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        #region SikayetDogrulamaAddKullanici
        public async Task<bool> SikayetDogrulamaAddKullanici(int sikayetId, int kullaniciId)
        {
            var sikayetdogrulama = new SikayetDogrulama
            {
                SikayetId = sikayetId,
                KullaniciId = kullaniciId,
                DogrulamaTarihi = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
            };
            await _unitofWork.Repository<SikayetDogrulama>().Add(sikayetdogrulama);
            await _unitofWork.CommitAsync();
            return true;
        }
        #endregion



        #region IncrementDogrulama
        public async Task<bool> IncrementDogrulama(int sikayetId, int kullaniciId)
        {
            try
            {
                // Sadece ilgili kullanıcının bu şikayeti doğrulayıp doğrulamadığını kontrol ediyoruz
                var dahaOnceDogruladiMi = await _unitofWork.Repository<SikayetDogrulama>()
                    .GetAll()
                    .AnyAsync(s => s.SikayetId == sikayetId && s.KullaniciId == kullaniciId);

                if (dahaOnceDogruladiMi)
                {
                    return false;
                }

                var sikayet = await _unitofWork.Repository<Sikayet>().GetById(sikayetId);
                if (sikayet == null) return false;

                sikayet.DogrulanmaSayisi++;
                await _unitofWork.Repository<Sikayet>().Update(sikayet);

                await SikayetDogrulamaAddKullanici(sikayetId, kullaniciId);
                await _unitofWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                return false;
            }
        }
        #endregion
    }
}


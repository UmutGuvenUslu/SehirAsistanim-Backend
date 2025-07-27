using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;
using SehirAsistanim.Infrastructure.UnitOfWork;

namespace SehirAsistanim.Infrastructure.Services
{
    public class SikayetCozumService : ISikayetCozumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SikayetCozumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Belediye biriminin görebileceği şikayetleri listele
        public async Task<List<Sikayet>> GetSikayetlerForBirimAsync(string birimAdi)
        {
            return await _unitOfWork.Repository<Sikayet>().
                GetQueryable()
                .Include(s => s.Kullanici)
                .Include(s => s.SikayetTuru)
                .Include(s => s.CozenBirim)
                .Include(s => s.sikayetCozum)
                .Where(s => s.CozenBirim != null &&
                            s.CozenBirim.BirimAdi == birimAdi &&
                            !s.Silindimi)
                .ToListAsync();
        }

        // Şikayete çözüm formu ekle (bir kez eklenebilir)
        public async Task<bool> AddCozumFormAsync(int sikayetId, int cozenKullaniciId, string aciklama, string? fotoUrl)
        {
            var sikayetRepo = _unitOfWork.Repository<Sikayet>();
            var cozumRepo = _unitOfWork.Repository<SikayetCozum>();

            var sikayet = await sikayetRepo.GetQueryable()
                .Include(s => s.sikayetCozum)
                .FirstOrDefaultAsync(s => s.Id == sikayetId);

            if (sikayet == null) return false;

            if (sikayet.sikayetCozum != null && sikayet.sikayetCozum.Any())
                return false;

            var cozum = new SikayetCozum
            {
                SikayetId = sikayetId,
                CozenKullaniciId = cozenKullaniciId,
                CozumAciklamasi = aciklama,
                CozumFotoUrl = fotoUrl,
                CozumeTarihi = DateTime.UtcNow
            };

            await cozumRepo.Add(cozum);
            await _unitOfWork.CommitAsync();

            return true;
        }

        // Tek bir şikayetin tür doğru mu alanını ayarla
        public async Task<bool> SetSikayetTurDogruMuAsync(int sikayetId, bool dogruMu)
        {
            var sikayetRepo = _unitOfWork.Repository<Sikayet>();
            var sikayet = await sikayetRepo.GetById(sikayetId);

            if (sikayet == null) return false;

            sikayet.turdogrumu = dogruMu;
            sikayetRepo.Update(sikayet);
            await _unitOfWork.CommitAsync();

            return true;
        }

        // Çoklu şikayet ID'leri için tür doğru mu alanını topluca true yap
        public async Task<bool> SetSikayetTurDogruMuBulkAsync(List<int> sikayetIdListesi)
        {
            var sikayetRepo = _unitOfWork.Repository<Sikayet>();

            var sikayetler = sikayetRepo.GetAll().Result
                .Where(s => sikayetIdListesi.Contains(s.Id));

            foreach (var sikayet in sikayetler)
            {
                sikayet.turdogrumu = true;
                sikayetRepo.Update(sikayet);
            }

            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

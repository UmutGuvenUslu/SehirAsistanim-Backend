using Microsoft.EntityFrameworkCore;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Enums;
using SehirAsistanim.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirAsistanim.Infrastructure.Services
{
    public class SikayetCozumService : ISikayetCozumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SikayetCozumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // Normalize fonksiyonu: küçük harf yapar, boşlukları ve Türkçe karakterleri sadeleştirir
        private static string Normalize(string text)
        {
            return string.Concat(text
                .ToLowerInvariant()
                .Replace("ı", "i")
                .Replace("ç", "c")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ü", "u")
                .Replace("ğ", "g")
                .Where(c => !char.IsWhiteSpace(c)));
        }

        public async Task<List<Sikayet>> GetSikayetlerForBirimAsync(string birimAdi)
        {
            var normalizedInput = Normalize(birimAdi);

            return _unitOfWork.Repository<Sikayet>()
                .GetQueryable()
                .Include(s => s.Kullanici)
                .Include(s => s.SikayetTuru)
                .Where(s => s.Durum == sikayetdurumu.Inceleniyor && s.SikayetTuru.Ad != null)
                .AsEnumerable() 
                .Where(s => Normalize(s.SikayetTuru.Ad).Contains(normalizedInput)) 
                .ToList(); 
        }

        public async Task<bool> AddCozumFormAsync(int sikayetId, int cozenKullaniciId, string aciklama, string? fotoUrl)
        {
            var sikayet = await _unitOfWork.Repository<Sikayet>().GetById(sikayetId);
            if (sikayet == null)
                return false;

            // Eğer zaten çözüm varsa false dönebiliriz
            var existingSolution =  _unitOfWork.Repository<SikayetCozum>().GetAll().Result.ToList().
                Find(c => c.SikayetId == sikayetId);
            if (existingSolution != null)
                return false;

            var newSolution = new SikayetCozum
            {
                SikayetId = sikayetId,
                CozenKullaniciId = cozenKullaniciId,
                CozumAciklamasi = aciklama,
                CozumFotoUrl = fotoUrl,
                CozumeTarihi = DateTime.UtcNow
            };

            await _unitOfWork.Repository<SikayetCozum>().Add(newSolution);

            // İstersen şikayetin durumunu güncelle
            sikayet.CozulmeTarihi = DateTime.UtcNow;

            _unitOfWork.Repository<Sikayet>().Update(sikayet);

            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> SetSikayetTurDogruMuAsync(int sikayetId, bool dogruMu)
        {
            var sikayet = await _unitOfWork.Repository<Sikayet>().GetById(sikayetId);
            if (sikayet == null)
                return false;

            sikayet.turdogrumu = dogruMu;
            await _unitOfWork.Repository<Sikayet>().Update(sikayet);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

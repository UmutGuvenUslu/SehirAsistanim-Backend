using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        // ROL ADINI normalize eden yardımcı fonksiyon
        private string NormalizeRolAdi(string rolAdi)
        {
            if (rolAdi.EndsWith("Birimi"))
                rolAdi = rolAdi.Substring(0, rolAdi.Length - "Birimi".Length);

            // Büyük harflerden önce boşluk ekle (Türkçe karakter destekli)
            var withSpaces = Regex.Replace(rolAdi, @"(?<!^)(?=[A-ZÇĞİÖŞÜ])", " ");

            // "ve" birleşikse boşluk ekle
            withSpaces = withSpaces.Replace("ve", " ve ");

            return withSpaces.Trim();
        }

        // Belediye biriminin görebileceği şikayetleri listele
        public async Task<List<Sikayet>> GetSikayetlerForBirimAsync(string roladi)
        {
            var normalizeRol = NormalizeRolAdi(roladi);

            return await _unitOfWork.Repository<Sikayet>()
                .GetQueryable()
                .Include(s => s.Kullanici)
                .Include(s => s.SikayetTuru)
                .Include(s => s.CozenBirim)
                .Include(s => s.SikayetCozumlar)  // navigation property ismiyle uyumlu
                .Where(s => s.CozenBirim != null &&
                            s.CozenBirim.BirimAdi.StartsWith(normalizeRol) &&
                            !s.Silindimi)
                .ToListAsync();
        }

        public async Task<bool> AddCozumFormAsync(int sikayetId, int cozenKullaniciId, string aciklama, string? fotoUrl)
        {
            var sikayetRepo = _unitOfWork.Repository<Sikayet>();
            var cozumRepo = _unitOfWork.Repository<SikayetCozum>();

            var sikayet = await sikayetRepo.GetQueryable()
                .Include(s => s.SikayetCozumlar)  // navigation property düzeltilmeli
                .FirstOrDefaultAsync(s => s.Id == sikayetId);

            if (sikayet == null) return false;

            if (sikayet.SikayetCozumlar != null && sikayet.SikayetCozumlar.Any())
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
    }
}

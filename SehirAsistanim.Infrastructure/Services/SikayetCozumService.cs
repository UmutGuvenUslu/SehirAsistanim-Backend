using Microsoft.EntityFrameworkCore;
using SehirAsistanim.Domain.Entities;
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

        public async Task<List<Sikayet>> GetSikayetlerForBirimAsync(string birimAdi)
        {
            var normalizedBirimAdi = birimAdi.Replace(" ", "").ToLowerInvariant();

            return await _unitOfWork.Repository<Sikayet>()
                .GetQueryable()
                .Include(s => s.Kullanici)
                .Include(s => s.SikayetTuru)
                .Include(s => s.CozenBirim)
                .Where(s => s.Durum.ToString() == "Onaylandı" &&
                            s.SikayetTuru.ToString().ToLower().Replace(" ", "") == normalizedBirimAdi)
                .ToListAsync();
        }

        public async Task<bool> AddCozumFormAsync(int sikayetId, int cozenKullaniciId, string aciklama, string? fotoUrl)
        {
            var sikayet = await _unitOfWork.Repository<Sikayet>().GetById(sikayetId);
            if (sikayet == null)
                return false;

            sikayet.Aciklama = aciklama;
            sikayet.FotoUrl = fotoUrl;
            sikayet.CozulmeTarihi = DateTime.UtcNow;
            sikayet.CozenBirimId = cozenKullaniciId;

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

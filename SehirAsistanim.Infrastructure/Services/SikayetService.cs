using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Enums;
using SehirAsistanim.Domain.Interfaces;
using SehirAsistanim.Infrastructure.UnitOfWork;

namespace SehirAsistanim.Infrastructure.Services
{
    public class SikayetService : ISikayetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtService _jwtService;
        private readonly IDuyguAnaliz _duyguAnalizService;

        public SikayetService(IUnitOfWork unitOfWork, IDuyguAnaliz duyguAnaliz, JwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _duyguAnalizService = duyguAnaliz;
        }

        public async Task<List<Sikayet>> GetAll()
        {
            // Burada await kullan, .Result veya .Wait() kullanma
            var all = await _unitOfWork.Repository<Sikayet>().GetAll();
            return all.ToList();
        }

        public async Task<Sikayet> GetById(int sikayetId)
        {
            return await _unitOfWork.Repository<Sikayet>().GetById(sikayetId);
        }

        public async Task<Sikayet> AddSikayet(Sikayet sikayet)
        {
            sikayet.DuyguPuani = _duyguAnalizService.HesaplaDuyguPuani(sikayet.Aciklama);
            await _unitOfWork.Repository<Sikayet>().Add(sikayet);
            await _unitOfWork.Commit();
            return sikayet;
        }

        public async Task<bool> UpdateDurumAsCozuldu(int sikayetId, int cozenBirimId)
        {
            var sikayet = await _unitOfWork.Repository<Sikayet>().GetById(sikayetId);
            if (sikayet == null) return false;

            sikayet.Durum = Domain.Enums.sikayetdurumu.Cozuldu;
            sikayet.CozulmeTarihi = DateTime.UtcNow;
            sikayet.CozenBirimId = cozenBirimId;

            await _unitOfWork.Repository<Sikayet>().Update(sikayet);
            await _unitOfWork.Commit();
            return true;
        }

        // SoftDelete yorum olarak bırakılmış, gerektiğinde açabilirsin
        //public async Task<bool> SoftDelete(int sikayetId)
        //{
        //    var sikayet = await _unitOfWork.Repository<Sikayet>().GetById(sikayetId);
        //    if (sikayet == null) return false;

        //    sikayet.Silindimi = true;

        //    await _unitOfWork.Repository<Sikayet>().Update(sikayet);
        //    await _unitOfWork.Commit();

        //    return true;
        //}

        public async Task<bool> IncrementDogrulama(int sikayetId)
        {
            var sikayet = await _unitOfWork.Repository<Sikayet>().GetById(sikayetId);
            if (sikayet == null) return false;

            sikayet.DogrulanmaSayisi++;
            await _unitOfWork.Repository<Sikayet>().Update(sikayet);
            await _unitOfWork.Commit();
            return true;
        }

        #region TotalSikayet
        public async Task<int> TotalSikayetSayisi()
        {
            var allComplaint = await _unitOfWork.Repository<Sikayet>().GetAll();
            return allComplaint.Count();
        }
        #endregion

        #region CozulenSikayetler
        public async Task<int> CozulenSikayetSayisi()
        {
            var sikayetler = await _unitOfWork.Repository<Sikayet>().GetAll();
            return sikayetler.Count(s => s.Durum == sikayetdurumu.Cozuldu);
        }
        #endregion

        #region BekleyenSikayetler
        public async Task<int> BekleyenSikayetSayisi()
        {
            var sikayetler = await _unitOfWork.Repository<Sikayet>().GetAll();
            return sikayetler.Count(s => s.Durum == sikayetdurumu.Inceleniyor);
        }
        #endregion
    }
}
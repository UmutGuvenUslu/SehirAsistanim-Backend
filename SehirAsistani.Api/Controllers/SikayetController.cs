using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SehirAsistanim.Domain.Interfaces;
using SehirAsistanim.Domain.Dto_s;

namespace SehirAsistani.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SikayetController : ControllerBase
    {
        private readonly ISikayetService _service;

        public SikayetController(ISikayetService service)
        {
            _service = service;
        }

        #region Tüm Şikayetleri Getir
        [HttpGet]
        public async Task<List<SikayetDetayDto>> GetAll()
        {
            try
            {
                return await _service.GetAll();
            }
            catch
            {
                return new List<SikayetDetayDto>();
            }
        }
        #endregion

        #region Id ile Şikayet Getir
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Sikayet> GetById(int id)
        {
            try
            {
                return await _service.GetById(id);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Şikayet Ekle
        [Authorize]
        [HttpPost]
        public async Task<Sikayet> Add([FromBody] Sikayet sikayet)
        {
            try
            {
                if (sikayet == null)
                    return null;

                return await _service.AddSikayet(sikayet);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Şikayet Güncelle (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<bool> Update([FromBody] Sikayet sikayet)
        {
            try
            {
                return await _service.UpdateSikayet(sikayet);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Durum Güncelle (Çözüldü Yap)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/{birimId}")]
        public async Task<bool> UpdateDurum(int id, int birimId)
        {
            try
            {
                return await _service.UpdateDurumAsCozuldu(id, birimId);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Doğrulama Sayısı Arttır
        [Authorize]
        [HttpPut("{id}")]
        public async Task<bool> IncrementDogrulama(int id)
        {
            try
            {
                return await _service.IncrementDogrulama(id);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region İstatistikler (Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<int> TotalSikayetSayisi()
        {
            try
            {
                return await _service.TotalSikayetSayisi();
            }
            catch
            {
                return 0;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<int> CozulenSikayetSayisi()
        {
            try
            {
                return await _service.CozulenSikayetSayisi();
            }
            catch
            {
                return 0;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<int> BekleyenSikayetSayisi()
        {
            try
            {
                return await _service.BekleyenSikayetSayisi();
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region Şikayet Sil (Admin)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _service.DeleteSikayet(id);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Kullanıcıya Ait Şikayetleri Getir
        [Authorize]
        [HttpGet]
        public async Task<List<SikayetDetayDto>> GetAllByUser([FromQuery] int userId)
        {
            try
            {
                if (userId <= 0)
                    return new List<SikayetDetayDto>();

                var complaints = await _service.GetAllByUser(userId);
                return complaints ?? new List<SikayetDetayDto>();
            }
            catch
            {
                return new List<SikayetDetayDto>();
            }
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;
using SehirAsistanim.Infrastructure.Services;

namespace SehirAsistani.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KullaniciController:ControllerBase
    {

        private readonly IKullaniciService _service;


        public KullaniciController(IKullaniciService service)
        {
            _service = service;
        }

        #region GetAll Kullanici
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<List<Kullanici>> GetAll()
        {
            try
            {
                return await _service.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAll hata: {ex.Message}");
                return new List<Kullanici>();
            }
        }
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public async Task<Kullanici> GetById(int id)
        {
            try
            {
                return await _service.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetById hata: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Add Kullanici
        [HttpPost]
        public async Task<Kullanici?> Add([FromBody] Kullanici kullanici)
        {
            try
            {
                return await _service.AddKullanici(kullanici);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add hata: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Update Kullanici
        [HttpPut]
        public async Task<Kullanici?> Update([FromBody] Kullanici kullanici)
        {
            try
            {
                return await _service.UpdateKullanici(kullanici);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update hata: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Delete Kullanici
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _service.DeleteKullanici(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete hata: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Total Kullanici Sayisi
        [HttpGet]
        public async Task<int> TotalKullaniciSayisi()
        {
            try
            {
                return await _service.TotalKullaniciSayisi();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TotalKullaniciSayisi hata: {ex.Message}");
                return 0;
            }
        }
        #endregion

    }
}

using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SehirAsistanim.Domain.Interfaces;

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

        [HttpGet]
        public async Task<List<Sikayet>> GetAll()
        {
            try
            {
                return await _service.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAll hata: {ex.Message}");
                return new List<Sikayet>();
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Sikayet> GetById(int id)
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
        [Authorize]
        [HttpPost]
        public async Task<Sikayet> Add([FromBody] Sikayet sikayet)
        {
            try
            {
                if (sikayet == null)
                {
                    Console.WriteLine("Add hata: Gönderilen şikayet bilgisi boş.");
                    return null;
                }

                // Kullanıcı kimliği ataması istersen buraya ekleyebilirsin:
                // var kullaniciIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                // if (kullaniciIdClaim != null)
                //     sikayet.KullaniciId = int.Parse(kullaniciIdClaim.Value);

                return await _service.AddSikayet(sikayet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add hata: {ex.Message}");
                return null;
            }
        }
        [Authorize]
        [HttpPut("{id}/{birimId}")]
        public async Task<bool> UpdateDurum(int id, int birimId)
        {
            try
            {
                return await _service.UpdateDurumAsCozuldu(id, birimId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateDurum hata: {ex.Message}");
                return false;
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<bool> IncrementDogrulama(int id)
        {
            try
            {
                return await _service.IncrementDogrulama(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IncrementDogrulama hata: {ex.Message}");
                return false;
            }
        }
    }
}

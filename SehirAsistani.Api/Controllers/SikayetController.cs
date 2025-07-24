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

        [HttpGet]
        public async Task<List<SikayetDetayDto>> GetAll()
        {
            try
            {
                return await _service.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAll hata: {ex.Message}");
                return new List<SikayetDetayDto>();
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
        [HttpPut]
        public async Task<bool> Update([FromBody] Sikayet sikayet)
        {
            try
            {
                return await _service.UpdateSikayet(sikayet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update hata: {ex.Message}");
                return false;
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

        [Authorize]
        [HttpGet]
        public async Task<int> TotalSikayetSayisi()
        {
            try
            {
                return await _service.TotalSikayetSayisi();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TotalSikayetSayisi hata: {ex.Message}");
                return 0;
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<int> CozulenSikayetSayisi()
        {
            try
            {
                return await _service.CozulenSikayetSayisi();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CozulenSikayetSayisi hata: {ex.Message}");
                return 0;
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<int> BekleyenSikayetSayisi()
        {
            try
            {
                return await _service.BekleyenSikayetSayisi();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BekleyenSikayetSayisi hata: {ex.Message}");
                return 0;
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _service.DeleteSikayet(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete hata: {ex.Message}");
                return false;
            }
        }
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
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllByUser hata: {ex.Message}");
                return new List<SikayetDetayDto>();
            }
        }


    }





}

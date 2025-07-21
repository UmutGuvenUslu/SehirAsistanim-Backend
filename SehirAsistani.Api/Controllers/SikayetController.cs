using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;
using System.Security.Claims;
using SehirAsistanim.Domain.Enums;

namespace SehirAsistani.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]//token olkamayn erişiemez
    public class SikayetController : ControllerBase
    {
        private readonly ISikayetService _sikayetService;

        public SikayetController(ISikayetService sikayetService)
        {
            _sikayetService = sikayetService;
        }

        [HttpGet]
        public List<Sikayet> GetAll()
        {
            try
            {
                var sikayetler =_sikayetService.GetAll().Result.ToList();
                return sikayetler;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{sikayetId}")]
        public async Task<IActionResult> GetById(int sikayetId)
        {
            try
            {
                var sikayet = await _sikayetService.GetById(sikayetId);
                if (sikayet == null)
                    return NotFound(new { message = "Şikayet bulunamadı." });

                return Ok(sikayet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddSikayet([FromBody] Sikayet model)
        {
            try
            {
                var kullaniciIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (kullaniciIdClaim == null)
                    return Unauthorized(new { message = "Kullanıcı doğrulanamadı." });

                model.KullaniciId = int.Parse(kullaniciIdClaim.Value);
                model.Durum = sikayetdurumu.Inceleniyor;

                var yeniSikayet = await _sikayetService.AddSikayet(model);
                return CreatedAtAction(nameof(GetById), new { sikayetId = yeniSikayet.Id }, yeniSikayet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
            }
        }

        [HttpPut("cozuldu/{sikayetId}")]
        public async Task<IActionResult> Cozuldu(int sikayetId, [FromQuery] int cozenBirimId)
        {
            try
            {
                var success = await _sikayetService.UpdateDurumAsCozuldu(sikayetId, cozenBirimId);
                if (!success)
                    return NotFound(new { message = "Şikayet bulunamadı." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
            }
        }
        [HttpPut("dogrula/{sikayetId}")]
        public async Task<IActionResult> IncrementDogrulama(int sikayetId)
        {
            try
            {
                var success = await _sikayetService.IncrementDogrulama(sikayetId);
                if (!success)
                    return NotFound(new { message = "Şikayet bulunamadı." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
            }
        }


    }
}


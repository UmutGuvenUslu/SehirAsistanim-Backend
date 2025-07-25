using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistani.Api.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class SikayetDogrulamaController:ControllerBase
    {
        private readonly ISikayetDogrulamaService _service;

        public SikayetDogrulamaController(ISikayetDogrulamaService service)
        {
            _service = service;
        }

        #region Doğrulama Sayısı Arttır
        [Authorize]
        [HttpPut]
        public async Task<bool> IncrementDogrulama(int sikayetId,int kullanciId)
        {
            try
            {
                return await _service.IncrementDogrulama(sikayetId,kullanciId);
            }
            catch
            {
                return false;
            }
        }
        #endregion


    }
}

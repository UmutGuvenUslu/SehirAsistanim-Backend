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


    }
}

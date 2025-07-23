using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Infrastructure.Services;

namespace SehirAsistani.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KullaniciController:ControllerBase
    {

        private readonly KullaniciService _services;


        public KullaniciController(KullaniciService services)
        {
            _services = services;
        }


        #region GetAll Kullanici
        [HttpGet]
        public List<Kullanici> GetAll()
        {
            var data = new List<Kullanici>();
            try
            {
                data = _services.GetAll();
                return data;
            }
            catch
            {
                return data;
            }
        }
        #endregion GetAll Kullanici


        #region GetById Kullanici

        #endregion GetById Kullanici

        #region Add Kullanici

        #endregion Add Kullanici

        #region Delete Kullanici

        #endregion Delete Kullanici

        #region Update Kullanici

        #endregion Update Kullanici

    }
}

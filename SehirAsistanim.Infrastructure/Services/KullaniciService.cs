using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistanim.Infrastructure.Services
{
    public class KullaniciService
    {
        public readonly IUnitOfWork _unitofWork;

        public KullaniciService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        #region GetAll
        public List<Kullanici> GetAll()
        {
            return _unitofWork.Repository<Kullanici>().GetAll().Result.ToList();

        }
        #endregion





    }
}

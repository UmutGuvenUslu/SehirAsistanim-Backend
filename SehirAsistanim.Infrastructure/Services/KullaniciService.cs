using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;
using SehirAsistanim.Infrastructure.UnitOfWork;

namespace SehirAsistanim.Infrastructure.Services
{
    public class KullaniciService : IKullaniciService
    {
        public readonly IUnitOfWork _unitofWork;

        public KullaniciService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        #region GetAll
        public async Task<List<Kullanici>> GetAll()
        {
            var result = await _unitofWork.Repository<Kullanici>().GetAll();
            return result.ToList(); 
        }
        #endregion

        #region GetById
        public async Task<Kullanici> GetById(int kullaniciId)
        {
            return await _unitofWork.Repository<Kullanici>().GetById(kullaniciId);
        }
        #endregion

        #region Add
        public async Task<Kullanici> AddKullanici(Kullanici kullanici)
        {
            await _unitofWork.Repository<Kullanici>().Add(kullanici);
            await _unitofWork.Commit();
            return kullanici;
        }
        #endregion

        #region Update
        public async Task<Kullanici> UpdateKullanici(Kullanici kullanici)
        {
            _unitofWork.Repository<Kullanici>().Update(kullanici);
            await _unitofWork.Commit();
            return kullanici;
        }
        #endregion

        #region HardDelete
        public async Task<bool> DeleteKullanici(int kullaniciId)
        {
            var kullanici = await _unitofWork.Repository<Kullanici>().GetById(kullaniciId);
            if (kullanici == null)
                return false;

            _unitofWork.Repository<Kullanici>().Delete(kullanici.Id);
            await _unitofWork.Commit();
            return true;
        }
        #endregion

        //#region SoftDelete
        //public async Task<bool> SoftDeleteKullanici(int kullaniciId)
        //{
        //    var kullanici = await _unitofWork.Repository<Kullanici>().GetById(kullaniciId);
        //    if (kullanici == null)
        //        return false;

        //    kullanici.SilindiMi = true;

        //    _unitofWork.Repository<Kullanici>().Update(kullanici);
        //    await _unitofWork.Commit();
        //    return true;
        //}
        //#endregion

        #region TotalKullaniciSayisi
        public async Task<int> TotalKullaniciSayisi()
        {
            var allUsers = await _unitofWork.Repository<Kullanici>().GetAll();
            return allUsers.Count();
        }
        #endregion




    }
}

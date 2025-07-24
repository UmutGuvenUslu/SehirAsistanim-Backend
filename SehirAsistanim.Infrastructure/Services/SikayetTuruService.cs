using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistanim.Infrastructure.Services
{
    public class SikayetTuruService : ISikayetTuruService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SikayetTuruService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetAll
        public async Task<List<SikayetTuru>> GetAll()
        {
            var result = await _unitOfWork.Repository<SikayetTuru>().GetAll();
            return result.ToList(); 
        }
        #endregion

        #region GetById
        public async Task<SikayetTuru> GetById(int sikayetTuruId)
        {
            return await _unitOfWork.Repository<SikayetTuru>().GetById(sikayetTuruId);
        }
        #endregion

        #region AddSikayetTuru
        public async Task<SikayetTuru> AddSikayetTuru(SikayetTuru sikayetTuru)
        {
            await _unitOfWork.Repository<SikayetTuru>().Add(sikayetTuru);
            await _unitOfWork.CommitAsync();
            return sikayetTuru;
        }
        #endregion

        #region UpdateSikayetTuru
        public async Task<SikayetTuru> UpdateSikayetTuru(SikayetTuru sikayetTuru)
        {
            _unitOfWork.Repository<SikayetTuru>().Update(sikayetTuru);
            await _unitOfWork.CommitAsync();
            return sikayetTuru;
        }
        #endregion

        #region DeleteSikayetTuru
        public async Task<bool> DeleteSikayetTuru(int sikayetTuruId)
        {
            // Önce silinecek türü getir
            var sikayetTuru = await _unitOfWork.Repository<SikayetTuru>().GetById(sikayetTuruId);
            if (sikayetTuru == null)
                return false;

            // Bu türe bağlı şikayetleri getir
            var sikayetler =  _unitOfWork.Repository<Sikayet>()
                .GetAll().Result.Where(q=>q.SikayetTuruId == sikayetTuruId);

            // Bağlantılı şikayetleri sil
            foreach (var sikayet in sikayetler)
            {
                _unitOfWork.Repository<Sikayet>().Delete(sikayet.Id);
            }

            // Şikayet türünü sil
            _unitOfWork.Repository<SikayetTuru>().Delete(sikayetTuru.Id);

            // Tüm değişiklikleri kaydet
            await _unitOfWork.CommitAsync();

            return true;
        }
        #endregion

        #region SoftDelete
        //public async Task<bool> SoftDeleteSikayetTuru(int sikayetTuruId)
        //{
        //    var sikayetTuru = await _unitOfWork.Repository<SikayetTuru>().GetById(sikayetTuruId);
        //    if (sikayetTuru == null)
        //        return false;

        //    sikayetTuru.SilindiMi = true;

        //    _unitOfWork.Repository<SikayetTuru>().Update(sikayetTuru.Id);
        //    await _unitOfWork.Commit();
        //    return true;
        //}
        #endregion
    }
}


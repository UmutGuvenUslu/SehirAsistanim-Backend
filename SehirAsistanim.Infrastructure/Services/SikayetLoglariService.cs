using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;

namespace SehirAsistanim.Infrastructure.Services
{
    public class SikayetLoglariService:ISikayetLoglariService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SikayetLoglariService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LogAsync(SikayetLog sikayetLog)
        {

            await _unitOfWork.Repository<SikayetLog>().Add(sikayetLog);
            await _unitOfWork.CommitAsync();
        }
    }
}

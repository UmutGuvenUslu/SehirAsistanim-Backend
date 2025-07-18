using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Interfaces;
using SehirAsistanim.Infrastructure.Repositories;

namespace SehirAsistanim.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SehirAsistaniDbContext _context;

        public UnitOfWork(SehirAsistaniDbContext context)
        {
            _context = context;
        }

        #region RepositoryCreate
        public IGenericRepository<T> Repository<T>() where T : class, IEntitiy
        {
            return new GenericRepository<T>(_context);
        }

        #endregion


        #region Commit
        public async Task<int> Commit()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                throw new Exception(errorMessage);
            }
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }

}

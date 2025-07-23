namespace SehirAsistanim.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class, IEntitiy;

        ValueTask DisposeAsync();
    }
}

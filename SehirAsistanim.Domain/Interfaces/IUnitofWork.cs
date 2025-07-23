namespace SehirAsistanim.Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class, IEntitiy;

        ValueTask DisposeAsync();
    }
}

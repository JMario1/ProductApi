namespace productMgtApi.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
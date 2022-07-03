namespace AgroContainerTracker.Domain.Common
{
    public interface IRepository<TEntity> where
        TEntity : class, IAggregate
    {
    }
}

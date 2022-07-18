namespace AgroContainerTracker.Domain
{
    public interface IRepository<T>
        where T : class, IAggregate
    {
    }
}

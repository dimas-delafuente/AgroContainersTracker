namespace AgroContainerTracker.Domain
{
    public class Entity<T>
    {
        public virtual T Id { get; protected set; }
    }
}
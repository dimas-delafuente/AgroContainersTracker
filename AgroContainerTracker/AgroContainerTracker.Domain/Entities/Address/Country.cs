using AgroContainerTracker.Domain.Common;
namespace AgroContainerTracker.Domain.Entities
{
    public class Country : IAggregate
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}

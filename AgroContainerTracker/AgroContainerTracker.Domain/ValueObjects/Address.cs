using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain
{
    public class Address : ValueObject
    {
        public string Street { get; private set;} = null!;

        public string Locality { get; private set; } = null!;

        public string State { get; private set; } = null!;

        public string PostalCode { get; private set; } = null!;

        public Country Country { get; private set; } = null!;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return Locality;
            yield return State;
            yield return PostalCode;
            yield return Country;
        }
    }
}
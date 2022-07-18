using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain
{
    public sealed class Country : Enumeration
    {
        private static readonly IDictionary<int, Country> _countries =
            new Dictionary<int, Country>();

        public static readonly Country Spain = new(1, "España");

        private Country(int id, string name) : base(id, name)
        {
            _countries.Add(id, this);
        }

        public static IReadOnlyCollection<Country>? Values => _countries.Values as IReadOnlyCollection<Country>;
    }
}
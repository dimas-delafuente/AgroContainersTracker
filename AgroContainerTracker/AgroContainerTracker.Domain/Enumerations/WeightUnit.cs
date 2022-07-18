using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Domain
{
    public sealed class WeightUnit : Enumeration
    {
        private static readonly IDictionary<int, WeightUnit> _units =
            new Dictionary<int, WeightUnit>();

        public static readonly WeightUnit Kilograms = new(1, "kg");

        private WeightUnit(int id, string name) : base(id, name)
        {
            Ensure.Positive(id, nameof(id));
            Ensure.Argument.NotNullOrEmpty(name, nameof(name));

            _units.Add(id, this);
        }

        public static IReadOnlyCollection<WeightUnit>? Values => _units.Values as IReadOnlyCollection<WeightUnit>;
    }
}
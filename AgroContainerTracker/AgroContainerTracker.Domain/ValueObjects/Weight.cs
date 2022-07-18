using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Domain
{
    public class Weight : ValueObject
    {
        public Amount Amount { get; }
        public WeightUnit Unit { get; }

        public Weight(Amount amount, WeightUnit unit)
        {
            Ensure.Argument.NotNull(amount, nameof(amount));
            Ensure.Argument.NotNull(unit, nameof(unit));
            Amount = amount;
            Unit = unit;
        }

        public static Weight From(decimal amount, string code)
        {
            return new Weight(Amount.FromScalar(amount), Enumeration.FromDisplayName<WeightUnit>(code));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Unit;
        }
    }
}
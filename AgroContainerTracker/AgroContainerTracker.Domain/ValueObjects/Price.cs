using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Domain
{
    public class Price : ValueObject
    {
        public Amount Amount { get; }
        public Currency Currency { get; }

        public Price(Amount amount, Currency currency)
        {
            Ensure.Argument.NotNull(amount, nameof(amount));
            Ensure.Argument.NotNull(currency, nameof(currency));
            Amount = amount;
            Currency = currency;
        }

        public static Price From(decimal amount, string code)
        {
            return new Price(Amount.FromScalar(amount), Enumeration.FromDisplayName<Currency>(code));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
namespace AgroContainerTracker.Domain
{
    public class Amount : ValueObject
    {
        public decimal Value { get; private set; }

        public Amount(decimal value)
        {
            Value = value;
        }

        public static Amount FromScalar(decimal value)
        {
            return new Amount(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator decimal(Amount amount)
        {
            return amount.Value;
        }

        public static explicit operator Amount(decimal value)
        {
            return new Amount(value);
        }
    }
}
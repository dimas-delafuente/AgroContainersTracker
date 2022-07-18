using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Domain
{
    public class BillingInfo : ValueObject
    {
        public BillingInfo(string bankAccount)
        {
            Ensure.NotNull(bankAccount, nameof(bankAccount));
            BankAccount = bankAccount;
        }

        public string BankAccount { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BankAccount;
        }
    }
}
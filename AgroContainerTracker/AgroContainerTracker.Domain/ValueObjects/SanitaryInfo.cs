namespace AgroContainerTracker.Domain
{
    public class SanitaryInfo : ValueObject
    {
        public string RegistrationNumber { get; private set;} = null!;

        public SanitaryInfo(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return RegistrationNumber;
        }
    }
}
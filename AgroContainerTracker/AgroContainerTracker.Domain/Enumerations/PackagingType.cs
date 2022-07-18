using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Domain
{
    public sealed class PackagingType : Enumeration
    {
        private static readonly IDictionary<int, PackagingType> _packagings =
            new Dictionary<int, PackagingType>();

        public static readonly PackagingType Palot = new(1, "Palot");
        public static readonly PackagingType Palet = new(2, "Palet");
        public static readonly PackagingType Box = new(3, "Caja");

        private PackagingType(int id, string name) : base(id, name)
        {
            Ensure.Positive(id, nameof(id));
            Ensure.Argument.NotNullOrEmpty(name, nameof(name));

            _packagings.Add(id, this);
        }

        public static IReadOnlyCollection<PackagingType>? Values => _packagings.Values as IReadOnlyCollection<PackagingType>;
    }
}
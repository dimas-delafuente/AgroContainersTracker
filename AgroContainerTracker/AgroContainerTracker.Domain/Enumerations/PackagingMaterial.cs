using AgroContainerTracker.Shared;

namespace AgroContainerTracker.Domain
{
    public sealed class PackagingMaterial : Enumeration
    {
        private static readonly IDictionary<int, PackagingMaterial> _materials =
            new Dictionary<int, PackagingMaterial>();

        public static readonly PackagingMaterial Wood = new(1, "Madera");
        public static readonly PackagingMaterial Plastic = new(2, "Plástico");

        private PackagingMaterial(int id, string name) : base(id, name)
        {
            Ensure.Positive(id, nameof(id));
            Ensure.Argument.NotNullOrEmpty(name, nameof(name));

            _materials.Add(id, this);
        }

        public static IReadOnlyCollection<PackagingMaterial>? Values => _materials.Values as IReadOnlyCollection<PackagingMaterial>;
    }
}
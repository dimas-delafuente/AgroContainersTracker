using System;
namespace AgroContainerTracker.Domain
{
    public enum PackagingMaterial
    {
        Wood,
        Plastic
    }

    public enum PackaginType
    {
        Palot,
        Palet,
        Box
    }

    public class Packaging
    {
        public int PackagingId { get; set; }

        public string Code { get; set; }

        public PackagingMaterial Material { get; set; }

        public double Weight { get; set; }

        public string Description { get; set; }

        public int ClientId { get; set; }

        public string Color { get; set; }

        public PackaginType Type { get; set; }

        public bool Active { get; set; }

        public int Total { get; set; }
    }
}

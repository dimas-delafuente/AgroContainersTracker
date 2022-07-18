namespace AgroContainerTracker.Domain
{
    // TODO: Move to view model
    public class ProductRecordPackaging
    {
        public Packaging Packaging { get; set; } = null!;
        public bool IsOwnPackaging { get; set; }
        public int Quantity { get; set; }
        public double PackagingWeight { get; set; }
        public double TotalWeight { get; set; }
    }
}

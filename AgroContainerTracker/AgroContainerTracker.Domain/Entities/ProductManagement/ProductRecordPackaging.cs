﻿namespace AgroContainerTracker.Domain.Entities
{
    public class ProductRecordPackaging
    {
        public Packaging Packaging { get; set; }
        public bool IsOwnPackaging { get; set; }
        public int Quantity { get; set; }
        public double PackagingWeight { get; set; }
        public double TotalWeight { get; set; }
    }
}

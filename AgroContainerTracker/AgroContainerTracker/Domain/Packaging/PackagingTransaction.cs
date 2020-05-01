using System;
namespace AgroContainerTracker.Domain
{
    public enum Transaction
    {
        Add,
        Substract
    }

    public class PackagingTransaction
    {
        public int PackagingTransactionId { get; set; }

        public int PackageId { get; set; }

        public Transaction Transaction { get; set; }

        public int Amount { get; set; }

        public DateTime Created { get; set; }

        public int Total { get; set; }
    }
}

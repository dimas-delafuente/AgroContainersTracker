using AgroContainerTracker.Domain.Companies;
using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Domain.ProductManagement
{
    public class ProductEntry
    {
        public int ProductEntryNumber { get; set; }
        public int CampaingId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }


        public string Observations { get; set; }
        public bool HasHail { get; set; }
        public bool HasPlague { get; set; }
        public bool HasSecondPasses { get; set; }

        public virtual Customer Buyer { get; set; }
        public virtual Customer Payer { get; set; }
        public virtual IEnumerable<Customer> Sellers { get; set; }
    }
}

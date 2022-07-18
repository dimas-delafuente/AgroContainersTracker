using AgroContainerTracker.Domain.Companies;
using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Domain.ProductManagement
{
    public class Input
    {
        public int InputNumber { get; set; }
        public int CampaignId { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Closed { get; set; }
        public bool HasProductExit { get; set; }


        public string Observations { get; set; }
        public bool HasHail { get; set; }
        public bool HasPlague { get; set; }
        public bool HasSecondPasses { get; set; }

        public Customer Buyer { get; set; }
        public Customer Payer { get; set; }
        public IEnumerable<Customer> Sellers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using AgroContainerTracker.Domain.Companies;
using AgroContainerTracker.Domain.Fruits;
using AgroContainerTracker.Domain.Rates;

namespace AgroContainerTracker.Domain.ProductManagement
{
    public class ProductWeighing
    {
        public int ProductWeighingId { get; set; }

        public int CampaingId { get; set; }

        public int ProductEntryNumber { get; set; }

        public Fruit Fruit { get; set; }

        public ColdRoom ColdRoom { get; set; }

        public Rate Rate { get; set; }

        public Customer Seller { get; set; }

        public Customer Buyer { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public DateTime? ExitDate { get; set; }
        public int TotalLabels { get; set; }

        public List<ProductRecord> ProductRecords { get; set; }
    }

    public class AddProductWeighingRequest
    {
        public int ProductWeighingId { get; set; }

        public int CampaingId { get; set; }

        public int ProductEntryNumber { get; set; }

        public int FruitId { get; set; }

        public int ColdRoomId { get; set; }

        public int RateId { get; set; }

        public int SellerId { get; set; }

        public int BuyerId { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public List<ProductRecordPackaging> ProductRecords { get; set; }
    }
}

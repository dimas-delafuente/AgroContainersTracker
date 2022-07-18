using System;
using System.Collections.Generic;
using AgroContainerTracker.Domain.Companies;
using AgroContainerTracker.Domain.Fruits;
using AgroContainerTracker.Domain.Rates;

namespace AgroContainerTracker.Domain.ProductManagement
{
    public class Weighing
    {
        public int WeighingId { get; set; }

        public int CampaignId { get; set; }

        public int InputNumber { get; set; }
        public Input Input { get; set; }

        public Fruit Fruit { get; set; }

        public Storage Storage { get; set; }

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

    public class AddWeighingRequest
    {
        public int WeighingId { get; set; }

        public int CampaignId { get; set; }

        public int InputNumber { get; set; }

        public int FruitId { get; set; }

        public int StorageId { get; set; }

        public int RateId { get; set; }

        public int SellerId { get; set; }

        public int BuyerId { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public List<ProductRecordPackaging> ProductRecords { get; set; }
    }
}

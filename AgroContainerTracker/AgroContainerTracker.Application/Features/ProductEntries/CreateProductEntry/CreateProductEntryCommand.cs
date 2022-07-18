using System.Linq;
using System;
using System.Collections.Generic;
using AgroContainerTracker.Domain;
using MediatR;

namespace AgroContainerTracker.Application.Features
{
    public class CreateInputCommand : IRequest<Input>
    {
        public int CampaignId { get; set; }
        public int InputNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public string Observations { get; set; }
        public bool HasHail { get; set; }
        public bool HasPlague { get; set; }
        public bool HasSecondPasses { get; set; }
        public int BuyerId { get; set; }
        public int PayerId { get; set; }
        public IEnumerable<int> SellerIds { get; set; }
    }

    public static class CreateInputCommandExtensions
    {
        public static Input ToDomain(this CreateInputCommand command)
            => new Input
            {
                Buyer = new Customer() { Id = command.BuyerId },
                CampaignId = command.CampaignId,
                CreatedDate = command.EntryDate,
                HasHail = command.HasHail,
                HasPlague = command.HasPlague,
                HasSecondPasses = command.HasSecondPasses,
                Observations = command.Observations,
                Payer = new Customer() { Id = command.PayerId },
                Id = command.InputNumber,
                Sellers = command.SellerIds.Select(id => new Customer() { Id = id}).ToArray()
            };
    }
}
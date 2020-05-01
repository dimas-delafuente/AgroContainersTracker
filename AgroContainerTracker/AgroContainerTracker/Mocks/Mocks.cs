using System;
using System.Collections.Generic;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Mocks
{
    public static class LocalMocks
    {
        public static List<Container> Containers { get
            {
                return new List<Container>
                {
                    new Container {
                        ContainerId = 1,
                        Size = 100,
                        Palots = new List<Palot>
                        {
                            new Palot
                            {
                                ContainerId = 1,
                                PalotId = 1,
                                ArrivalNumber = "00001",
                                PalotCode = "P00001",
                                Arrival = new DateTime(),
                                Type = Palot.PalotType.Palot,
                                Weight = 200,
                                Fruit = new Fruit
                                {
                                    FruitId = 0,
                                    Name = "Manzana"
                                },
                                Buyer = new Customer
                                {
                                    CompanyId = 0,
                                    Name = "Haciendas Bio"
                                },
                                Seller = new Customer
                                {
                                    CompanyId = 1,
                                    Name = "Frutas Nene"
                                },
                                IsActive = true
                            }
                        }
                    },

                    new Container {
                        ContainerId = 2,
                        Size = 150,
                        Palots = new List<Palot>
                        {
                            new Palot
                            {
                                ContainerId = 2,
                                PalotId = 2,
                                ArrivalNumber = "00002",
                                PalotCode = "P00002",
                                Arrival = new DateTime(),
                                Type = Palot.PalotType.Palot,
                                Weight = 250,
                                Fruit = new Fruit
                                {
                                    FruitId = 1,
                                    Name = "Pera"
                                },
                                Buyer = new Customer
                                {
                                    CompanyId = 0,
                                    Name = "Haciendas Bio"
                                },
                                Seller = new Customer
                                {
                                    CompanyId = 1,
                                    Name = "Frutas Nene"
                                },
                                IsActive = true
                            }
                        }
                    }
                };
            }
        }

        public static List<Palot> Palots { get
            {
                return new List<Palot>
                {
                    new Palot
                        {
                            ContainerId = 1,
                            PalotId = 1,
                            ArrivalNumber = "00001",
                            PalotCode = "P00001",
                            Arrival = new DateTime(),
                            Type = Palot.PalotType.Palot,
                            Weight = 200,
                            Fruit = new Fruit
                            {
                                FruitId = 0,
                                Name = "Manzana"
                            },
                            Buyer = new Customer
                            {
                                CompanyId = 0,
                                Name = "Haciendas Bio"
                            },
                            Seller = new Customer
                            {
                                CompanyId = 1,
                                Name = "Frutas Nene"
                            },
                            IsActive = true
                        },
                    new Palot
                            {
                                ContainerId = 2,
                                PalotId = 2,
                                ArrivalNumber = "00002",
                                PalotCode = "P00002",
                                Arrival = new DateTime(),
                                Type = Palot.PalotType.Palot,
                                Weight = 250,
                                Fruit = new Fruit
                                {
                                    FruitId = 1,
                                    Name = "Pera"
                                },
                                Buyer = new Customer
                                {
                                    CompanyId = 0,
                                    Name = "Haciendas Bio"
                                },
                                Seller = new Customer
                                {
                                    CompanyId = 1,
                                    Name = "Frutas Nene"
                                },
                                IsActive = true
                            }
                };
            }
        }
    }
}

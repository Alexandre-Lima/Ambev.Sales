﻿using Ambev.Sales.Domain.Entities;
using Ambev.Sales.UnitTests.Utils;
using Bogus;

namespace Ambev.Sales.UnitTests.Builders
{
    public class CreateSalesRequestBuilder
    {
        private readonly CreateSalesRequest _instance;
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public CreateSalesRequestBuilder()
        {
            _instance = new CreateSalesRequest
            {
                BranchId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                SaleNumber = _faker.Random.String(20),
                SaleDate = DateTime.Now,
                TotalSale = _faker.Random.Decimal(),
                Items = new List<ItemSales>
                {
                    NewItem()
                }
            };
        }

        private static ItemSales NewItem()
        {
            return new ItemSales
            {
                ProductsId = Guid.NewGuid(),
                Quantities = _faker.Random.Decimal(),
                Discounts = _faker.Random.Decimal(),
                UnitValues = _faker.Random.Decimal(),
                TotalItem = _faker.Random.Decimal(),
                Cancelled = _faker.Random.Bool(),
            };
        }

        public CreateSalesRequest Build()
        {
            return _instance;
        }
    }
}

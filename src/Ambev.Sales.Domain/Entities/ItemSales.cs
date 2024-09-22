namespace Ambev.Sales.Domain.Entities
{
    public class ItemSales
    {
        public required Guid ProductsId { get; set; }
        public required decimal Quantities { get; set; }
        public required decimal UnitValues { get; set; }
        public required decimal Discounts { get; set; }
        public required decimal TotalItem { get; set; }
        public required bool Cancelled { get; set; }
    }
}

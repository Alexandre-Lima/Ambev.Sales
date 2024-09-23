namespace Ambev.Sales.Domain.Entities
{
    public class SalesItemRequest
    {
        public required Guid ProductsId { get; set; }
        public required bool Cancelled { get; set; }
    }
}

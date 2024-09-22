namespace Ambev.Sales.Domain.Entities
{
    public class SalesRequest
    {
        public required Guid BranchId { get; set; }
        public required Guid CustomerId { get; set; }
        public required string SaleNumber { get; set; }
        public required DateTime SaleDate { get; set; }        
        public decimal TotalSale { get; set; }
        public required IEnumerable<ItemSales> Items { get; set; }
    }
}

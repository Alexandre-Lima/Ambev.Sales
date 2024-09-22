namespace Ambev.Sales.Domain.Entities
{
    public class SalesStorageResponse
    {
        public required Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public required string Id { get; set; }
    }
}

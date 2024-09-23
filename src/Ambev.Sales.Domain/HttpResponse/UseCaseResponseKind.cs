namespace Ambev.Sales.Domain.HttpResponse
{
    public enum UseCaseResponseKind
    {
        Success = 200,
        Created = 201,
        NotFound = 404,
        InternalServerError = 500
    }
}

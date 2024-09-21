using Ambev.Sales.Domain.HttpResponse;
namespace Ambev.Sales.Domain.Shared
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute(TRequest request);
    }
}

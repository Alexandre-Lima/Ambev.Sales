using Ambev.Sales.Domain.HttpResponse;
using Microsoft.Extensions.Logging;

namespace Ambev.Sales.Domain.Shared
{
    public abstract class UseCase<TRequest, TResponse> : UseCaseBase<TResponse>, IUseCase<TRequest, TResponse>
    {
        protected UseCase(ILogger logger, string errorMessage)
            : base(logger, errorMessage)
        {

        }

        public async Task<UseCaseResponse<TResponse>> Execute(TRequest request)
        {
            return await SafeExecute(async () =>
            {
                return await OnExecute(request);
            });
        }

        protected abstract Task<UseCaseResponse<TResponse>> OnExecute(TRequest request);
    }
}

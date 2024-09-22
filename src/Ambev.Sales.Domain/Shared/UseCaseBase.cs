using Ambev.Sales.Domain.HttpResponse;
using Microsoft.Extensions.Logging;

namespace Ambev.Sales.Domain.Shared
{
    public abstract class UseCaseBase<TResponse>
    {
        protected ILogger Logger { get; }
        protected string ErrorMessage { get; set; }

        protected UseCaseBase(ILogger logger, string errorMessage)
        {
            Logger = logger;
            ErrorMessage = errorMessage;
        }

        protected async Task<UseCaseResponse<TResponse>> SafeExecute(Func<Task<UseCaseResponse<TResponse>>> action)
        {
            var useCaseName = this.GetType().Name;
            
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                var message = $"Erro ao executar o caso de uso {useCaseName}. {ErrorMessage} {ex.Message}";
                Logger.LogError(ex, "{ErrorMessage}", message);

                return InternalServerError(ErrorMessage);
            }
        }

        protected UseCaseResponse<TResponse> Result(TResponse result)
        {
            var response = new UseCaseResponse<TResponse>();
            return response.SetResult(result);
        }

        protected UseCaseResponse<TResponse> Created(TResponse result)
        {
            var response = new UseCaseResponse<TResponse>();
            return response.SetCreated(result);
        }

        protected UseCaseResponse<TResponse> InternalServerError(string errorMessage)
        {
            var response = new UseCaseResponse<TResponse>();
            return response.SetInternalServerError(errorMessage);
        }
    }
}

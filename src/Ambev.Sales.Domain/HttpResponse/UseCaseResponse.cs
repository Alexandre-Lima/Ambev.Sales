using Ambev.Sales.Domain.Shared;

namespace Ambev.Sales.Domain.HttpResponse
{
    public class UseCaseResponse<T> : IResponse
    {
        public UseCaseResponseKind Status { get; private set; }
        public string? ErrorMessage { get; private set; }
        public T? Result { get; private set; }

        public UseCaseResponse<T> SetCreated(T result)
        {
            Status = UseCaseResponseKind.Created;
            Result = result;
            ErrorMessage = null;
            return this;
        }

        public UseCaseResponse<T> SetResult(T result)
        {
            Status = UseCaseResponseKind.Success;
            Result = result;
            ErrorMessage = null;
            return this;
        }

        public UseCaseResponse<T> SetInternalServerError(string errorMessage)
        {
            Status = UseCaseResponseKind.InternalServerError;
            ErrorMessage = errorMessage;            
            return this;
        }

        public UseCaseResponse<T> SetNotFound(string errorMessage)
        {
            Status = UseCaseResponseKind.NotFound;
            ErrorMessage = errorMessage;
            return this;
        }
    }
}

using Grpc.Core;
using Grpc.Core.Interceptors;
using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Interceptors;

public sealed class ValidationInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        if (request is not IValidatableObject validatable)
        {
            return await continuation(request, context);
        }

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(validatable);

        if (!Validator.TryValidateObject(
                validatable,
                validationContext,
                validationResults,
                validateAllProperties: true))
        {
            string errorMessage = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
            throw new RpcException(new Status(StatusCode.InvalidArgument, errorMessage));
        }

        return await continuation(request, context);
    }
}
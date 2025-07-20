using FluentValidation;
using MediatR;
using Users.Apis.Shared.Exceptions;

namespace Users.Apis.Shared.Behaviours;
    public sealed class ExceptionPipelineBehavior<TRequest, TResponse>(
        ILogger<ExceptionPipelineBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error handling request of type {RequestType}", typeof(TRequest).Name);
                throw ConvertException(ex);
            }
        }

        private static Exception ConvertException(Exception ex)
        {
            return ex switch
            {
                UnauthorizedException => new ProblemDetailsException(
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    ex.Message),

                NotFoundException => new ProblemDetailsException(
                    StatusCodes.Status404NotFound,
                    "Not Found",
                    ex.Message),

                ValidationException validationEx => new ProblemDetailsException(
                    StatusCodes.Status400BadRequest,
                    "Validation Error",
                    "One or more validation errors occurred",
                    validationEx.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray())),

                _ => new ProblemDetailsException(
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error",
                    ex.Message)
            };
        }
    }

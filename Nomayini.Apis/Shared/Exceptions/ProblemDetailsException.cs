namespace Nomayini.Apis.Shared.Exceptions;

// Shared/Core/Exceptions/ProblemDetailsException.cs
public class ProblemDetailsException : Exception
    {
        public int StatusCode { get; }
        public string Title { get; }
        public object? Extensions { get; }

        public ProblemDetailsException(
            int statusCode,
            string title,
            string detail,
            object? extensions = null)
            : base(detail)
        {
            StatusCode = statusCode;
            Title = title;
            Extensions = extensions;
        }
    }

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileAvatar.Maping
{
    public class ApiKyMiddlewaer
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKyMiddlewaer> _logger;
        private const string API_KEY_HEADER_NAME = "X-API-KEY";
        private const string API_KEY = "your-api-key"; // Replace with your actual API key
        private const int MaxRequestPerMinute = 10;

        private static readonly Dictionary<string, (int Count, DateTime ResetTime)> RequestCounts = new();
        private static readonly object LockObject = new();

        public ApiKyMiddlewaer(RequestDelegate next, ILogger<ApiKyMiddlewaer> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            if (path != null && (path.StartsWith("/swagger") || path.StartsWith("/uploads")))
            {
                await _next(context);
                return;
            }

            if(!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var key) || key != API_KEY)
            {
                _logger.LogWarning("Tnvalid api key");
                await WriteProblemDeteilsAsync(context, HttpStatusCode.Unauthorized, "incalid apy key");
                return;
            }

            var clientId = key.ToString();
            int reminingRequest;
            lock (LockObject)
            {
                var now = DateTime.UtcNow;
                if (!RequestCounts.TryGetValue(clientId, out var clientData)
                    || now >= clientData.ResetTime)
                {
                    clientData = (0, now.AddMinutes(1));
                }
                if (clientData.Count >= MaxRequestPerMinute)
                {
                    _logger.LogWarning("Overload Limit");
                    WriteProblemDeteilsAsync(context, HttpStatusCode.TooManyRequests,
                    "Limit is empty, try after 1 minute");
                    return;
                }

                clientData.Count++;
                RequestCounts[clientId] = clientData;
                reminingRequest = MaxRequestPerMinute - clientData.Count;
            }

            context.Items["ClientAppId"] = clientId;
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Append("X-RateLimit-Limit", MaxRequestPerMinute.ToString());
                context.Response.Headers.Append("X-RateLimit-Remaining", reminingRequest.ToString());
                return Task.CompletedTask;
            });

            await _next(context);

        }

        private async Task WriteProblemDeteilsAsync(HttpContext context, HttpStatusCode statusCode, string detail)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = statusCode.ToString(),
                Detail = detail,
                Instance = context.Request.Path
            };
            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    
    }
    public static class ApiKeyMiddlewareExtentions
    {
        public static IApplicationBuilder UseApiKeyAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKyMiddlewaer>();
        }
    }

}
    
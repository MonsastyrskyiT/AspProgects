
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FileAvatar.Middleware
{
    public class BlockedUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BlockedUserMiddleware> _logger;

        private const string USER_ID_HEADER = "X-User-Id";

        private static readonly HashSet<Guid> BlockedUserIds = new()
        {
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Guid.Parse("22222222-2222-2222-2222-222222222222")
        };

        public BlockedUserMiddleware(
            RequestDelegate next,
            ILogger<BlockedUserMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(
                USER_ID_HEADER, out var userIdHeader))
            {
                if (Guid.TryParse(userIdHeader.ToString(), out var userId))
                {
                    if (BlockedUserIds.Contains(userId))
                    {
                        _logger.LogWarning(
                            "Blocked user: {UserId}", userId);

                        context.Response.StatusCode =
                            StatusCodes.Status403Forbidden;

                        context.Response.ContentType =
                            "application/problem+json";

                        var problem = new ProblemDetails
                        {
                            Status = 403,
                            Title = "User Blocked",
                            Detail = "Ваш акаунт заблоковано за порушення правил сервісу.",
                            Instance = context.Request.Path
                        };

                        var json = JsonSerializer.Serialize(
                            problem,
                            new JsonSerializerOptions(JsonSerializerDefaults.Web));

                        await context.Response.WriteAsync(json);
                        return;
                    }
                }
            }

            await _next(context);
        }
    }

    
    public static class BlockedUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseBlockedUser(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BlockedUserMiddleware>();
        }
    }
}

//(PhamNhatLam_0306231402_3:00 17/9/2025)
namespace BT1.Middleware 
{
    public class BlockIpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HashSet<string> _blocked;

        public BlockIpMiddleware(RequestDelegate next, IConfiguration cfg)
        {
            _next = next;

            var list = cfg.GetSection("AppConfig:Security:BlockedIPs").Get<string[]>()
                       ?? Array.Empty<string>();

            _blocked = list.Select(ip => ip.Trim())
                           .Where(ip => !string.IsNullOrWhiteSpace(ip))
                           .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrEmpty(remoteIp) && _blocked.Contains(remoteIp))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Your IP is blocked.");
                return;
            }

            await _next(context);
        }
    }
}

namespace BT1.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath = "request.log"; // file log sẽ được tạo trong thư mục chạy app

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy thông tin request
            var url = context.Request.Path;
            var ip = context.Connection.RemoteIpAddress?.ToString();

            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Định dạng nội dung log (có thể tùy chỉnh theo ý bạn)
            var logInfo = $"[{time}] - IP: {ip} - URL: {url}";

            // Ghi vào file (Append thêm mỗi request)
            await File.AppendAllTextAsync(_logFilePath, logInfo + Environment.NewLine);

            // Chuyển request sang middleware tiếp theo
            await _next(context);
        }
    }
}

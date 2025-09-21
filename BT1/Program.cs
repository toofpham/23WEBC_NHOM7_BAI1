using BT1;
using BT1.Middleware;
using Microsoft.AspNetCore.Http.Features;
using BT1.Middleware;
using BT1.Models;
using BT1.Services;

var builder = WebApplication.CreateBuilder(args);


// Ð?c MaxFileSizeMB t? appsettings.json b?ng IConfiguration(PhamNhatLam_0306231402_3:00 17/9/2025)
long maxMb = builder.Configuration.GetValue<long>("AppConfig:Upload:MaxFileSizeMB", 10);

// Áp gi?i h?n body multipart (upload) ? t?ng form – ch?n request quá l?n t? s?m (PhamNhatLam_0306231402_3:00 17/9/2025)
builder.Services.Configure<FormOptions>(o =>
{
    o.MultipartBodyLengthLimit = maxMb * 1024L * 1024L;
});

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware ch?n IP (PhamNhatLam_0306231402_3:00 17/9/2025)
app.UseMiddleware<BlockIpMiddleware>();


app.UseMiddleware<RequestLoggingMiddleware>();

// Middleware Load danh sách users vào DI (Tran Tuan Kiet_4:37 21/09/2025)
app.UseMiddleware<UserLoadingMiddleware>();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

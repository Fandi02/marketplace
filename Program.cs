using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using marketplace.Datas;
using marketplace.Interfaces;
using marketplace.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<marketplaceContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );

#region  Business Services Injection

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IKategoriService, KategoriService>();
builder.Services.AddScoped<IProdukService, ProdukService>();
builder.Services.AddScoped<IProdukKategoriService, ProdukKategoriService>();
builder.Services.AddScoped<IAlamatService, AlamatService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IKeranjangService, KeranjangService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStatusService, StatusService>();
// builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(360);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Home/Denied";
                options.LoginPath = "/AccountCustomer/Login";
            }
    );

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

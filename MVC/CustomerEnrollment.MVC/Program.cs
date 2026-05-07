using CustomerEnrollment.Core.Repositories;
using CustomerEnrollment.Core.Services;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews()
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll", policy =>
    {
        policy.WithOrigins("https://localhost:7200", "http://localhost:5200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("allowAll");

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Enroll}/{id?}");

app.Run();

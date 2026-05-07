using CustomerEnrollment.Core.Repositories;
using CustomerEnrollment.Core.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSwaggerGen();


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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowAll");

app.MapControllers();

app.Run();
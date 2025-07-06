using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services;
using Services.Services;
using System;
using WebAPI_FirmTaskProject;
using WebAPI_FirmTaskProject.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServiceDependencies();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.ApplyMigrations();

app.Run();

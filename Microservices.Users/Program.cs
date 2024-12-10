using MassTransit;
using Microservices.Core.Auth;
using Microservices.Core.Extensions;
using Microservices.Core.Middleware;
using Microservices.Users.Application.Services;
using Microservices.Users.Core.Services;
using Microservices.Users.Persistence;
using Microservices.Users.Supporting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuth("Users");

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<JwtSettings>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddKafka(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MigrateDatabase<DataContext>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
using Microservices.Core.Extensions;
using Microservices.Core.Middleware;
using Microservices.Languages.Application.Services;
using Microservices.Languages.Core.Services;
using Microservices.Languages.Persistence;
using Microservices.Languages.Supporting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddKafka(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddScoped<ILanguageService, LanguageService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MigrateDatabase<DataContext>();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

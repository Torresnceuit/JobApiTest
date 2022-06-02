using DataAccess;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.DependencyInjection;
using WebApi.Extensions;
#pragma warning disable CA1812 //https://github.com/dotnet/roslyn-analyzers/issues/5628

// Configure Services
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLamar();
builder.Host.ConfigureContainer<Lamar.ServiceRegistry>((_, services) =>
{
    services.IncludeRegistry<WebApiServiceRegistry>();
    services.AddControllers();
    services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }));
    services.AddDbContext<TechTestDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("fergus.techtest.sqlserver")));
});

// Configure Middleware
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

// Database Setup
app.ApplyDatabaseMigrations(redeployDatabase: app.Environment.IsDevelopment());

// Run application
app.Run();

using SkySoft.APIHost;
using SkySoft.IBPPApplication;
using SkySoft.Net.DnsServerConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ApplicationConfiguration.RegisterServices(builder);
builder.Services.AddTransient<IRequestController, RequestController>();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplicationConfiguration.InitializeApplication(app.Services);
app.Run();
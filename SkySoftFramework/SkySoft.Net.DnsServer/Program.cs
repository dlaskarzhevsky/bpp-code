using SkySoft.APIHost.DPL;
using SkySoft.IBPPApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IReceiverController, ReceiverController>();
SkySoft.DnsServer.CFG.APIHostInitializer.RegisterServices(builder);
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SkySoft.DnsServer.CFG.APIHostInitializer.InitializeApplication(app.Services);
app.Run();
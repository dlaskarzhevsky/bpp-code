using SkySoft.APIHost.DPL;
using SkySoft.IBPPApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SkySoft.BPPApplication.CFG.Configurator.RegisterServices(builder);
SkySoft.APIHost.CFG.Configurator.RegisterServices(builder);
SkySoft.DnsClient.CFG.Configurator.RegisterServices(builder);
BPP.Person.DALCFG.Configurator.RegisterServices(builder);

builder.Services.AddTransient<IReceiverController, ReceiverController>();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Start receiver controller
app.Services.GetRequiredService<IReceiverController>();

app.Run();

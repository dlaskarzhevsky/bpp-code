using SkySoft.APIHost.DPL;
using SkySoft.IBPPApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IReceiverController, ReceiverController>();
BPP.Person.DALCFG.APIHostInitializer.RegisterServices(builder);
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

BPP.Person.DALCFG.APIHostInitializer.InitializeApplication(app.Services);
app.Run();

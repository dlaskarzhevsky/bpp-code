using SkySoft.APIHost.DPL;
using SkySoft.IBPPApplication;
using BPP.Person.CFG;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IRequestController, RequestController>();
APIHostInitializer.RegisterServices(builder);
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

APIHostInitializer.InitializeApplication(app.Services);
app.Run();

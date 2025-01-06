using SkySoft.IBPPApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IRequestHandler, BPP.Person.DAL.SearchingRequestHandler>();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

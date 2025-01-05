using SkySoft.IBPPApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddTransient<IRequestHandler, BPP.Person.DPL.SearchingRequestHandler>();
//builder.Services.AddTransient<IRequestHandler, BPP.Person.DAL.SearchingRequestHandler>();
builder.Services.AddTransient<IRequestHandler, BPP.Person.BL.SearchingRequestHandler>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

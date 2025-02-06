WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.ApplicationServer.CFG.Registrar.Register(webApplicationBuilder);
BPP.Person.DPLCFG.Registrar.Register(webApplicationBuilder);
BPP.PersonApp.DPLCFG.Registrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddMemoryCache();
WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.ApplicationServer.Runtime.ApplicationStarter.Start(webApplication);

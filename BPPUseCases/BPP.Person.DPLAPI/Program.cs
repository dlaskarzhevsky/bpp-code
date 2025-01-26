WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.BPPApplication.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.APIHost.DPLCFG.Registrar.Register(webApplicationBuilder);
SkySoft.Http.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsClient.DPLCFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsClientApp.DPLCFG.Registrar.Register(webApplicationBuilder);
BPP.Person.DPLCFG.Registrar.Register(webApplicationBuilder);
BPP.PersonApp.DPLCFG.Registrar.Register(webApplicationBuilder);

SkySoft.APIHost.DPLCFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddMemoryCache();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.BPPApplication.Runtime.ApplicationStarter.Start(webApplication);

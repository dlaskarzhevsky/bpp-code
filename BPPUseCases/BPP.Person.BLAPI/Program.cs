WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.BPPApplication.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.Http.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsClient.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsClientApp.CFG.Registrar.Register(webApplicationBuilder);
BPP.Person.BLCFG.Registrar.Register(webApplicationBuilder);
BPP.PersonApp.BLCFG.Registrar.Register(webApplicationBuilder);

SkySoft.APIHost.CFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddMemoryCache();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.BPPApplication.Runtime.ApplicationStarter.Start(webApplication);

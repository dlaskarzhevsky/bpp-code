WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.BPPApplication.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.Http.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsServer.DALCFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsServer.DPLCFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsServerApp.DPLCFG.Registrar.Register(webApplicationBuilder);

SkySoft.APIHost.CFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddMemoryCache();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.BPPApplication.Runtime.ApplicationStarter.Start(webApplication);

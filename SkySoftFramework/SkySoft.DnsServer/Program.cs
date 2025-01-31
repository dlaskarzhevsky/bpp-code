WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.BPPApplication.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.APIHost.DPLCFG.Registrar.Register(webApplicationBuilder);
SkySoft.Http.CFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsServer.DALCFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsServer.DPLCFG.Registrar.Register(webApplicationBuilder);
SkySoft.DnsServerApp.DPLCFG.Registrar.Register(webApplicationBuilder);
SkySoft.Logging.CFG.Registrar.Register(webApplicationBuilder);

SkySoft.APIHost.DPLCFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.ApplicationServer.Runtime.ApplicationStarter.Start(webApplication);


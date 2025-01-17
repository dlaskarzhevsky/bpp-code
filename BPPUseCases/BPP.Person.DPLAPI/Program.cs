WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.BPPApplication.CFG.BPPApplicationComponentsRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.DriversRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.ReceiverControllerRegistrar.Register(webApplicationBuilder);
SkySoft.DnsClient.CFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.DnsClientApp.CFG.EventHandlersRegistrar.Register(webApplicationBuilder);
BPP.Person.DPLCFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
BPP.PersonApp.DPLCFG.EventHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddMemoryCache();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.BPPApplication.Runtime.ApplicationStarter.Start(webApplication);

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.BPPApplication.CFG.BPPApplicationComponentsRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.Http.CFG.DriversRegistrar.Register(webApplicationBuilder);
SkySoft.Http.CFG.ReceiverControllerRegistrar.Register(webApplicationBuilder);
SkySoft.DnsClient.CFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.DnsClientApp.CFG.EventHandlersRegistrar.Register(webApplicationBuilder);
BPP.Person.DALCFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddMemoryCache();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.BPPApplication.Runtime.ApplicationStarter.Start(webApplication);

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

SkySoft.BPPApplication.CFG.BPPApplicationComponentsRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.APIHost.CFG.ReceiverControllerRegistrar.Register(webApplicationBuilder);
SkySoft.DnsServer.CFG.RequestHandlersRegistrar.Register(webApplicationBuilder);
SkySoft.DnsServer.CFG.ApplicationInitializerRegistrar.Register(webApplicationBuilder);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddMemoryCache();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

SkySoft.BPPApplication.Runtime.ApplicationStarter.Start(webApplication);

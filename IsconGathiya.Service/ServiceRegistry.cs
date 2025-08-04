using IsconGathiya.Common;
using IsconGathiya.Common.Cache;
using IsconGathiya.Data;
using IsconGathiya.Service.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;


namespace IsconGathiya.Service
{
    public static class ServiceRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<ITempDataDictionary, TempDataDictionary>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IDapperService, DapperService>();
            services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

            // scan all dependency of solution
            services.AddDependencyScanning().ScanAssembly();
        }
    }
}
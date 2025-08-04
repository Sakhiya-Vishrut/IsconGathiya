using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyScanning(this IServiceCollection services)
        {
            services.AddSingleton<Scanner>();
            return services;
        }

        public static IServiceCollection ScanFromAssembly(this IServiceCollection services, AssemblyName assemblyName)
        {
            var scanner = services.GetScanner();
            scanner.RegisterAssembly(services, assemblyName);
            return services;
        }

        public static IServiceCollection ScanAssembly(this IServiceCollection services)
        {
            services.ScanFromAssembly(new AssemblyName("IsconGathiya.Common"));
            services.ScanFromAssembly(new AssemblyName("IsconGathiya.Service"));
            return services;
        }

        private static Scanner GetScanner(this IServiceCollection services)
        {
            var scanner = services.BuildServiceProvider().GetService<Scanner>();
            if (null == scanner)
            {
                throw new InvalidOperationException("Unable to resolve scanner. Did you forget to call services.AddDependencyScanning?");
            }
            return scanner;
        }
    }
}

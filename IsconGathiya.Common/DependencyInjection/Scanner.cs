using IsconGathiya.Common.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public class Scanner
    {
        public void RegisterAssembly(IServiceCollection services, AssemblyName assemblyName)
        {
            var assembly = AssemblyLoader(assemblyName);
            foreach (var type in assembly.DefinedTypes)
            {
                var dependencyAttributes = type.GetCustomAttributes<DependencyAttribute>();
                // each dependency can be registered as various types
                foreach (var dependencyAttribute in dependencyAttributes)
                {
                    var serviceDescriptor = dependencyAttribute.BuildServiceDescriptor(type);
                    services.Add(serviceDescriptor);
                }
            }
        }

        public static Assembly AssemblyLoader(AssemblyName assemblyName)
        {
            return Assembly.Load(assemblyName);
        }
    }
}

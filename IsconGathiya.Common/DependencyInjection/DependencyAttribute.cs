using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class DependencyAttribute : Attribute
    {
        public ServiceLifetime DependencyType { get; set; }

        public Type? ServiceType { get; set; }

        protected DependencyAttribute(ServiceLifetime dependencyType)
        {
            DependencyType = dependencyType;
        }

        public ServiceDescriptor BuildServiceDescriptor(TypeInfo type)
        {
            var serviceType = ServiceType ?? type.AsType();
            return new ServiceDescriptor(serviceType, type.AsType(), DependencyType);
        }
    }

    public class ScopedDependencyAttribute : DependencyAttribute
    {
        public ScopedDependencyAttribute()
            : base(ServiceLifetime.Scoped)
        { }
    }
    public class SingletonDependencyAttribute : DependencyAttribute
    {
        public SingletonDependencyAttribute()
            : base(ServiceLifetime.Singleton)
        { }
    }
    public class TransientDependencyAttribute : DependencyAttribute
    {
        public TransientDependencyAttribute()
            : base(ServiceLifetime.Scoped)
        { }
    }
}

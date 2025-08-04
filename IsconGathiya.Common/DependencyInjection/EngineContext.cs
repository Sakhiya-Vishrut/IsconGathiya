using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using IsconGathiya.Common;

namespace IsconGathiya.Common
{
    public static class EngineContext
    {
        static EngineContext()
        {
            if (StaticContext == null)
                StaticContext = new List<object>();
        }

        public static IList<object> StaticContext { get; set; }

        public static bool IsStaticContext { get; set; }

        public static void AddService(object item)
        {
            if (StaticContext == null)
                StaticContext = new List<object>();
            StaticContext.Insert(StaticContext != null ? StaticContext.Count : 0, item);
        }

        public static T GetService<T>()
        {
            var obj = StaticContext.First(d => d is T);
            return (T)obj;
        }

        public static object GetService(Type t)
        {
            return StaticContext.First(d => t.IsInstanceOfType(d));
        }

        public static T Resolve<T>()
        {
            return (T)HttpHelper.HttpContext.RequestServices.GetRequiredService(typeof(T));
        }

        public static object GetInstance(Type t)
        {
            return HttpHelper.HttpContext.RequestServices.GetRequiredService(t);
        }
    }
}

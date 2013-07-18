using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace Injection
{
    public static class Factory
    {
        public static object Get(string token)
        {
            return ApplicationContext.GetObject(token);
        }

        public static TType Get<TType>(string token) where TType : class
        {
            return (TType)ApplicationContext.GetObject(token);
        }

        public static TType Get<TType>(string token, object[] arguments) where TType : class
        {
            return (TType)ApplicationContext.GetObject(token, arguments);
        }

        public static bool IsDefined(string token)
        {
            return ApplicationContext.ContainsObjectDefinition(token);
        }

        public static string ApplicationContextName { get; set; }

        public static IApplicationContext ApplicationContext
        {
            get
            {
                return ContextRegistry.GetContext();
            }
        }
    }
}

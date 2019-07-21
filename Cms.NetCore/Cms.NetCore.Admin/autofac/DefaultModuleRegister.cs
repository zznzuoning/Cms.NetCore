using Autofac;
using Cms.NetCore.Admin.Filter;
using Cms.NetCore.Repository;
using Cms.NetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace Cms.NetCore.Admin.autofac
{
    public class DefaultModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ButtonRepository).Assembly)
                  .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Context"))
                  .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ButtionServices).Assembly)
                 .Where(t => t.Name.EndsWith("Services"))
                 .AsImplementedInterfaces();
            builder.RegisterType<OperationalLogFilterAttribute>();
        }

        public static Assembly GetAssembly(string assemblyName)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
            return assembly;
        }
    }
}

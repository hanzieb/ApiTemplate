using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core.Activators.Reflection;

namespace ApiTemplate.Web.Data.AutofacModules
{
    /// <summary>
    /// Finds constructors that match a finder function.
    /// </summary>
    public class InternalConstructorFinder : IConstructorFinder
    {
        readonly Func<Type, ConstructorInfo[]> _finder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConstructorFinder" /> class.
        /// </summary>
        /// <remarks>
        /// Default to selecting all public constructors.
        /// </remarks>
        public InternalConstructorFinder() : this(type =>
            type.GetTypeInfo().DeclaredConstructors.ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConstructorFinder" /> class.
        /// </summary>
        /// <param name="finder">The finder function.</param>
        public InternalConstructorFinder(Func<Type, ConstructorInfo[]> finder)
        {
            if (finder == null) throw new ArgumentNullException("finder");

            _finder = finder;
        }

        /// <summary>
        /// Finds suitable constructors on the target type.
        /// </summary>
        /// <param name="targetType">Type to search for constructors.</param>
        /// <returns>Suitable constructors.</returns>
        public ConstructorInfo[] FindConstructors(Type targetType)
        {
            return _finder(targetType);
        }
    }
    public class CommonModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            IList<Assembly> allassemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //due to the eager load nature of core in IIS, not all DLLS are loaded at startup time
            //so some are missed from the DI registrar unless we force load all of them here
            var dlls = Directory.GetFiles(path, "*.dll")
                .Select(x => new FileInfo(x))
                .Where(x => x.Name.Contains("APITemplate"))
                .Where(x => !x.Name.Contains("Views"))
                .ToArray();

            foreach (FileInfo dll in dlls)
            {
                var assy = Assembly.Load(Assembly.LoadFile(dll.FullName).FullName);
                allassemblies.Add(assy);
            }

            // register some common patterns in the application
            builder.RegisterAssemblyTypes(allassemblies.ToArray())
                .Where(t => 
                       t.Name.EndsWith("Repository")
                    || t.Name.EndsWith("Wrapper")
                    || t.Name.EndsWith("Service")
                    || t.Name.EndsWith("Manager")
                    || t.Name.EndsWith("Provider"))
                .AsImplementedInterfaces()
                .FindConstructorsWith(new InternalConstructorFinder())
                .InstancePerLifetimeScope();
        }
    }
}
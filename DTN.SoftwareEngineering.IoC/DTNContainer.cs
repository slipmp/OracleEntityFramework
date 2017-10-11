using System.Reflection;
using Autofac;
using DTN.SoftwareEngineering.Core;
using DTN.SoftwareEngineering.Data;
using DTN.SoftwareEngineering.Data.Interfaces;

namespace DTN.SoftwareEngineering.IoC
{
    // ReSharper disable once InconsistentNaming
    public class DTNContainer
    {
        public static IContainer Container { get; set; }

        public void RegisterDependencies(bool useEntityFramework)
        {
            //Where things start
            var builder = new ContainerBuilder();

            //Normal stratight forward registrations
            builder.RegisterType<CalculatorService>().As<ICalculatorService>();
            builder.RegisterType<TaxService>().As<ITaxService>();

            //Conditional registrations
            if (useEntityFramework)
                builder.RegisterType<Data.EntityFramework.ApplicationConfigurationRepository>()
                    .As<IApplicationConfigurationRepository>();
            else
                builder.RegisterType<Data.JSON.ApplicationConfigurationRepository>()
                    .As<IApplicationConfigurationRepository>();

            //Registrations by Assembly
            var address = System.IO.Directory.GetCurrentDirectory().TrimEnd('\\') +
                          "\\DTN.SoftwareEngineering.Core.Data.JSON.dll";
            var assembly = Assembly.LoadFile(address);

            builder.RegisterAssemblyTypes(assembly)
                //You can also specify a filter 
                //.Where(x => x.Name.EndsWith("Parser"))
                .AsImplementedInterfaces();

            //Single Instance Registrations
            builder.RegisterType<DTNGlobalSettings>().As<IDTNGlobalSettings>().InstancePerLifetimeScope();

            //Making the registrations available
            Container = builder.Build();
        }
    }
}

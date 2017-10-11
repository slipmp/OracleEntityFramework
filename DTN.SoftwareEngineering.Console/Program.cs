using System;
using Autofac;
using DTN.SoftwareEngineering.Core;
using DTN.SoftwareEngineering.Core.Data.JSON;
using DTN.SoftwareEngineering.Data;
using DTN.SoftwareEngineering.Data.EntityFramework;
using DTN.SoftwareEngineering.Data.Interfaces;
using DTN.SoftwareEngineering.Domain;
using DTN.SoftwareEngineering.IoC;

namespace DTN.SoftwareEngineering.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RegisterDependencies();

                PrintOntarioTaxes();
                
                ShowAssemblyRegistrations();

                GetTopApplicationConfiguration_NotUsingIoC();
                
                GetTopApplicationConfiguration_UsingIoC();

                ShowSingleInstance();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("An Exception was thrown: ");
                System.Console.WriteLine(e);
            }
            finally
            {
                System.Console.ReadKey();
            }
        }

        /// <summary>
        /// Since this is a Console Application, it must be executed at the beginning
        /// On a Web Application, it must be located at Global.asax
        /// </summary>
        private static void RegisterDependencies()
        {
            var dtnContainer = new DTNContainer();
            var dtnConfigurationParser = new DTNConfigurationParser();
            var useEntityFramework = dtnConfigurationParser.GetUseEntityFramework();

            dtnContainer.RegisterDependencies(useEntityFramework);
        }

        private static void PrintOntarioTaxes()
        {
            var taxService = DTNContainer.Container.Resolve<ITaxService>();

            var amount = 100;
            var totalTaxes = taxService.GetTotalTax(ProvinceEnum.Ontario, amount);

            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine(
                "Printing Taxes ");
            System.Console.ForegroundColor = ConsoleColor.White;

            System.Console.WriteLine(
                $"Total taxes in {ProvinceEnum.Ontario} on the amount {amount:C} is {totalTaxes:C}");
            System.Console.WriteLine();
        }

        private static void GetTopApplicationConfiguration_NotUsingIoC()
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(
                "Top 10 Application Configuration from Oracle Database Not Using IoC ");
            System.Console.ForegroundColor = ConsoleColor.White;

            //Where the Magic happens
            var app = new ApplicationConfigurationRepository();
            var result = app.GetList(10);

            foreach (var applicationConfiguration in result)
            {
                System.Console.WriteLine(applicationConfiguration);
            }
            System.Console.WriteLine();
        }

        private static void GetTopApplicationConfiguration_UsingIoC()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine(
                "Top 10 Application Configuration from Oracle Database using IoC ");
            System.Console.ForegroundColor = ConsoleColor.White;

            //Where the Magic happens
            using (var scope = DTNContainer.Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplicationConfigurationRepository>();

                var result = app.GetList(10);
                foreach (var applicationConfiguration in result)
                {
                    System.Console.WriteLine(applicationConfiguration);
                }
            }
            System.Console.WriteLine();
        }

        private static void ShowAssemblyRegistrations()
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(
                "Showing Assembly Registrations on Autofac");
            System.Console.ForegroundColor = ConsoleColor.White;

            var applicationConfigurationParser = DTNContainer.Container.Resolve<IApplicationConfigurationParser>();
            System.Console.WriteLine(applicationConfigurationParser.GetType().ToString());
            System.Console.WriteLine();
        }

        private static void ShowSingleInstance()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkCyan;
            System.Console.WriteLine(
                "Showing Singleton using Autofac");
            System.Console.ForegroundColor = ConsoleColor.White;

            var dtnGlobalSettings = DTNContainer.Container.Resolve<IDTNGlobalSettings>();
            dtnGlobalSettings.LenderCode = "DSJ";
            System.Console.WriteLine($"Lender code is: {dtnGlobalSettings.LenderCode}");
            dtnGlobalSettings.LenderCode = "TRI";

            //Pretend this is being called on a totally different class/method/object etc.
            var dtnGlobalSettings2 = DTNContainer.Container.Resolve<IDTNGlobalSettings>();
            System.Console.WriteLine($"Lender code is: {dtnGlobalSettings2.LenderCode} after updating the Singleton");

            System.Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTN.SoftwareEngineering.Data;

namespace DTN.SoftwareEngineering.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(
                    "Top 10 Application Configuration from Oracle Database using Entity Framework: ");
                System.Console.ForegroundColor = ConsoleColor.White;

                //Where the Magic happens
                var app = new ApplicationConfigurationRepository();
                var result = app.GetList(10);

                foreach (var applicationConfiguration in result)
                {
                    System.Console.WriteLine(applicationConfiguration);
                }
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
    }
}

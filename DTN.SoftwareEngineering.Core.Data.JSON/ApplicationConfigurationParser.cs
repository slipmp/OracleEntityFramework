using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTN.SoftwareEngineering.Domain;
using Newtonsoft.Json.Linq;

namespace DTN.SoftwareEngineering.Core.Data.JSON
{
    public interface IApplicationConfigurationParser
    {
        IList<ApplicationConfiguration> GetApplicationConfigurationList();
    }

    public class ApplicationConfigurationParser : IApplicationConfigurationParser
    {
        private const string FileName = "ApplicationConfigurationList.json";

        public IList<ApplicationConfiguration> GetApplicationConfigurationList()
        {
            var fileContent = System.IO.File.ReadAllText(FileName);

            var listOfApplication = JArray.Parse(fileContent);

            var result = new List<ApplicationConfiguration>();
            foreach (var applicationConfiguration in listOfApplication)
            {
                var item = new ApplicationConfiguration();
                item.Id = Convert.ToInt32(applicationConfiguration["Id"]);
                item.AssetCode = applicationConfiguration["AssetCode"].ToString();
                item.ConfigKey = applicationConfiguration["ConfigKey"].ToString();
                item.ConfigValue = applicationConfiguration["ConfigValue"].ToString();
                item.LenderCode = applicationConfiguration["LenderCode"].ToString();
                item.ProductCode = applicationConfiguration["ProductCode"].ToString();

                result.Add(item);
            }

            return result;
        }
    }
}

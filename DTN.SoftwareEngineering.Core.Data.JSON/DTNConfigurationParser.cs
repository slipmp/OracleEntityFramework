using System;
using Newtonsoft.Json.Linq;

namespace DTN.SoftwareEngineering.Core.Data.JSON
{
    // ReSharper disable once InconsistentNaming
    public interface IDTNConfigurationParser
    {
        bool GetUseEntityFramework();
    }

    // ReSharper disable once InconsistentNaming
    public class DTNConfigurationParser : IDTNConfigurationParser
    {
        public const string FileName = "DTNConfiguration.json";

        public bool GetUseEntityFramework()
        {
            var fileContent = System.IO.File.ReadAllText(FileName);

            var item = JObject.Parse(fileContent);
            var result = Convert.ToBoolean(item["UseEntityFramework"]);

            return result;
        }
    }
}

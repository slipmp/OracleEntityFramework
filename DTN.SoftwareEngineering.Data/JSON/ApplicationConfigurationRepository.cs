using System;
using System.Collections.Generic;
using System.Linq;
using DTN.SoftwareEngineering.Core.Data.JSON;
using DTN.SoftwareEngineering.Data.Interfaces;
using DTN.SoftwareEngineering.Domain;

namespace DTN.SoftwareEngineering.Data.JSON
{
    public class ApplicationConfigurationRepository : IApplicationConfigurationRepository
    {
        private readonly IApplicationConfigurationParser _applicationConfigurationParser;

        public ApplicationConfigurationRepository(IApplicationConfigurationParser applicationConfigurationParser)
        {
            _applicationConfigurationParser = applicationConfigurationParser;
        }

        public List<ApplicationConfiguration> GetList(int take)
        {
            var result = _applicationConfigurationParser.GetApplicationConfigurationList();

            return result.Take(take).ToList();
        }

        public ApplicationConfiguration GetTaxActivationDate()
        {
            var result = _applicationConfigurationParser.GetApplicationConfigurationList();

            var item = result
                .FirstOrDefault(x => x.ConfigKey == "PEINewTaxActivationDate");

            return item;
        }
    }
}

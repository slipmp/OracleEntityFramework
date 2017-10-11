using System.Collections.Generic;
using System.Linq;
using DTN.SoftwareEngineering.Data.Interfaces;
using DTN.SoftwareEngineering.Domain;

namespace DTN.SoftwareEngineering.Data.EntityFramework
{
    public class ApplicationConfigurationRepository : IApplicationConfigurationRepository
    {
        public List<Domain.ApplicationConfiguration> GetList(int take)
        {
            var context = new ApplicationContext();
            var query = context.ApplicationConfigurations.Take(take);
            var result = query.ToList();

            return result;
        }

        public ApplicationConfiguration GetTaxActivationDate()
        {
            var context = new ApplicationContext();
            var result = context.ApplicationConfigurations
                .FirstOrDefault(x => x.ConfigKey == "PEINewTaxActivationDate");

            return result;
        }
    }
}

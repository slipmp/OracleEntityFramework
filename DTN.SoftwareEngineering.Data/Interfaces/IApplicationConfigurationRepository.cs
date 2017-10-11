using System.Collections.Generic;
using DTN.SoftwareEngineering.Domain;

namespace DTN.SoftwareEngineering.Data.Interfaces
{
    public interface IApplicationConfigurationRepository
    {
        List<Domain.ApplicationConfiguration> GetList(int take);
        ApplicationConfiguration GetTaxActivationDate();
    }
}

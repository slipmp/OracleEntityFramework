using System;
using DTN.SoftwareEngineering.Data;
using DTN.SoftwareEngineering.Domain;

namespace DTN.SoftwareEngineering.Core
{
    public interface ITaxService
    {
        decimal GetTotalTax(ProvinceEnum province, decimal subTotal);
    }
    public class TaxService : ITaxService
    {
        private readonly IApplicationConfigurationRepository _applicationConfigurationRepository;

        public TaxService(IApplicationConfigurationRepository applicationConfigurationRepository)
        {
            _applicationConfigurationRepository = applicationConfigurationRepository;
        }

        public decimal GetTotalTax(ProvinceEnum province, decimal subTotal)
        {
            switch (province)
            {
                case ProvinceEnum.Alberta:
                    return subTotal * (decimal)0.10;

                case ProvinceEnum.BritishColumbia:
                    return subTotal * (decimal)0.11;

                case ProvinceEnum.Ontario:
                    return subTotal * GetOntarioTax();
            }

            throw new NotImplementedException($"Province {province} is not implemented");
        }

        private decimal GetOntarioTax()
        {
            const decimal defaultOntarioTaxes = (decimal) 0.13;
            const decimal ontarioTaxesAfter2017 = (decimal) 0.15;

            var applicationConfiguration = _applicationConfigurationRepository.GetTaxActivationDate();

            if (string.IsNullOrEmpty(applicationConfiguration?.ConfigValue))
                return defaultOntarioTaxes;

            DateTime activationDate;
            DateTime.TryParse(applicationConfiguration.ConfigValue, out activationDate);

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (activationDate > DateTime.Now)
                return ontarioTaxesAfter2017;

            return defaultOntarioTaxes;
        }
    }
}

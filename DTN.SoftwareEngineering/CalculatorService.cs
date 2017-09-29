using DTN.SoftwareEngineering.Domain;

namespace DTN.SoftwareEngineering.Core
{
    public interface ICalculatorService
    {
        decimal GetTotalValue(Application application);
        decimal GetTotalValue(Application application, bool isDealerTrackEmployee);
    }
    public class CalculatorService: ICalculatorService
    {
        private readonly ITaxService _taxService;
        public CalculatorService(ITaxService taxService)
        {
            _taxService = taxService;
        }

        public decimal GetTotalValue(Application application)
        {
            return GetTotalValue(application, false);
        }

        public decimal GetTotalValue(Application application, bool isDealerTrackEmployee)
        {
            var subTotal = application.NumberOfInstallments * application.InstallmentPrice;

            if(isDealerTrackEmployee)
            {
                var totalDiscount = subTotal * (decimal)0.05;
                subTotal = subTotal - totalDiscount;
            }
            var totalTaxes = _taxService.GetTotalTax(application.Province, subTotal);

            var totalValue = subTotal + totalTaxes;

            return totalValue;
        }
    }
}

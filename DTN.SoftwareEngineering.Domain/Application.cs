using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTN.SoftwareEngineering.Domain
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public string LenderId { get; set; }
        public int VehicleId { get; set; }
        public ProvinceEnum Province { get; set; }
        public decimal Taxes { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentPrice { get; set; }
        public decimal TotalValue { get; set; }
    }
}

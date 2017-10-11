using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTN.SoftwareEngineering.Data
{
    // ReSharper disable once InconsistentNaming
    public interface IDTNGlobalSettings
    {
        string LenderCode { get; set; }
    }

    // ReSharper disable once InconsistentNaming
    public class DTNGlobalSettings : IDTNGlobalSettings
    {
        public string LenderCode { get; set; }
    }
}

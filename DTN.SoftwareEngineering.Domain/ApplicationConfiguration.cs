namespace DTN.SoftwareEngineering.Domain
{
    public class ApplicationConfiguration
    {
        public int Id { get; set; }
        public string LenderCode { get; set; }
        public string AssetCode { get; set; }
        public string ProductCode { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(LenderCode)}: {LenderCode}, {nameof(AssetCode)}: {AssetCode}, " +
                   $"{nameof(ProductCode)}: {ProductCode}, {nameof(ConfigKey)}: {ConfigKey}, " +
                   $"{nameof(ConfigValue)}: {ConfigValue}";
        }
    }
}

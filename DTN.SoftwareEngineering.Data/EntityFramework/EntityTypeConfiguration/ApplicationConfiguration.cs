using System.Data.Entity.ModelConfiguration;

namespace DTN.SoftwareEngineering.Data.EntityFramework.EntityTypeConfiguration
{
    /// <summary>
    /// This is where we can Set up the Entity "ApplicationConfiguration" in order to make EF understand it
    /// </summary>
    internal class ApplicationConfiguration: EntityTypeConfiguration<Domain.ApplicationConfiguration>
    {
        public ApplicationConfiguration()
        {
            /*
                It is important to notice here, that every configuration is capitalized here.
                The reason is because how Entity Framework creates its SQL Scripts.

                Even though by default, Oracle identifiers (table names, column names, etc.) are case-insensitive. 
                You can make them case-sensitive by using quotes around them 
                (eg: SELECT * FROM "My_Table" WHERE "my_field" = 1). 
                
                It seems that EF may be using quotes. If you don't specify this, you may face an exception like:
                {"ORA-00904: \"c\".\"AssetCode\": invalid identifier"}
            */
            ToTable("APPLICATIONCONFIGURATION");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("ID");
            Property(x => x.LenderCode).HasColumnName("LENDERCODE");
            Property(x => x.AssetCode).HasColumnName("ASSETCODE");
            Property(x => x.ProductCode).HasColumnName("PRODUCTCODE");
            Property(x => x.ConfigKey).HasColumnName("CONFIGKEY");
            Property(x => x.ConfigValue).HasColumnName("CONFIGVALUE");
        }
    }
}

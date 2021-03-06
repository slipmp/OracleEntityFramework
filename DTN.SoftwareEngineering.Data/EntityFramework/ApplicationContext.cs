﻿using System.Data.Entity;
using ApplicationConfiguration = DTN.SoftwareEngineering.Data.EntityFramework.EntityTypeConfiguration.ApplicationConfiguration;

namespace DTN.SoftwareEngineering.Data.EntityFramework
{
    /// <summary>
    /// This class must be internal, because it should not be available on the UI projects.
    /// Everything should be exposed through Repositories
    /// </summary>
    internal class ApplicationContext: DbContext
    {
        public IDbSet<Domain.ApplicationConfiguration> ApplicationConfigurations { get; set; }

        public ApplicationContext() : base("OracleDbContext")
        {
            //In order to disable the Entity Framework to Create tables automatically
            Database.SetInitializer<ApplicationContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DTCN");
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

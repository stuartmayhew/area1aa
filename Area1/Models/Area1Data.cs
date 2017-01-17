using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Area1.Models
{
    public class Area1Data:DbContext
    {
        public Area1Data()
            : base("Name=Dist23Data")
        {
            Database.SetInitializer<Area1Data>(null);
        }

        public DbSet<Contacts> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Login> Login { get; set; }
    }
}
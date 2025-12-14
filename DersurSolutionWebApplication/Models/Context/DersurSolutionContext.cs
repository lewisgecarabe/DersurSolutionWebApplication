using DersurSolutionWebApplication.Models.Maps;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DersurSolutionContext : DbContext
    {
        static DersurSolutionContext()
        {
            Database.SetInitializer<DersurSolutionContext>(null);
        }

        public DersurSolutionContext() : base("Name=dersursolutiondb") { }

        public virtual DbSet<tbl_contact_message_model> tbl_contact_message { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tbl_contact_message_map());
        }
    }
}
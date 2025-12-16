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
        public virtual DbSet<tbl_project_model> tbl_project { get; set; }
        public virtual DbSet<tbl_project_stat_model> tbl_project_stat { get; set; }
        public virtual DbSet<tbl_project_testimonial_model> tbl_project_testimonial { get; set; }
        public virtual DbSet<tbl_service_model> tbl_service { get; set; }
        public virtual DbSet<tbl_admin_user_model> tbl_admin_user { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tbl_contact_message_map());
            modelBuilder.Configurations.Add(new tbl_project_map());
            modelBuilder.Configurations.Add(new tbl_project_stat_map());
            modelBuilder.Configurations.Add(new tbl_project_testimonial_map());
            modelBuilder.Configurations.Add(new tbl_service_map());
            modelBuilder.Configurations.Add(new tbl_admin_user_map());

        }
    }
}
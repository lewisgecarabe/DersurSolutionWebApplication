using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models.Maps
{
    public class tbl_project_map : EntityTypeConfiguration<tbl_project_model>
    {
        public tbl_project_map()
        {
            HasKey(x => x.ProjectID);
            ToTable("tbl_project");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models.Maps
{
    public class tbl_admin_user_map : EntityTypeConfiguration<tbl_admin_user_model>
    {
        public tbl_admin_user_map()
        {
            ToTable("tbl_admin_user");
            HasKey(x => x.AdminID);
        }
    }
}
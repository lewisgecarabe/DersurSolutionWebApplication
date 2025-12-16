using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models.Maps
{
    public class tbl_service_map : EntityTypeConfiguration<tbl_service_model>
    {
        public tbl_service_map()
        {
            HasKey(x => x.ServiceID);
            ToTable("tbl_service");
        }
    }

}
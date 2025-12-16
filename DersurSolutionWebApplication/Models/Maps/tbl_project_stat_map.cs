using System.Data.Entity.ModelConfiguration;
using DersurSolutionWebApplication.Models;

namespace DersurSolutionWebApplication.Models.Maps
{
    public class tbl_project_stat_map
        : EntityTypeConfiguration<tbl_project_stat_model>
    {
        public tbl_project_stat_map()
        {
            // Table
            ToTable("tbl_project_stat");

            // Primary Key
            HasKey(x => x.ProjectStatID);

        }
    }
}

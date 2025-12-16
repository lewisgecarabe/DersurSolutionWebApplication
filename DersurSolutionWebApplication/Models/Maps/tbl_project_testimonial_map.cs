using System.Data.Entity.ModelConfiguration;
using DersurSolutionWebApplication.Models;

namespace DersurSolutionWebApplication.Models.Maps
{
    public class tbl_project_testimonial_map
        : EntityTypeConfiguration<tbl_project_testimonial_model>
    {
        public tbl_project_testimonial_map()
        {
            // Table
            ToTable("tbl_project_testimonial");

            // Primary Key
            HasKey(x => x.TestimonialID);

            
        }
    }
}

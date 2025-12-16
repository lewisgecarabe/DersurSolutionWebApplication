using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models
{
    public class tbl_project_testimonial_model
    {
        public int TestimonialID { get; set; }

        public int ProjectID { get; set; }   // ID-to-ID only

        public string ClientName { get; set; }

        public string ClientRole { get; set; }

        public string TestimonialText { get; set; }

        public string ClientImage { get; set; }

        public DateTime CreatedAt { get; set; }

        public int IsArchived { get; set; }
    }
}
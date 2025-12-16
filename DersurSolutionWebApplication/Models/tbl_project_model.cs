using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models
{
    public class tbl_project_model
    {
        public int ProjectID { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectDescription { get; set; }

        public string ProjectImage { get; set; }

        public string ProjectLink { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int IsArchived { get; set; }
    }
}
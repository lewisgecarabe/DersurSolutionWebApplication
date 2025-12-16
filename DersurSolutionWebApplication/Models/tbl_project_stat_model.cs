using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models
{
    public class tbl_project_stat_model
    {
        public int ProjectStatID { get; set; }

        public int ProjectID { get; set; }   // ID-to-ID only

        public string StatLabel { get; set; }

        public string StatValue { get; set; }
    }
}
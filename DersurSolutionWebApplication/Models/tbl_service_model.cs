using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models
{
    public class tbl_service_model
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal ServicePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int IsArchived { get; set; }
    }
}
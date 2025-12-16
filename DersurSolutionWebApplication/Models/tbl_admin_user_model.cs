using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models
{
    public class tbl_admin_user_model
    {
        public int AdminID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IsActive { get; set; }
    }
}
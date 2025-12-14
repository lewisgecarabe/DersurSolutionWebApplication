using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DersurSolutionWebApplication.Models.Maps
{
    public class tbl_contact_message_map : EntityTypeConfiguration<tbl_contact_message_model>
    {
        public tbl_contact_message_map() {
            HasKey(i => i.ContactID);
            ToTable("tbl_contact_message");
                }
    }
}
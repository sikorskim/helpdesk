using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class helpdeskContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public helpdeskContext() : base("name=helpdeskContext")
        {
        }

        public System.Data.Entity.DbSet<helpdesk.Models.Credential> Credentials { get; set; }
        public System.Data.Entity.DbSet<helpdesk.Models.Category> Categories { get; set; }
        public System.Data.Entity.DbSet<helpdesk.Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<helpdesk.Models.Status> Status { get; set; }
        public System.Data.Entity.DbSet<helpdesk.Models.OrderComment> OrderComment { get; set; }
        public System.Data.Entity.DbSet<helpdesk.Models.AppUser> AppUser { get; set; }
    }
}

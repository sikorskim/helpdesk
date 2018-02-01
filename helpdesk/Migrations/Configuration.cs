namespace helpdesk.Migrations
{
    using helpdesk.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<helpdesk.Models.helpdeskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(helpdesk.Models.helpdeskContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Categories.AddOrUpdate(
  c => c.CategoryName,
  new Category { CategoryName = "El-dok" },
  new Category { CategoryName = "QNT" },
  new Category { CategoryName = "PlanB" },
  new Category { CategoryName = "JST+" },
  new Category { CategoryName = "Bestia" },
  new Category { CategoryName = "GeoInfo" },
  new Category { CategoryName = "Microsoft Office" },
  new Category { CategoryName = "Microsoft Windows" },
  new Category { CategoryName = "Sprzt" });

            context.Status.AddOrUpdate(
s => s.StatusName,
new Status { StatusName = "nowe" },
new Status { StatusName = "realizowane" },
new Status { StatusName = "zamknite" },
new Status { StatusName = "anulowane" }
);
            context.Credentials.AddOrUpdate(
s => s.Name,
new Credential { Name = "administrator" },
new Credential { Name = "uytkownik" }
);

            Credential credential = context.Credentials.Single(p => p.Name == "administrator");
            context.AppUser.AddOrUpdate(
s => s.Username,
new AppUser { Username = @"sp-268\and", Credential = credential },
new AppUser { Username = @"sp-228\and", Credential = credential },
new AppUser { Username = @"spsrem\admin", Credential = credential }
);


        }
    }
}

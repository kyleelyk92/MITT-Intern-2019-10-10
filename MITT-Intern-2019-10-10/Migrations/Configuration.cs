namespace MITT_Intern_2019_10_10.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MITT_Intern_2019_10_10.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MITT_Intern_2019_10_10.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MITT_Intern_2019_10_10.Models.ApplicationDbContext context)
        {
            var db = context;
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Users.Any(u => u.UserName == "KyleE"))
            {
                if (!System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Launch();

                var store = new UserStore<ApplicationUser>(context);
                var usermanager = new ApplicationUserManager(store);

                var kyle = new Student() { UserName = "KyleE", Email = "kyleelyk92@hotmail.com", FirstName = "Kyle", LastName = "Elyk" };
                usermanager.Create(kyle, "Password1!");
                usermanager.Create(new Student() { UserName = "JulienM", Email = "jm@hotmail.com", FirstName = "Julien", LastName = "Martel" }, "Password1!");
                usermanager.Create(new Student() { UserName = "Farcas1235", Email = "fac@hmail.com", FirstName = "Farc", LastName = "As" }, "Password1!");
                usermanager.Create(new Student() { UserName = "Student4", Email = "Student4@email.com", FirstName = "Studfour", LastName = "FourLastName" }, "Password1!");

                var company2 = new Company() { UserName = "GQMag", Email = "gq@gq.com" };
                var company3 = new Company() { UserName = "BoldContentBS", Email = "BOLD@bold.com" };

                var company1 = new Company() { UserName = "Company1Name", Email = "firstEmail@Company.com" };

                var thisoneworks = usermanager.Create(new Company() { UserName = "Companymadename", Email = "CompanyEmail" }, "Password1!");

                var thisonedoesnt = usermanager.Create(company2, "Password1!");

                usermanager.Create(company3, "Password1!");
                usermanager.Create(company1, "Password1!");
                

                db.Programs.AddOrUpdate(x => x.Title,
                new SchoolProgram() { Title = "Software Developer" },
                new SchoolProgram() { Title = "Culinary" },
                new SchoolProgram() { Title = "Automotive" }
                );
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);

                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Student" });
                roleManager.Create(new IdentityRole { Name = "Company" });
                roleManager.Create(new IdentityRole { Name = "Teacher" });
            }
            db.SaveChanges();
        }
    }
}

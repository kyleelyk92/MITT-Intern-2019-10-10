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
            var kyle = new Student() { UserName = "KyleE", Email = "kyleelyk92@hotmail.com", FirstName = "Kyle", LastName = "Elyk" };
            var company1 = new Company() { UserName = "Company1Name", Email = "firstEmail@Company.com"};
            db.Students.AddOrUpdate(
                x => x.UserName,
                kyle,
                new Student() { UserName = "JulienM", Email = "jm@hotmail.com", FirstName = "Julien", LastName = "Martel" },
                new Student() { UserName = "Farcas1235", Email = "fac@hmail.com", FirstName = "Farc", LastName = "As" }
                ); ;
            db.Companies.AddOrUpdate(x => x.CompanyName,
                new Company() { UserName = "GQ Mag", Email = "gq@gq.com" },
                new Company() { UserName = "Bold Content BS", Email = "BOLD@bold.com" }
                );
            db.Programs.AddOrUpdate(x => x.Title,
                new SchoolProgram() { Title = "Software Developer"},
                new SchoolProgram() { Title = "Culinary" },
                new SchoolProgram() { Title = "Automotive" }
                );
            kyle.SchoolProgram = new SchoolProgram() { Title = "Kyles special program" };

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                manager.Create(new IdentityRole { Name = "Admin" });
                manager.Create(new IdentityRole { Name = "Student" });
                manager.Create(new IdentityRole { Name = "Company" });
                manager.Create(new IdentityRole { Name = "Teacher" });
            }
            db.SaveChanges();
        }
    }
}

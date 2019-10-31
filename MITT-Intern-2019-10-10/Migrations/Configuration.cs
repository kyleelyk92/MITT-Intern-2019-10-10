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

            var softwareDev = new SchoolProgram() { Title = "Software Developer" };

            var js = new Skill() { Name = "Javascript" };
            var react = new Skill() { Name = "React.js" };
            var html5 = new Skill() { Name = "HTML5" };
            var sass = new Skill() { Name = "SASS" };
            var ajax = new Skill() { Name = "AJAX" };

            db.Skills.AddOrUpdate(s => s.Name,
                new Skill() { Name = "CSS3" },
                new Skill() { Name = "Git" },
                new Skill() { Name = "ASP.NET C#" },
                new Skill() { Name = "MVC" },
                new Skill() { Name = "Unit Testing" },
                new Skill() { Name = "Bash" },
                new Skill() { Name = "Linux" },
                new Skill() { Name = "Node.js" },
                new Skill() { Name = "Front-End" },
                new Skill() { Name = "Back-End" },
                new Skill() { Name = "SQL" },
                new Skill() { Name = "Python" },
                new Skill() { Name = "DevOps" }
                );

            if (!context.Users.Any(u => u.UserName == "KyleE"))
            {
                var store = new UserStore<ApplicationUser>(db);
                var usermanager = new ApplicationUserManager(store);
                
                
                var kyle = new Student() { UserName = "KyleE", Email = "kyleelyk92@hotmail.com", FirstName = "Kyle", LastName = "Elyk" };
                usermanager.Create(kyle, "Password1!");
                usermanager.Create(new Student() { UserName = "JulienM", Email = "jm@hotmail.com", FirstName = "Julien", LastName = "Martel" }, "Password1!");
                usermanager.Create(new Student() { UserName = "Farcas1235", Email = "fac@hmail.com", FirstName = "Farc", LastName = "As" }, "Password1!");
                usermanager.Create(new Student() { UserName = "Student4", Email = "Student4@email.com", FirstName = "Studfour", LastName = "FourLastName" }, "Password1!");

                var company1 = new Company() { UserName = "Company1Name", Email = "firstEmail@Company.com" };

                var thisoneworks = usermanager.Create(new Company() { UserName = "Companymadename", Email = "CompanyEmail" }, "Password1!");

                usermanager.Create(company1, "Password1!");
                usermanager.Create(new Company() { UserName = "GQMag", Email = "gq@gq.com" }, "Password1!");

                var bold = new Company() { UserName = "BoldContentBS", Email = "BOLD@bold.com" };

                usermanager.Create(bold, "Password1!");

                var post = new Posting()
                {
                    Title = "Front-end Javasript dev wanted",
                    PostingDate = DateTime.Now,
                    ClosingDate = DateTime.Now.AddDays(60),
                    Content = "we are looking for a javascriot dev that likes to work in fron-end. Hopefully they will also like react. Looking for a stduent ike blah bah bha bkha bha bkab blah blah blahbh sdjfojsdufs jsdfuisdf i foims oi osd do you know dflksdf kf pofdnf",
                    SchoolProgram = softwareDev,
                };

                post.Skills.Add(js);
                post.Skills.Add(react);
                post.Skills.Add(html5);
                post.Skills.Add(sass);
                post.Skills.Add(ajax);

                bold.Postings.Add(post);

                
            }
            db.Programs.AddOrUpdate(x => x.Title,
                new SchoolProgram() { Title = "Culinary" },
                new SchoolProgram() { Title = "Automotive" }
                );

            

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

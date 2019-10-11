namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        CompanyName = c.String(),
                        Location = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        SchoolProgram_Id = c.Int(),
                        Program_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolPrograms", t => t.SchoolProgram_Id)
                .ForeignKey("dbo.SchoolPrograms", t => t.Program_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.SchoolProgram_Id)
                .Index(t => t.Program_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Postings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostingDate = c.DateTime(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                        SchoolProgram_Id = c.Int(),
                        Company_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolPrograms", t => t.SchoolProgram_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Company_Id)
                .Index(t => t.SchoolProgram_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.SchoolPrograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassRoom = c.String(),
                        Location = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateSent = c.DateTime(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        filepath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TeacherStudents",
                c => new
                    {
                        Teacher_Id = c.String(nullable: false, maxLength: 128),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.Student_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.Teacher_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Postings", "Company_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Postings", "SchoolProgram_Id", "dbo.SchoolPrograms");
            DropForeignKey("dbo.TeacherStudents", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherStudents", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Program_Id", "dbo.SchoolPrograms");
            DropForeignKey("dbo.AspNetUsers", "SchoolProgram_Id", "dbo.SchoolPrograms");
            DropIndex("dbo.TeacherStudents", new[] { "Student_Id" });
            DropIndex("dbo.TeacherStudents", new[] { "Teacher_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Postings", new[] { "Company_Id" });
            DropIndex("dbo.Postings", new[] { "SchoolProgram_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Program_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "SchoolProgram_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.TeacherStudents");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Resumes");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.SchoolPrograms");
            DropTable("dbo.Postings");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}

namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seewhatsgoingon : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Postings", "Student_Id", "dbo.AspNetUsers");
            //DropForeignKey("dbo.Skills", "Posting_Id", "dbo.Postings");
            DropIndex("dbo.Postings", new[] { "Student_Id" });
            //DropIndex("dbo.Skills", new[] { "Posting_Id" });
            
            //RenameIndex(table: "dbo.Postings", name: "IX_Company_Id", newName: "IX_CompanyId");
            //CreateTable(
            //    "dbo.StudentPostings",
            //    c => new
            //        {
            //            Student_Id = c.String(nullable: false, maxLength: 128),
            //            Posting_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Student_Id, t.Posting_Id })
            //    .ForeignKey("dbo.AspNetUsers", t => t.Student_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Postings", t => t.Posting_Id, cascadeDelete: true)
            //    .Index(t => t.Student_Id)
            //    .Index(t => t.Posting_Id);
            
            //CreateTable(
            //    "dbo.SkillPostings",
            //    c => new
            //        {
            //            Skill_Id = c.Int(nullable: false),
            //            Posting_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Skill_Id, t.Posting_Id })
            //    .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Postings", t => t.Posting_Id, cascadeDelete: true)
            //    .Index(t => t.Skill_Id)
            //    .Index(t => t.Posting_Id);
            
            DropColumn("dbo.Postings", "Student_Id");
            //DropColumn("dbo.Skills", "Posting_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Posting_Id", c => c.Int());
            AddColumn("dbo.Postings", "Student_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.SkillPostings", "Posting_Id", "dbo.Postings");
            DropForeignKey("dbo.SkillPostings", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.StudentPostings", "Posting_Id", "dbo.Postings");
            DropForeignKey("dbo.StudentPostings", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SkillPostings", new[] { "Posting_Id" });
            DropIndex("dbo.SkillPostings", new[] { "Skill_Id" });
            DropIndex("dbo.StudentPostings", new[] { "Posting_Id" });
            DropIndex("dbo.StudentPostings", new[] { "Student_Id" });
            DropTable("dbo.SkillPostings");
            DropTable("dbo.StudentPostings");
            RenameIndex(table: "dbo.Postings", name: "IX_CompanyId", newName: "IX_Company_Id");
            RenameColumn(table: "dbo.Postings", name: "CompanyId", newName: "Company_Id");
            CreateIndex("dbo.Skills", "Posting_Id");
            CreateIndex("dbo.Postings", "Student_Id");
            AddForeignKey("dbo.Skills", "Posting_Id", "dbo.Postings", "Id");
            AddForeignKey("dbo.Postings", "Student_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seewat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentPostings",
                c => new
                    {
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        Posting_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Posting_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Postings", t => t.Posting_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Posting_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentPostings", "Posting_Id", "dbo.Postings");
            DropForeignKey("dbo.StudentPostings", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.StudentPostings", new[] { "Posting_Id" });
            DropIndex("dbo.StudentPostings", new[] { "Student_Id" });
            DropTable("dbo.StudentPostings");
        }
    }
}

namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedSomeStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Postings", "Student_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Postings", "Student_Id");
            AddForeignKey("dbo.Postings", "Student_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Postings", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Postings", new[] { "Student_Id" });
            DropColumn("dbo.Postings", "Student_Id");
        }
    }
}

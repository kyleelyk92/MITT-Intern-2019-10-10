namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movedSkillsFromCompanyToPosting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "Posting_Id", c => c.Int());
            CreateIndex("dbo.Skills", "Posting_Id");
            AddForeignKey("dbo.Skills", "Posting_Id", "dbo.Postings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "Posting_Id", "dbo.Postings");
            DropIndex("dbo.Skills", new[] { "Posting_Id" });
            DropColumn("dbo.Skills", "Posting_Id");
        }
    }
}

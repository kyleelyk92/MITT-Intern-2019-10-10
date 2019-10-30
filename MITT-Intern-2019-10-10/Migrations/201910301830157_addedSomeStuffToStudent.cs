namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSomeStuffToStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HasResume", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "ResumeLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ResumeLink");
            DropColumn("dbo.AspNetUsers", "HasResume");
        }
    }
}

namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStuffToCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BannerImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BannerImagePath");
        }
    }
}

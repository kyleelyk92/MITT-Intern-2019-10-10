namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNavigatorToCompanyFromPosting : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Postings", name: "Company_Id", newName: "CompanyId");
            RenameIndex(table: "dbo.Postings", name: "IX_Company_Id", newName: "IX_CompanyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Postings", name: "IX_CompanyId", newName: "IX_Company_Id");
            RenameColumn(table: "dbo.Postings", name: "CompanyId", newName: "Company_Id");
        }
    }
}

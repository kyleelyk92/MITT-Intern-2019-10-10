namespace MITT_Intern_2019_10_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial11111 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "Posting_Id", "dbo.Postings");
            DropIndex("dbo.Skills", new[] { "Posting_Id" });
            CreateTable(
                "dbo.SkillPostings",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Posting_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Posting_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Postings", t => t.Posting_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Posting_Id);
            
            DropColumn("dbo.Skills", "Posting_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Posting_Id", c => c.Int());
            DropForeignKey("dbo.SkillPostings", "Posting_Id", "dbo.Postings");
            DropForeignKey("dbo.SkillPostings", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillPostings", new[] { "Posting_Id" });
            DropIndex("dbo.SkillPostings", new[] { "Skill_Id" });
            DropTable("dbo.SkillPostings");
            CreateIndex("dbo.Skills", "Posting_Id");
            AddForeignKey("dbo.Skills", "Posting_Id", "dbo.Postings", "Id");
        }
    }
}

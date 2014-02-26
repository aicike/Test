namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.User", "UserTagID", c => c.Int());
            CreateIndex("dbo.User", "UserTagID");
            AddForeignKey("dbo.User", "UserTagID", "dbo.UserTag", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserTagID", "dbo.UserTag");
            DropIndex("dbo.User", new[] { "UserTagID" });
            DropColumn("dbo.User", "UserTagID");
            DropTable("dbo.UserTag");
        }
    }
}

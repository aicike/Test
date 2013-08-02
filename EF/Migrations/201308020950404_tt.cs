namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUpdate",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.ID)
                .Index(t => t.ID);
            
            AddColumn("dbo.AccountMain", "AppUpdateID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.AppUpdate", new[] { "ID" });
            DropForeignKey("dbo.AppUpdate", "ID", "dbo.AccountMain");
            DropColumn("dbo.AccountMain", "AppUpdateID");
            DropTable("dbo.AppUpdate");
        }
    }
}

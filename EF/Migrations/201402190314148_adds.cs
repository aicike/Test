namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppAdvertorialOperation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AppAdvertorialID = c.Int(nullable: false),
                        EnumAdvertorialUType = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ForwardCnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppAdvertorial", t => t.AppAdvertorialID)
                .Index(t => t.AppAdvertorialID);
            
            AlterColumn("dbo.ActivityInfo", "ActivityStratDate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppAdvertorialOperation", "AppAdvertorialID", "dbo.AppAdvertorial");
            DropIndex("dbo.AppAdvertorialOperation", new[] { "AppAdvertorialID" });
            AlterColumn("dbo.ActivityInfo", "ActivityStratDate", c => c.DateTime(nullable: false));
            DropTable("dbo.AppAdvertorialOperation");
        }
    }
}

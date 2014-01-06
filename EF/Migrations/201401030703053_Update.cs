namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Remarks = c.String(nullable: false, maxLength: 500),
                        AccountID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ActivityInfoParticipator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        ActivityInfoID = c.Int(nullable: false),
                        EnumAdvertorialUType = c.Int(),
                        UserID = c.Int(),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Name = c.String(maxLength: 30),
                        JoinDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityInfo", t => t.ActivityInfoID)
                .Index(t => t.ActivityInfoID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ActivityInfoParticipator", new[] { "ActivityInfoID" });
            DropIndex("dbo.ActivityInfo", new[] { "AccountID" });
            DropIndex("dbo.ActivityInfo", new[] { "AccountMainID" });
            DropForeignKey("dbo.ActivityInfoParticipator", "ActivityInfoID", "dbo.ActivityInfo");
            DropForeignKey("dbo.ActivityInfo", "AccountID", "dbo.Account");
            DropForeignKey("dbo.ActivityInfo", "AccountMainID", "dbo.AccountMain");
            DropTable("dbo.ActivityInfoParticipator");
            DropTable("dbo.ActivityInfo");
        }
    }
}

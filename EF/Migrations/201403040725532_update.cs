namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutoMessage_Reply", "AccountMainID", "dbo.AccountMain");
            DropIndex("dbo.AutoMessage_Reply", new[] { "AccountMainID" });
            CreateTable(
                "dbo.ActivityInfoSignIn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        ActivityInfoID = c.Int(nullable: false),
                        EnumAdvertorialUType = c.Int(),
                        UserID = c.Int(),
                        Phone = c.String(maxLength: 15),
                        Name = c.String(maxLength: 30),
                        Email = c.String(maxLength: 30),
                        Company = c.String(maxLength: 50),
                        Position = c.String(maxLength: 30),
                        JoinDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityInfo", t => t.ActivityInfoID)
                .Index(t => t.ActivityInfoID);
            
            AddColumn("dbo.AutoMessage_Reply", "AccountID", c => c.Int(nullable: false));
            CreateIndex("dbo.AutoMessage_Reply", "AccountID");
            AddForeignKey("dbo.AutoMessage_Reply", "AccountID", "dbo.Account", "ID");
            DropColumn("dbo.AutoMessage_Reply", "AccountMainID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutoMessage_Reply", "AccountMainID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AutoMessage_Reply", "AccountID", "dbo.Account");
            DropForeignKey("dbo.ActivityInfoSignIn", "ActivityInfoID", "dbo.ActivityInfo");
            DropIndex("dbo.AutoMessage_Reply", new[] { "AccountID" });
            DropIndex("dbo.ActivityInfoSignIn", new[] { "ActivityInfoID" });
            DropColumn("dbo.AutoMessage_Reply", "AccountID");
            DropTable("dbo.ActivityInfoSignIn");
            CreateIndex("dbo.AutoMessage_Reply", "AccountMainID");
            AddForeignKey("dbo.AutoMessage_Reply", "AccountMainID", "dbo.AccountMain", "ID");
        }
    }
}

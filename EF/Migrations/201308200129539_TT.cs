namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PushMsg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Text = c.String(),
                        EnumMessageType = c.Int(nullable: false),
                        LibraryID = c.Int(),
                        PushTime = c.DateTime(nullable: false),
                        EnumPushType = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.PushMsgDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        PushMsgID = c.Int(nullable: false),
                        EnumClientUserType = c.Int(nullable: false),
                        ReceiveID = c.Int(nullable: false),
                        ReceiveTime = c.DateTime(),
                        EnumReceiveStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PushMsg", t => t.PushMsgID)
                .Index(t => t.PushMsgID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PushMsgDetail", new[] { "PushMsgID" });
            DropIndex("dbo.PushMsg", new[] { "AccountID" });
            DropIndex("dbo.PushMsg", new[] { "AccountMainID" });
            DropForeignKey("dbo.PushMsgDetail", "PushMsgID", "dbo.PushMsg");
            DropForeignKey("dbo.PushMsg", "AccountID", "dbo.Account");
            DropForeignKey("dbo.PushMsg", "AccountMainID", "dbo.AccountMain");
            DropTable("dbo.PushMsgDetail");
            DropTable("dbo.PushMsg");
        }
    }
}

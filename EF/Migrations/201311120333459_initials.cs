namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initials : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageGroupChat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        MessageID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                        SID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Message", t => t.MessageID)
                .Index(t => t.AccountMainID)
                .Index(t => t.MessageID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MessageGroupChat", new[] { "MessageID" });
            DropIndex("dbo.MessageGroupChat", new[] { "AccountMainID" });
            DropForeignKey("dbo.MessageGroupChat", "MessageID", "dbo.Message");
            DropForeignKey("dbo.MessageGroupChat", "AccountMainID", "dbo.AccountMain");
            DropTable("dbo.MessageGroupChat");
        }
    }
}

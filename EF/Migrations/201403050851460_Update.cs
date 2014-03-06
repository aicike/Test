namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutoMessage_Reply", "AccountMainID", "dbo.AccountMain");
            DropIndex("dbo.AutoMessage_Reply", new[] { "AccountMainID" });
            AddColumn("dbo.AutoMessage_Reply", "AccountID", c => c.Int(nullable: false));
            AlterColumn("dbo.ClientInfo", "ClientID", c => c.String(maxLength: 100));
            CreateIndex("dbo.AutoMessage_Reply", "AccountID");
            AddForeignKey("dbo.AutoMessage_Reply", "AccountID", "dbo.Account", "ID");
            DropColumn("dbo.AutoMessage_Reply", "AccountMainID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutoMessage_Reply", "AccountMainID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AutoMessage_Reply", "AccountID", "dbo.Account");
            DropIndex("dbo.AutoMessage_Reply", new[] { "AccountID" });
            AlterColumn("dbo.ClientInfo", "ClientID", c => c.String(maxLength: 50));
            DropColumn("dbo.AutoMessage_Reply", "AccountID");
            CreateIndex("dbo.AutoMessage_Reply", "AccountMainID");
            AddForeignKey("dbo.AutoMessage_Reply", "AccountMainID", "dbo.AccountMain", "ID");
        }
    }
}

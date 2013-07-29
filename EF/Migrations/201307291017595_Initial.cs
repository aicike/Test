namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID1", "dbo.AutoMessage_Keyword");
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AutoMessage_Keyword_ID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AutoMessage_Keyword_ID1" });
            DropColumn("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID");
            DropColumn("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID1", c => c.Int());
            AddColumn("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID", c => c.Int());
            CreateIndex("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID1");
            CreateIndex("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID");
            AddForeignKey("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID1", "dbo.AutoMessage_Keyword", "ID");
            AddForeignKey("dbo.AutoMessage_Keyword", "AutoMessage_Keyword_ID", "dbo.AutoMessage_Keyword", "ID");
        }
    }
}

namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TT : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Keyword", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.KeywordAutoMessage", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.TextReply", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropIndex("dbo.Keyword", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.TextReply", new[] { "AutoMessage_KeywordID" });
            AddForeignKey("dbo.Keyword", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword", "ID", cascadeDelete: true);
            AddForeignKey("dbo.KeywordAutoMessage", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TextReply", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword", "ID", cascadeDelete: true);
            CreateIndex("dbo.Keyword", "AutoMessage_KeywordID");
            CreateIndex("dbo.KeywordAutoMessage", "AutoMessage_KeywordID");
            CreateIndex("dbo.TextReply", "AutoMessage_KeywordID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TextReply", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.Keyword", new[] { "AutoMessage_KeywordID" });
            DropForeignKey("dbo.TextReply", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.KeywordAutoMessage", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.Keyword", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            CreateIndex("dbo.TextReply", "AutoMessage_KeywordID");
            CreateIndex("dbo.KeywordAutoMessage", "AutoMessage_KeywordID");
            CreateIndex("dbo.Keyword", "AutoMessage_KeywordID");
            AddForeignKey("dbo.TextReply", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword", "ID");
            AddForeignKey("dbo.KeywordAutoMessage", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword", "ID");
            AddForeignKey("dbo.Keyword", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword", "ID");
        }
    }
}

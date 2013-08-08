namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutoMessage_Keyword", "IsFistAutoMessage", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutoMessage_Keyword", "IsFistAutoMessage");
        }
    }
}

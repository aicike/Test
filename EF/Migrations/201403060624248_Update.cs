namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classify", "BackgroundColor", c => c.String(maxLength: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classify", "BackgroundColor");
        }
    }
}

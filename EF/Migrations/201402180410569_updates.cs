namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityInfo", "EnrollEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActivityInfo", "ActivityStratDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActivityInfo", "MaxCount", c => c.Int(nullable: false));
            AddColumn("dbo.AppAdvertorial", "BrowseCnt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppAdvertorial", "BrowseCnt");
            DropColumn("dbo.ActivityInfo", "MaxCount");
            DropColumn("dbo.ActivityInfo", "ActivityStratDate");
            DropColumn("dbo.ActivityInfo", "EnrollEndDate");
        }
    }
}

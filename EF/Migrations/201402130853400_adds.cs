namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityInfo", "MaxCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityInfo", "MaxCount");
        }
    }
}

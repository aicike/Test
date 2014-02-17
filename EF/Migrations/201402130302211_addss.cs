namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityInfo", "EnrollEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActivityInfo", "ActivityStratDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ActivityInfo", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityInfo", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ActivityInfo", "ActivityStratDate");
            DropColumn("dbo.ActivityInfo", "EnrollEndDate");
        }
    }
}

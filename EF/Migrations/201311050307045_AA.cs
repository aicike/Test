namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holiday", "HoliDayValue", c => c.DateTime(nullable: false));
            DropColumn("dbo.Holiday", "HoliDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Holiday", "HoliDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Holiday", "HoliDayValue");
        }
    }
}

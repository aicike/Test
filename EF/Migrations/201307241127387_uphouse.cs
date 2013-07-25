namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uphouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountMainHouseInfoDetail", "RoomNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountMainHouseInfoDetail", "RoomNumber");
        }
    }
}

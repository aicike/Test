namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountMainHouses", "TelSales", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountMainHouses", "TelSales");
        }
    }
}

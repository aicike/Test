namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountMainHouseInfo", "AccountMain_ID", "dbo.AccountMain");
            DropIndex("dbo.AccountMainHouseInfo", new[] { "AccountMain_ID" });
            AddColumn("dbo.AccountMainHouseType", "AccountMain_ID", c => c.Int());
            AddForeignKey("dbo.AccountMainHouseType", "AccountMain_ID", "dbo.AccountMain", "ID");
            CreateIndex("dbo.AccountMainHouseType", "AccountMain_ID");
            DropColumn("dbo.AccountMainHouseInfo", "AccountMain_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountMainHouseInfo", "AccountMain_ID", c => c.Int());
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMain_ID" });
            DropForeignKey("dbo.AccountMainHouseType", "AccountMain_ID", "dbo.AccountMain");
            DropColumn("dbo.AccountMainHouseType", "AccountMain_ID");
            CreateIndex("dbo.AccountMainHouseInfo", "AccountMain_ID");
            AddForeignKey("dbo.AccountMainHouseInfo", "AccountMain_ID", "dbo.AccountMain", "ID");
        }
    }
}

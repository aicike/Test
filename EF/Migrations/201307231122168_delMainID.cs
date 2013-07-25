namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delMainID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountMainHouseType", "AccountMain_ID", "dbo.AccountMain");
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMain_ID" });
            DropColumn("dbo.AccountMainHouseType", "AccountMain_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountMainHouseType", "AccountMain_ID", c => c.Int());
            CreateIndex("dbo.AccountMainHouseType", "AccountMain_ID");
            AddForeignKey("dbo.AccountMainHouseType", "AccountMain_ID", "dbo.AccountMain", "ID");
        }
    }
}

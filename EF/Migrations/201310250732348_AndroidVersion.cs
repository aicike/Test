namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AndroidVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountMain", "AndroidVersion", c => c.String());
            AddColumn("dbo.AccountMain", "IOSVersion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountMain", "IOSVersion");
            DropColumn("dbo.AccountMain", "AndroidVersion");
        }
    }
}

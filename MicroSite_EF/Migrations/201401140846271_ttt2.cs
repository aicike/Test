namespace MicroSite_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ttt2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountMain", "SystemUserID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountMain", "SystemUserID", c => c.Int(nullable: false));
        }
    }
}

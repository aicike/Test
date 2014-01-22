namespace MicroSite_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserDeliveryAddress", "TelePhone", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDeliveryAddress", "TelePhone", c => c.String());
        }
    }
}

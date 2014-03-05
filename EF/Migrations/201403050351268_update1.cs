namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientInfo", "ClientID", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientInfo", "ClientID", c => c.String(maxLength: 50));
        }
    }
}

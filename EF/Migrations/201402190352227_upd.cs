namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityInfo", "ActivityStratDate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityInfo", "ActivityStratDate", c => c.DateTime(nullable: false));
        }
    }
}

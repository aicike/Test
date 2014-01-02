namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "IsAppMenu", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Menu", "Controller", c => c.String(maxLength: 50));
            AlterColumn("dbo.Menu", "Action", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Menu", "Action", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Menu", "Controller", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Menu", "IsAppMenu");
        }
    }
}

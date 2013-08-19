namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Role", "IsCanFindByUser", c => c.Boolean());
            DropColumn("dbo.SystemUserRole", "IsCanFindByUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SystemUserRole", "IsCanFindByUser", c => c.Boolean());
            DropColumn("dbo.Role", "IsCanFindByUser");
        }
    }
}

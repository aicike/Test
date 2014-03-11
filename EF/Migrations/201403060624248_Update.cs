namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role");
            DropIndex("dbo.RoleMenu", new[] { "RoleID" });
            AddColumn("dbo.Classify", "BackgroundColor", c => c.String(maxLength: 7));
            CreateIndex("dbo.RoleMenu", "RoleID");
            AddForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role");
            DropIndex("dbo.RoleMenu", new[] { "RoleID" });
            DropColumn("dbo.Classify", "BackgroundColor");
            CreateIndex("dbo.RoleMenu", "RoleID");
            AddForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role", "ID");
        }
    }
}

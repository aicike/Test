namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Account", "RoleID", "dbo.Role");
            DropIndex("dbo.Account", new[] { "RoleID" });
            DropColumn("dbo.Account", "RoleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "RoleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Account", "RoleID");
            AddForeignKey("dbo.Account", "RoleID", "dbo.Role", "ID");
        }
    }
}

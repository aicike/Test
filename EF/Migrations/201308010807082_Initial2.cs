namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Email", c => c.String(maxLength: 50));
            AddColumn("dbo.UserLoginInfo", "Name", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.UserLoginInfo", "Address", c => c.String(maxLength: 50));
            AddColumn("dbo.UserLoginInfo", "Phone", c => c.String(maxLength: 20));
            AddColumn("dbo.UserLoginInfo", "HeadImagePath", c => c.String(maxLength: 500));
            AddColumn("dbo.UserLoginInfo", "IdentityCard", c => c.String(maxLength: 30));
            AlterColumn("dbo.UserLoginInfo", "Email", c => c.String(maxLength: 50));
            DropColumn("dbo.UserLoginInfo", "LoginName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserLoginInfo", "LoginName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.UserLoginInfo", "Email", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.UserLoginInfo", "IdentityCard");
            DropColumn("dbo.UserLoginInfo", "HeadImagePath");
            DropColumn("dbo.UserLoginInfo", "Phone");
            DropColumn("dbo.UserLoginInfo", "Address");
            DropColumn("dbo.UserLoginInfo", "Name");
            DropColumn("dbo.User", "Email");
        }
    }
}

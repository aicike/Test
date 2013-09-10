namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLoginInfo", "FindPwdCode", c => c.String());
            AddColumn("dbo.UserLoginInfo", "FindPwdTime", c => c.DateTime());
            AddColumn("dbo.UserLoginInfo", "FindPwdValidity", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLoginInfo", "FindPwdValidity");
            DropColumn("dbo.UserLoginInfo", "FindPwdTime");
            DropColumn("dbo.UserLoginInfo", "FindPwdCode");
        }
    }
}

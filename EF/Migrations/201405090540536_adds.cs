namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Merchant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        LogoImagePath = c.String(maxLength: 500),
                        Email = c.String(maxLength: 50),
                        LoginPwd = c.String(nullable: false, maxLength: 100),
                        LoginPwdPage = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Merchant");
        }
    }
}

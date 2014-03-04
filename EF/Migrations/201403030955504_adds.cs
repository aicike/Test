namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityInfoSignIn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        ActivityInfoID = c.Int(nullable: false),
                        EnumAdvertorialUType = c.Int(),
                        UserID = c.Int(),
                        Phone = c.String(maxLength: 15),
                        Name = c.String(maxLength: 30),
                        Email = c.String(maxLength: 30),
                        Company = c.String(maxLength: 50),
                        Position = c.String(maxLength: 30),
                        JoinDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityInfo", t => t.ActivityInfoID)
                .Index(t => t.ActivityInfoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityInfoSignIn", "ActivityInfoID", "dbo.ActivityInfo");
            DropIndex("dbo.ActivityInfoSignIn", new[] { "ActivityInfoID" });
            DropTable("dbo.ActivityInfoSignIn");
        }
    }
}

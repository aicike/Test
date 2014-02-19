namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppAdvertorialOperation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AppAdvertorialID = c.Int(nullable: false),
                        EnumAdvertorialUType = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ForwardCnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppAdvertorial", t => t.AppAdvertorialID)
                .Index(t => t.AppAdvertorialID);
            
            AddColumn("dbo.AccountMain", "IsOrganization", c => c.Boolean());
            AddColumn("dbo.AccountMain", "ParentAccountMainID", c => c.Int());
            CreateIndex("dbo.AccountMain", "ParentAccountMainID");
            AddForeignKey("dbo.AccountMain", "ParentAccountMainID", "dbo.AccountMain", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppAdvertorialOperation", "AppAdvertorialID", "dbo.AppAdvertorial");
            DropForeignKey("dbo.AccountMain", "ParentAccountMainID", "dbo.AccountMain");
            DropIndex("dbo.AppAdvertorialOperation", new[] { "AppAdvertorialID" });
            DropIndex("dbo.AccountMain", new[] { "ParentAccountMainID" });
            DropColumn("dbo.AccountMain", "ParentAccountMainID");
            DropColumn("dbo.AccountMain", "IsOrganization");
            DropTable("dbo.AppAdvertorialOperation");
        }
    }
}

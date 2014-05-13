namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.M_TakeOut", "SystemUserID", "dbo.SystemUser");
            DropIndex("dbo.M_TakeOut", new[] { "SystemUserID" });
            CreateTable(
                "dbo.M_CommunityMapping",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        M_TakeOutID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.M_TakeOut", t => t.M_TakeOutID)
                .Index(t => t.AccountMainID)
                .Index(t => t.M_TakeOutID);
            
            AddColumn("dbo.M_TakeOut", "Phone", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.M_TakeOut", "SystemUserID", c => c.Int());
            AlterColumn("dbo.M_TakeOut", "PublishDate", c => c.DateTime());
            CreateIndex("dbo.M_TakeOut", "SystemUserID");
            AddForeignKey("dbo.M_TakeOut", "SystemUserID", "dbo.SystemUser", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.M_TakeOut", "SystemUserID", "dbo.SystemUser");
            DropForeignKey("dbo.M_CommunityMapping", "M_TakeOutID", "dbo.M_TakeOut");
            DropForeignKey("dbo.M_CommunityMapping", "AccountMainID", "dbo.AccountMain");
            DropIndex("dbo.M_TakeOut", new[] { "SystemUserID" });
            DropIndex("dbo.M_CommunityMapping", new[] { "M_TakeOutID" });
            DropIndex("dbo.M_CommunityMapping", new[] { "AccountMainID" });
            AlterColumn("dbo.M_TakeOut", "PublishDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.M_TakeOut", "SystemUserID", c => c.Int(nullable: false));
            DropColumn("dbo.M_TakeOut", "Phone");
            DropTable("dbo.M_CommunityMapping");
            CreateIndex("dbo.M_TakeOut", "SystemUserID");
            AddForeignKey("dbo.M_TakeOut", "SystemUserID", "dbo.SystemUser", "ID");
        }
    }
}

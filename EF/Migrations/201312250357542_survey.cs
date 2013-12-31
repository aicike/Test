namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class survey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyMain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        SurveyTitle = c.String(nullable: false, maxLength: 50),
                        SurveyRemarks = c.String(nullable: false, maxLength: 500),
                        AccountID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.SurveyTrouble",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SurveyMainID = c.Int(nullable: false),
                        TroubleNumber = c.Int(nullable: false),
                        TroubleName = c.String(nullable: false, maxLength: 50),
                        EnumTroubleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveyMain", t => t.SurveyMainID)
                .Index(t => t.SurveyMainID);
            
            CreateTable(
                "dbo.SurveyOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SurveyTroubleID = c.Int(nullable: false),
                        Option = c.String(nullable: false, maxLength: 50),
                        Fraction = c.Double(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveyTrouble", t => t.SurveyTroubleID)
                .Index(t => t.SurveyTroubleID);
            
            CreateTable(
                "dbo.SurveyAnswer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SurveyTroubleID = c.Int(nullable: false),
                        SurveyOptionID = c.Int(),
                        Content = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                        UserID = c.Int(),
                        UserType = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveyTrouble", t => t.SurveyTroubleID)
                .ForeignKey("dbo.SurveyOption", t => t.SurveyOptionID)
                .Index(t => t.SurveyTroubleID)
                .Index(t => t.SurveyOptionID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyOptionID" });
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTroubleID" });
            DropIndex("dbo.SurveyOption", new[] { "SurveyTroubleID" });
            DropIndex("dbo.SurveyTrouble", new[] { "SurveyMainID" });
            DropIndex("dbo.SurveyMain", new[] { "AccountID" });
            DropIndex("dbo.SurveyMain", new[] { "AccountMainID" });
            DropForeignKey("dbo.SurveyAnswer", "SurveyOptionID", "dbo.SurveyOption");
            DropForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropForeignKey("dbo.SurveyOption", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropForeignKey("dbo.SurveyTrouble", "SurveyMainID", "dbo.SurveyMain");
            DropForeignKey("dbo.SurveyMain", "AccountID", "dbo.Account");
            DropForeignKey("dbo.SurveyMain", "AccountMainID", "dbo.AccountMain");
            DropTable("dbo.SurveyAnswer");
            DropTable("dbo.SurveyOption");
            DropTable("dbo.SurveyTrouble");
            DropTable("dbo.SurveyMain");
        }
    }
}

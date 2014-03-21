namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsupd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lottery_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Lottery_dishID = c.Int(),
                        Lottery_eggID = c.Int(),
                        Dish_Egg_detailID = c.Int(nullable: false),
                        UserID = c.Int(),
                        ActivityInfoParticipatorID = c.Int(),
                        SurveyAnswerUserID = c.Int(),
                        LotteryDate = c.DateTime(nullable: false),
                        EnumLotteryStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveyAnswerUser", t => t.SurveyAnswerUserID)
                .ForeignKey("dbo.ActivityInfoParticipator", t => t.ActivityInfoParticipatorID)
                .ForeignKey("dbo.Lottery_dish", t => t.Lottery_dishID)
                .ForeignKey("dbo.Lottery_egg", t => t.Lottery_eggID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.SurveyAnswerUserID)
                .Index(t => t.ActivityInfoParticipatorID)
                .Index(t => t.Lottery_dishID)
                .Index(t => t.Lottery_eggID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AppAdvertorialBrowse",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        BrowseDate = c.DateTime(nullable: false),
                        BrowseCnt = c.Int(nullable: false),
                        AppAdvertorialID = c.Int(),
                        ActivityInfoID = c.Int(),
                        SurveyMainID = c.Int(),
                        EnumBrowseAdvertorialType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityInfo", t => t.ActivityInfoID)
                .ForeignKey("dbo.AppAdvertorial", t => t.AppAdvertorialID)
                .ForeignKey("dbo.SurveyMain", t => t.SurveyMainID)
                .Index(t => t.ActivityInfoID)
                .Index(t => t.AppAdvertorialID)
                .Index(t => t.SurveyMainID);
            
            CreateTable(
                "dbo.Lottery_dish",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.Lottery_dish_detail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Lottery_dishID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Ratio = c.Double(nullable: false),
                        Image = c.String(),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lottery_dish", t => t.Lottery_dishID)
                .Index(t => t.Lottery_dishID);
            
            CreateTable(
                "dbo.Lottery_egg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.SurveyMain", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lottery_User", "UserID", "dbo.User");
            DropForeignKey("dbo.Lottery_User", "Lottery_eggID", "dbo.Lottery_egg");
            DropForeignKey("dbo.Lottery_User", "Lottery_dishID", "dbo.Lottery_dish");
            DropForeignKey("dbo.Lottery_dish_detail", "Lottery_dishID", "dbo.Lottery_dish");
            DropForeignKey("dbo.Lottery_dish", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Lottery_User", "ActivityInfoParticipatorID", "dbo.ActivityInfoParticipator");
            DropForeignKey("dbo.Lottery_User", "SurveyAnswerUserID", "dbo.SurveyAnswerUser");
            DropForeignKey("dbo.AppAdvertorialBrowse", "SurveyMainID", "dbo.SurveyMain");
            DropForeignKey("dbo.AppAdvertorialBrowse", "AppAdvertorialID", "dbo.AppAdvertorial");
            DropForeignKey("dbo.AppAdvertorialBrowse", "ActivityInfoID", "dbo.ActivityInfo");
            DropIndex("dbo.Lottery_User", new[] { "UserID" });
            DropIndex("dbo.Lottery_User", new[] { "Lottery_eggID" });
            DropIndex("dbo.Lottery_User", new[] { "Lottery_dishID" });
            DropIndex("dbo.Lottery_dish_detail", new[] { "Lottery_dishID" });
            DropIndex("dbo.Lottery_dish", new[] { "AccountMainID" });
            DropIndex("dbo.Lottery_User", new[] { "ActivityInfoParticipatorID" });
            DropIndex("dbo.Lottery_User", new[] { "SurveyAnswerUserID" });
            DropIndex("dbo.AppAdvertorialBrowse", new[] { "SurveyMainID" });
            DropIndex("dbo.AppAdvertorialBrowse", new[] { "AppAdvertorialID" });
            DropIndex("dbo.AppAdvertorialBrowse", new[] { "ActivityInfoID" });
            DropColumn("dbo.SurveyMain", "Content");
            DropTable("dbo.Lottery_egg");
            DropTable("dbo.Lottery_dish_detail");
            DropTable("dbo.Lottery_dish");
            DropTable("dbo.AppAdvertorialBrowse");
            DropTable("dbo.Lottery_User");
        }
    }
}

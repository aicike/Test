namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
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
                .ForeignKey("dbo.ActivityInfoParticipator", t => t.ActivityInfoParticipatorID)
                .ForeignKey("dbo.Lottery_dish", t => t.Lottery_dishID)
                .ForeignKey("dbo.Lottery_egg", t => t.Lottery_eggID)
                .ForeignKey("dbo.SurveyAnswerUser", t => t.SurveyAnswerUserID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.ActivityInfoParticipatorID)
                .Index(t => t.Lottery_dishID)
                .Index(t => t.Lottery_eggID)
                .Index(t => t.SurveyAnswerUserID)
                .Index(t => t.UserID);
            
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
            
            AddColumn("dbo.SurveyAnswerUser", "QQ_Weixin", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lottery_User", "UserID", "dbo.User");
            DropForeignKey("dbo.Lottery_User", "SurveyAnswerUserID", "dbo.SurveyAnswerUser");
            DropForeignKey("dbo.Lottery_User", "Lottery_eggID", "dbo.Lottery_egg");
            DropForeignKey("dbo.Lottery_User", "Lottery_dishID", "dbo.Lottery_dish");
            DropForeignKey("dbo.Lottery_dish_detail", "Lottery_dishID", "dbo.Lottery_dish");
            DropForeignKey("dbo.Lottery_dish", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Lottery_User", "ActivityInfoParticipatorID", "dbo.ActivityInfoParticipator");
            DropIndex("dbo.Lottery_User", new[] { "UserID" });
            DropIndex("dbo.Lottery_User", new[] { "SurveyAnswerUserID" });
            DropIndex("dbo.Lottery_User", new[] { "Lottery_eggID" });
            DropIndex("dbo.Lottery_User", new[] { "Lottery_dishID" });
            DropIndex("dbo.Lottery_dish_detail", new[] { "Lottery_dishID" });
            DropIndex("dbo.Lottery_dish", new[] { "AccountMainID" });
            DropIndex("dbo.Lottery_User", new[] { "ActivityInfoParticipatorID" });
            DropColumn("dbo.SurveyAnswerUser", "QQ_Weixin");
            DropTable("dbo.Lottery_egg");
            DropTable("dbo.Lottery_dish_detail");
            DropTable("dbo.Lottery_dish");
            DropTable("dbo.Lottery_User");
        }
    }
}

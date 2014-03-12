namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyAnswerUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        UserName = c.String(),
                        UserPhone = c.String(),
                        UserEmail = c.String(),
                        UserComPany = c.String(),
                        UserPosition = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Product", "EnumProductDiscountType", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Discount", c => c.Double());
            AddColumn("dbo.Product", "DiscountPrice", c => c.Double());
            AddColumn("dbo.Product", "IsRelease", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Sort", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyMain", "IsRegistered", c => c.Boolean(nullable: false));
            AddColumn("dbo.SurveyAnswer", "SurveyAnswerUserID", c => c.Int());
            CreateIndex("dbo.SurveyAnswer", "SurveyAnswerUserID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyAnswerUser", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyAnswerUser");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyAnswerUserID" });
            DropColumn("dbo.SurveyAnswer", "SurveyAnswerUserID");
            DropColumn("dbo.SurveyMain", "IsRegistered");
            DropColumn("dbo.Product", "Stock");
            DropColumn("dbo.Product", "Sort");
            DropColumn("dbo.Product", "IsRelease");
            DropColumn("dbo.Product", "DiscountPrice");
            DropColumn("dbo.Product", "Discount");
            DropColumn("dbo.Product", "EnumProductDiscountType");
            DropTable("dbo.SurveyAnswerUser");
        }
    }
}

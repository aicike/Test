namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTroubleID" });
            AddColumn("dbo.SurveyMain", "IsRegistered", c => c.Boolean(nullable: false));
            AddColumn("dbo.SurveyAnswer", "SurveyAnswerUserID", c => c.Int());
            AddColumn("dbo.SurveyAnswer", "SurveyTrouble_ID", c => c.Int());
            CreateIndex("dbo.SurveyAnswer", "SurveyAnswerUserID");
            CreateIndex("dbo.SurveyAnswer", "SurveyTrouble_ID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyTrouble", "ID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble", "ID");
            DropColumn("dbo.Product", "EnumProductDiscountType");
            DropColumn("dbo.Product", "Discount");
            DropColumn("dbo.Product", "DiscountPrice");
            DropColumn("dbo.Product", "IsRelease");
            DropColumn("dbo.Product", "Sort");
            DropColumn("dbo.Product", "Stock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Sort", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "IsRelease", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "DiscountPrice", c => c.Double());
            AddColumn("dbo.Product", "Discount", c => c.Double());
            AddColumn("dbo.Product", "EnumProductDiscountType", c => c.Int(nullable: false));
            DropForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble");
            DropForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyTrouble");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTrouble_ID" });
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyAnswerUserID" });
            DropColumn("dbo.SurveyAnswer", "SurveyTrouble_ID");
            DropColumn("dbo.SurveyAnswer", "SurveyAnswerUserID");
            DropColumn("dbo.SurveyMain", "IsRegistered");
            CreateIndex("dbo.SurveyAnswer", "SurveyTroubleID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble", "ID");
        }
    }
}

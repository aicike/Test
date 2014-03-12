namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role");
            DropForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropIndex("dbo.RoleMenu", new[] { "RoleID" });
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTroubleID" });
            AddColumn("dbo.Product", "EnumProductDiscountType", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Discount", c => c.Double());
            AddColumn("dbo.Product", "DiscountPrice", c => c.Double());
            AddColumn("dbo.Product", "IsRelease", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Sort", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyMain", "IsRegistered", c => c.Boolean(nullable: false));
            AddColumn("dbo.SurveyAnswer", "SurveyAnswerUserID", c => c.Int());
            AddColumn("dbo.SurveyAnswer", "SurveyTrouble_ID", c => c.Int());
            CreateIndex("dbo.SurveyAnswer", "SurveyAnswerUserID");
            CreateIndex("dbo.RoleMenu", "RoleID");
            CreateIndex("dbo.SurveyAnswer", "SurveyTrouble_ID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyTrouble", "ID");
            AddForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble");
            DropForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role");
            DropForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyTrouble");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTrouble_ID" });
            DropIndex("dbo.RoleMenu", new[] { "RoleID" });
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyAnswerUserID" });
            DropColumn("dbo.SurveyAnswer", "SurveyTrouble_ID");
            DropColumn("dbo.SurveyAnswer", "SurveyAnswerUserID");
            DropColumn("dbo.SurveyMain", "IsRegistered");
            DropColumn("dbo.Product", "Stock");
            DropColumn("dbo.Product", "Sort");
            DropColumn("dbo.Product", "IsRelease");
            DropColumn("dbo.Product", "DiscountPrice");
            DropColumn("dbo.Product", "Discount");
            DropColumn("dbo.Product", "EnumProductDiscountType");
            CreateIndex("dbo.SurveyAnswer", "SurveyTroubleID");
            CreateIndex("dbo.RoleMenu", "RoleID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble", "ID");
            AddForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role", "ID");
        }
    }
}

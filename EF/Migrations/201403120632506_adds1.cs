namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTrouble_ID" });
            CreateIndex("dbo.SurveyAnswer", "SurveyTroubleID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble", "ID");
            DropColumn("dbo.SurveyAnswer", "SurveyTrouble_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyAnswer", "SurveyTrouble_ID", c => c.Int());
            DropForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTroubleID" });
            CreateIndex("dbo.SurveyAnswer", "SurveyTrouble_ID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble", "ID");
        }
    }
}

namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTrouble_ID" });
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
            
            CreateIndex("dbo.SurveyAnswer", "SurveyTroubleID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble", "ID");
            DropColumn("dbo.SurveyAnswer", "SurveyTrouble_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyAnswer", "SurveyTrouble_ID", c => c.Int());
            DropForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTroubleID" });
            DropTable("dbo.SurveyAnswerUser");
            CreateIndex("dbo.SurveyAnswer", "SurveyTrouble_ID");
            AddForeignKey("dbo.SurveyAnswer", "SurveyTrouble_ID", "dbo.SurveyTrouble", "ID");
        }
    }
}

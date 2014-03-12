namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upda : DbMigration
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
            
            AddForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyAnswerUser", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyAnswer", "SurveyAnswerUserID", "dbo.SurveyAnswerUser");
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyAnswerUserID" });
            DropTable("dbo.SurveyAnswerUser");
        }
    }
}

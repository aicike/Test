namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyAnswerUser", "QQ_Weixin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyAnswerUser", "QQ_Weixin");
        }
    }
}

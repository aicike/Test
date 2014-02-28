namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityInfo", "Content", c => c.String(nullable: false));
            AddColumn("dbo.ActivityInfo", "MainImagPath", c => c.String(nullable: false));
            AddColumn("dbo.ActivityInfo", "AppShowImagePath", c => c.String());
            AddColumn("dbo.ActivityInfo", "MinImagePath", c => c.String());
            AddColumn("dbo.ActivityInfo", "ISGenerateUserAdvisory", c => c.Int(nullable: false));
            AddColumn("dbo.ActivityInfo", "ISGenerateSaleAdvisory", c => c.Int(nullable: false));
            AddColumn("dbo.AppAdvertorial", "UrlID", c => c.Int());
            AddColumn("dbo.SurveyMain", "MainImagPath", c => c.String(nullable: false));
            AddColumn("dbo.SurveyMain", "AppShowImagePath", c => c.String());
            AddColumn("dbo.SurveyMain", "MinImagePath", c => c.String());
            AddColumn("dbo.SurveyMain", "ISGenerateUserAdvisory", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyMain", "ISGenerateSaleAdvisory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyMain", "ISGenerateSaleAdvisory");
            DropColumn("dbo.SurveyMain", "ISGenerateUserAdvisory");
            DropColumn("dbo.SurveyMain", "MinImagePath");
            DropColumn("dbo.SurveyMain", "AppShowImagePath");
            DropColumn("dbo.SurveyMain", "MainImagPath");
            DropColumn("dbo.AppAdvertorial", "UrlID");
            DropColumn("dbo.ActivityInfo", "ISGenerateSaleAdvisory");
            DropColumn("dbo.ActivityInfo", "ISGenerateUserAdvisory");
            DropColumn("dbo.ActivityInfo", "MinImagePath");
            DropColumn("dbo.ActivityInfo", "AppShowImagePath");
            DropColumn("dbo.ActivityInfo", "MainImagPath");
            DropColumn("dbo.ActivityInfo", "Content");
        }
    }
}

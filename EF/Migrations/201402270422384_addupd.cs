namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addupd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityInfo", "MainImagPath", c => c.String());
            AlterColumn("dbo.SurveyMain", "MainImagPath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SurveyMain", "MainImagPath", c => c.String(nullable: false));
            AlterColumn("dbo.ActivityInfo", "MainImagPath", c => c.String(nullable: false));
        }
    }
}

namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyMain", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyMain", "Content");
        }
    }
}

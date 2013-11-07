namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskRule", "TaskInfoName", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskRule", "TaskInfoName", c => c.String(maxLength: 30));
        }
    }
}

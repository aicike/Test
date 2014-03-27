namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppAdvertorial", "ActivitySignUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppAdvertorial", "ActivitySignUrl");
        }
    }
}

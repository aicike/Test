namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityInfoParticipator", "Company", c => c.String(maxLength: 50));
            AddColumn("dbo.ActivityInfoParticipator", "Position", c => c.String(maxLength: 30));
            AlterColumn("dbo.ActivityInfoParticipator", "Phone", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityInfoParticipator", "Phone", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.ActivityInfoParticipator", "Position");
            DropColumn("dbo.ActivityInfoParticipator", "Company");
        }
    }
}

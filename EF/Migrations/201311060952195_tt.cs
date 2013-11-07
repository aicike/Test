namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Task", name: "TaskRule_ID", newName: "TaskRuleID");
            DropColumn("dbo.Task", "TaskInfoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "TaskInfoID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Task", name: "TaskRuleID", newName: "TaskRule_ID");
        }
    }
}

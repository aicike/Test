namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AutoMessage_Add", "Content", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AutoMessage_Add", "Content", c => c.String(nullable: false, maxLength: 4000));
        }
    }
}

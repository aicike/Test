namespace MicroSite_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classify", "Depict", c => c.String(maxLength: 20));
            AlterColumn("dbo.Product", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Classify", "Depict");
        }
    }
}

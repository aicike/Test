namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppAdvertorial", "EnumAdvertorialUType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppAdvertorial", "EnumAdvertorialUType");
        }
    }
}

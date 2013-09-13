namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LibraryImageText", "Summary", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LibraryImageText", "Summary", c => c.String(nullable: false, maxLength: 200));
        }
    }
}

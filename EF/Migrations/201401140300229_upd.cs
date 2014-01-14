namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classify", "ImgPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classify", "ImgPath");
        }
    }
}

namespace ShortURL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.URLMap",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShortUrl = c.String(maxLength: 6),
                        FullUrl = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.URLMap");
        }
    }
}

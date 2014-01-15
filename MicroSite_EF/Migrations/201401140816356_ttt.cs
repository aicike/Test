namespace MicroSite_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ttt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        PImgOriginal = c.String(),
                        PImgMini = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.ProductID);
            
            AddColumn("dbo.Product", "Freight", c => c.Double(nullable: false));
            AddColumn("dbo.Classify", "ImgPath", c => c.String());
            DropColumn("dbo.Product", "imgFilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "imgFilePath", c => c.String());
            DropForeignKey("dbo.ProductImg", "ProductID", "dbo.Product");
            DropIndex("dbo.ProductImg", new[] { "ProductID" });
            DropColumn("dbo.Classify", "ImgPath");
            DropColumn("dbo.Product", "Freight");
            DropTable("dbo.ProductImg");
        }
    }
}

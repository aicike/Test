namespace MicroSite_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "BeginDate", c => c.DateTime());
            AlterColumn("dbo.Order", "EndDate", c => c.DateTime());
            DropColumn("dbo.OrderDetail", "ProductImg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetail", "ProductImg", c => c.String());
            AlterColumn("dbo.Order", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Order", "BeginDate", c => c.DateTime(nullable: false));
        }
    }
}

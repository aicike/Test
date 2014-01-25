namespace MicroSite_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetail", "ProductImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetail", "ProductImg");
        }
    }
}

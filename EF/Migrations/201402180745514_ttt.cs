namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ttt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountMain", "ParrentAccountMainID", "dbo.AccountMain");
            DropIndex("dbo.AccountMain", new[] { "ParrentAccountMainID" });
            RenameColumn(table: "dbo.AccountMain", name: "ParrentAccountMainID", newName: "ParentAccountMainID");
            CreateIndex("dbo.AccountMain", "ParentAccountMainID");
            AddForeignKey("dbo.AccountMain", "ParentAccountMainID", "dbo.AccountMain", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountMain", "ParentAccountMainID", "dbo.AccountMain");
            DropIndex("dbo.AccountMain", new[] { "ParentAccountMainID" });
            RenameColumn(table: "dbo.AccountMain", name: "ParentAccountMainID", newName: "ParrentAccountMainID");
            CreateIndex("dbo.AccountMain", "ParrentAccountMainID");
            AddForeignKey("dbo.AccountMain", "ParrentAccountMainID", "dbo.AccountMain", "ID");
        }
    }
}

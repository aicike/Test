namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.M_Domestic", "PriceRemark", c => c.String(nullable: false));
            AddColumn("dbo.M_DryCleaning", "PriceRemark", c => c.String(nullable: false));
            AddColumn("dbo.M_EducationTrain", "PriceRemark", c => c.String(nullable: false));
            AddColumn("dbo.M_PetHospital", "PriceRemark", c => c.String(nullable: false));
            AddColumn("dbo.M_Tutor", "PriceRemark", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.M_Tutor", "PriceRemark");
            DropColumn("dbo.M_PetHospital", "PriceRemark");
            DropColumn("dbo.M_EducationTrain", "PriceRemark");
            DropColumn("dbo.M_DryCleaning", "PriceRemark");
            DropColumn("dbo.M_Domestic", "PriceRemark");
        }
    }
}

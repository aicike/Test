namespace MicroSite_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {

            //var migrationDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\");
            //var ddlSqlFiles = new string[] { "InitialProvince.sql", "Initial.sql" };
            //foreach (var file in ddlSqlFiles)
            //{
            //    string scriptText = System.IO.File.ReadAllText(System.IO.Path.Combine(migrationDir, file));
            //    var commandTexts = scriptText.Split(new string[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var commandText in commandTexts)
            //    {
            //        if (!String.IsNullOrWhiteSpace(commandText))
            //            Sql(commandText);
            //    }
            //}
        }
        
        public override void Down()
        {
        }
    }
}

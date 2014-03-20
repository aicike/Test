namespace ShortURL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ShortURL.EF;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EF.Context context)
        {

        }
    }
}

namespace MusicBrowser.Console.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicBrowser.Console.DataAccess.EntityFramework.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataAccess\EntityFramework\Migrations";
        }

        protected override void Seed(MusicBrowser.Console.DataAccess.EntityFramework.DataContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

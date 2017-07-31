namespace Sandbox.Migrations
{
    using EFSoftDelete;    
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonTestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            this.UseSoftDeleteMigrator();
        }
    }
}

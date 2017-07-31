using ConsoleApp1.Migrations;
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            using (PersonTestContext ctx = new PersonTestContext())
            {
                // Use this to insert a new person with a new car
                //var person = new Person { Name = "Akos", Age = 19 };
                //person.Cars.Add(new Car { LicensePlate = "123ABC" });
                //ctx.People.Add(person);
                //ctx.SaveChanges();

                // Use this to delete the first person
                var person = ctx.People.FirstOrDefault();
                ctx.People.Remove(person);
                ctx.SaveChanges();
            }

            // Use this to script the migration to the debug window
            //var configuration = new Configuration();
            //var migrator = new DbMigrator(configuration);

            //var scriptor = new MigratorScriptingDecorator(migrator);
            //string script = scriptor.ScriptUpdate(sourceMigration: null, targetMigration: null);
            //Debug.WriteLine(script);
        }
    }
}

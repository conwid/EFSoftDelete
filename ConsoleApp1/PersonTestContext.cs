using EFSoftDelete;
using System.Data.Entity;

namespace Sandbox
{
    public class PersonTestContext : DbContext
    {
        public PersonTestContext() : base("name=MTest")
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<Car> Cars { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().UseSoftDelete();
            modelBuilder.Entity<Car>().UseSoftDelete();
        }
    }
}

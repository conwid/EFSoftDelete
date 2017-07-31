using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSoftDelete
{
    public static class SoftDeleteExtensions
    {
        public static EntityTypeConfiguration<TEntity> UseSoftDelete<TEntity>(this EntityTypeConfiguration<TEntity> config) where TEntity : class
        {
            // TODO: Refactor magic string 
            config.HasTableAnnotation("SoftDelete", true);
            config.MapToStoredProcedures();
            return config;
        }
        public static void UseSoftDeleteMigrator<TContext>(this DbMigrationsConfiguration<TContext> configuration) where TContext : DbContext
        {            
            configuration.SetSqlGenerator("System.Data.SqlClient", new SoftDeleteMigrator());
        }
    }
}

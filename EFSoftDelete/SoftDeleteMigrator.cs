using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Diagnostics;

namespace EFSoftDelete
{
    public class SoftDeleteMigrator : SqlServerMigrationSqlGenerator
    {       
        protected override void Generate(CreateTableOperation createTableOperation)
        {
            // TODO: Refactor magic string 
            if (createTableOperation.Annotations.TryGetValue("SoftDelete", out object hasSoftDeleteObject))
            {
                if (bool.TryParse(hasSoftDeleteObject.ToString(), out bool hasSoftDeleteResult) && hasSoftDeleteResult)
                {
                    createTableOperation.Columns.Add( new ColumnModel(PrimitiveTypeKind.Boolean) {
                                                                        // TODO: Refactor magic string 
                                                                        Name = "IsDeleted",
                                                                        IsNullable = false,
                                                                        DefaultValueSql = "0"
                                                    });
                    base.Generate(createTableOperation);
                    using (var writer = Writer())
                    {
                        // TODO: Refactor function name creation to a common place, so it can be referred to form the CREATE SECURITY POLICY clause
                        writer.WriteLine(
                            $"CREATE FUNCTION {createTableOperation.Name.ToLower()}Predicate(@IsDeleted AS bit)" +
                            "RETURNS TABLE" + Environment.NewLine +
                            "WITH SCHEMABINDING" + Environment.NewLine +
                            "AS" + Environment.NewLine +
                                "RETURN SELECT 1 AS accessResult" + Environment.NewLine +
                                "WHERE @IsDeleted=0;" + Environment.NewLine
                            );
                        writer.WriteLine(
                           $"CREATE SECURITY POLICY {createTableOperation.Name}Filter" + Environment.NewLine +
                           $"ADD FILTER PREDICATE {createTableOperation.Name.ToLower()}Predicate(IsDeleted)" + Environment.NewLine +
                           $"ON {createTableOperation.Name}" + Environment.NewLine +
                           "WITH(STATE = ON);" + Environment.NewLine
                            );
                        Statement(writer);
                    }
                }
            }
            else
            {
                base.Generate(createTableOperation);
            }
        }
    }
}

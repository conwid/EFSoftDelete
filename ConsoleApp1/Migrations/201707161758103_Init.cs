namespace Sandbox.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        LicensePlate = c.String(),
                        PersonId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "SoftDelete", "True" },
                })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "SoftDelete", "True" },
                })
                .PrimaryKey(t => t.PersonId);
            
            CreateStoredProcedure(
                "dbo.Car_Insert",
                p => new
                    {
                        LicensePlate = p.String(),
                        PersonId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Cars]([LicensePlate], [PersonId])
                      VALUES (@LicensePlate, @PersonId)
                      
                      DECLARE @CarId int
                      SELECT @CarId = [CarId]
                      FROM [dbo].[Cars]
                      WHERE @@ROWCOUNT > 0 AND [CarId] = scope_identity()
                      
                      SELECT t0.[CarId]
                      FROM [dbo].[Cars] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[CarId] = @CarId"
            );
            
            CreateStoredProcedure(
                "dbo.Car_Update",
                p => new
                    {
                        CarId = p.Int(),
                        LicensePlate = p.String(),
                        PersonId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Cars]
                      SET [LicensePlate] = @LicensePlate, [PersonId] = @PersonId
                      WHERE ([CarId] = @CarId)"
            );
            
            CreateStoredProcedure(
                "dbo.Car_Delete",
                p => new
                    {
                        CarId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Cars]
                      WHERE ([CarId] = @CarId)"
            );
            
            CreateStoredProcedure(
                "dbo.Person_Insert",
                p => new
                    {
                        Name = p.String(),
                        Age = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[People]([Name], [Age])
                      VALUES (@Name, @Age)
                      
                      DECLARE @PersonId int
                      SELECT @PersonId = [PersonId]
                      FROM [dbo].[People]
                      WHERE @@ROWCOUNT > 0 AND [PersonId] = scope_identity()
                      
                      SELECT t0.[PersonId]
                      FROM [dbo].[People] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[PersonId] = @PersonId"
            );
            
            CreateStoredProcedure(
                "dbo.Person_Update",
                p => new
                    {
                        PersonId = p.Int(),
                        Name = p.String(),
                        Age = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[People]
                      SET [Name] = @Name, [Age] = @Age
                      WHERE ([PersonId] = @PersonId)"
            );
            
            CreateStoredProcedure(
                "dbo.Person_Delete",
                p => new
                    {
                        PersonId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[People]
                      WHERE ([PersonId] = @PersonId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Person_Delete");
            DropStoredProcedure("dbo.Person_Update");
            DropStoredProcedure("dbo.Person_Insert");
            DropStoredProcedure("dbo.Car_Delete");
            DropStoredProcedure("dbo.Car_Update");
            DropStoredProcedure("dbo.Car_Insert");
            DropForeignKey("dbo.Cars", "PersonId", "dbo.People");
            DropIndex("dbo.Cars", new[] { "PersonId" });
            DropTable("dbo.People",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "SoftDelete", "True" },
                });
            DropTable("dbo.Cars",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "SoftDelete", "True" },
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Juliapos.Portal.ProductApi.Db.Postgres
{
    public static class MigrationBuilderExtensions
    {
        public static void CreateView(this MigrationBuilder migrationBuilder, string viewName, string schemaName, string selectStatement)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"CREATE VIEW \"{schemaName}\".\"{viewName}\" AS {selectStatement}");
            else
                migrationBuilder.Sql($"CREATE VIEW \"{viewName}\" AS {selectStatement}");
        }

        public static void UpdateView(this MigrationBuilder migrationBuilder, string viewName, string schemaName, string selectStatement)
        {
            DropView(migrationBuilder, viewName, schemaName);
            CreateView(migrationBuilder, viewName, schemaName, selectStatement);
        }

        public static void DropView(this MigrationBuilder migrationBuilder, string viewName, string schemaName)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"DROP VIEW \"{schemaName}\".\"{viewName}\"");
            else
                migrationBuilder.Sql($"DROP VIEW \"{viewName}\"");
        }


        public static void CreateFunction(this MigrationBuilder migrationBuilder, string functionName, string schemaName, string selectStatement)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"CREATE OR REPLACE FUNCTION \"{schemaName}\".\"{functionName}\" {selectStatement}");
            else
                migrationBuilder.Sql($"CREATE OR REPLACE FUNCTION \"{functionName}\" {selectStatement}");
        }

        public static void UpdateFunction(this MigrationBuilder migrationBuilder, string functionName, string schemaName, string selectStatement)
        {
            DropFunction(migrationBuilder, functionName, schemaName);
            CreateFunction(migrationBuilder, functionName, schemaName, selectStatement);
        }

        public static void DropFunction(this MigrationBuilder migrationBuilder, string functionName, string schemaName)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"DROP FUNCTION \"{schemaName}\".\"{functionName}\"");
            else
                migrationBuilder.Sql($"DROP FUNCTION \"{functionName}\"");
        }


        public static void CreateProcedure(this MigrationBuilder migrationBuilder, string procedureName, string schemaName, string selectStatement)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"CREATE OR REPLACE PROCEDURE \"{schemaName}\".\"{procedureName}\" {selectStatement}");
            else
                migrationBuilder.Sql($"CREATE OR REPLACE PROCEDURE \"{procedureName}\" {selectStatement}");
        }

        public static void UpdateProcedure(this MigrationBuilder migrationBuilder, string procedureName, string schemaName, string selectStatement)
        {
            DropFunction(migrationBuilder, procedureName, schemaName);
            CreateFunction(migrationBuilder, procedureName, schemaName, selectStatement);
        }

        public static void DropProcedure(this MigrationBuilder migrationBuilder, string procedureName, string schemaName)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"DROP PROCEDURE \"{schemaName}\".\"{procedureName}\"");
            else
                migrationBuilder.Sql($"DROP PROCEDURE \"{procedureName}\"");
        }

        public static void CreateType(this MigrationBuilder migrationBuilder, string typeName, string schemaName, string selectStatement)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"CREATE TYPE \"{schemaName}\".\"{typeName}\" AS {selectStatement}");
            else
                migrationBuilder.Sql($"CREATE TYPE \"{typeName}\" AS {selectStatement}");
        }

        public static void UpdateType(this MigrationBuilder migrationBuilder, string typeName, string schemaName, string selectStatement)
        {
            DropFunction(migrationBuilder, typeName, schemaName);
            CreateFunction(migrationBuilder, typeName, schemaName, selectStatement);
        }

        public static void DropType(this MigrationBuilder migrationBuilder, string typeName, string schemaName)
        {
            if (!string.IsNullOrEmpty(schemaName))
                migrationBuilder.Sql($"DROP TYPE \"{schemaName}\".\"{typeName}\"");
            else
                migrationBuilder.Sql($"DROP TYPE \"{typeName}\"");
        }


    }
}

﻿using System.IO;
using Evolve.Connection;
using Evolve.Dialect.SQLite;
using Evolve.Migration;
using Microsoft.Data.Sqlite;

namespace Evolve.Core.Test
{
    public static class TestUtil
    {
        public static WrappedConnection CreateSQLiteWrappedCnx() => new WrappedConnection(new SqliteConnection(TestContext.SQLiteInMemoryConnectionString));

        public static SQLiteSchema GetDefaultSQLiteSchema(WrappedConnection connection) => new SQLiteSchema(connection);

        public static SQLiteSchema LoadChinookDatabase(WrappedConnection connection)
        {
            connection.Open();
            using (var command = connection.DbConnection.CreateCommand())
            {
                command.CommandText = File.ReadAllText(TestContext.ChinookScriptPath);
                command.ExecuteNonQuery();
            }

            return new SQLiteSchema(connection);
        }
    }

    public class FakeMigrationScript : MigrationScript
    {
        public FakeMigrationScript(string content)
            : base("1", "no description", "no name", content)
        { }
    }
}

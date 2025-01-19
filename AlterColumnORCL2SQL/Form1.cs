namespace AlterColumnORCL2SQL
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Data.SqlClient;
    using System.Diagnostics;
    using Oracle.ManagedDataAccess.Client;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void ConvertOracleToSqlServer(string inputFilePath, string outputFilePath)
        {
            try
            {
                // Read the content of the Oracle SQL file
                string oracleSqlContent = File.ReadAllText(inputFilePath);

                // Process the Oracle SQL content to generate SQL Server syntax
                string sqlServerSqlContent = ConvertToSqlServer(oracleSqlContent);

                // Write the SQL Server content to the output file
                File.WriteAllText(outputFilePath, sqlServerSqlContent);
                Process.Start(new ProcessStartInfo(outputFilePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during conversion: {ex.Message}");
            }
        }

        private static string ConvertToSqlServer(string oracleSql)
        {
            var tableColumnsMap = new Dictionary<string, List<string>>();

            // Split the input Oracle SQL content into individual `ALTER TABLE` statements
            string[] alterStatements = oracleSql.Split(new[] { "ALTER TABLE" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var statement in alterStatements)
            {
                // Extract the table name
                string tableName = ExtractTableName(statement);

                // Extract the column definitions
                int columnsStart = statement.IndexOf("(") + 1;
                int columnsEnd = statement.LastIndexOf(")");
                string[] columns = statement.Substring(columnsStart, columnsEnd - columnsStart).Split(',');

                // Group the columns by table
                if (!tableColumnsMap.ContainsKey(tableName))
                {
                    tableColumnsMap[tableName] = new List<string>();
                }

                foreach (var column in columns)
                {
                    
                        string[] parts = column.Trim().Split(new[] { ' ' }, 2);
                        string columnName = parts[0];
                        string columnType = parts[1].Replace("VARCHAR2", "NVARCHAR").Replace(" CHAR", "");
                        tableColumnsMap[tableName].Add($"ALTER COLUMN {columnName} {columnType}");


                }
            }

            // Generate SQL Server syntax
            var sqlServerStatements = new StringBuilder();
            foreach (var tableName in tableColumnsMap.Keys)
            {
                foreach (var columnDefinition in tableColumnsMap[tableName])
                {
                    string modifiedTableName = "";
                    // Find the index of the dot (.)
                    int dotIndex = tableName.IndexOf('.');

                    // If the dot exists, replace the part before it
                    if (dotIndex != -1)
                    {
                        modifiedTableName = "mline" + tableName.Substring(dotIndex);
                    }
                    else
                    {
                        modifiedTableName = tableName;
                    }

                    sqlServerStatements.AppendLine($"ALTER TABLE {modifiedTableName} {columnDefinition};");
                }
            }

            return sqlServerStatements.ToString();
        }

        private static string ExtractTableName(string statement)
        {
            int tableNameStart = statement.IndexOf(" ") + 1;
            int tableNameEnd = statement.IndexOf("modify", StringComparison.OrdinalIgnoreCase);
            return statement.Substring(tableNameStart, tableNameEnd - tableNameStart).Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oracleFilePath = txtOracle.Text;
            string sqlServerFilePath = txtMSSQL.Text;

            ConvertOracleToSqlServer(oracleFilePath, sqlServerFilePath);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sqlServerFilePath = txtMSSQL.Text;
            string connectionString = txtConnectionString.Text;// "Server=KINITH;Database=TrunkJava21;User Id=sa;Password=Abcd_123;TrustServerCertificate=True;";

            string logFilePath = txtLog.Text;

            ExecuteSqlStatements(sqlServerFilePath, logFilePath, connectionString);
            Process.Start(new ProcessStartInfo(logFilePath) { UseShellExecute = true });

        }

        static void ExecuteSqlStatements(string sqlFilePath, string logFilePath, string connectionString)
        {
            File.AppendAllText(logFilePath, $"Testing alteration for  : {connectionString}\n\n");

            try
            {
                // Read all SQL statements from the file
                string[] sqlStatements = File.ReadAllText(sqlFilePath)
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // Ensure log file is empty before starting
                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath);
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (string statement in sqlStatements)
                    {
                        string trimmedStatement = statement.Trim();
                        if (string.IsNullOrWhiteSpace(trimmedStatement)) continue;

                        try
                        {
                            using (SqlCommand command = new SqlCommand(trimmedStatement, connection))
                            {
                                command.ExecuteNonQuery();
                                File.AppendAllText(logFilePath, $"Passed : {trimmedStatement}\n\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the failed statement and error message to the log file
                            File.AppendAllText(logFilePath, $"=====================================================================================================\nFailed : {trimmedStatement}\nError: {ex.Message}\n=====================================================================================================\n\n");
                        }
                    }

                    File.AppendAllText(logFilePath, "Execution completed. Check the log file for any failed statements.\n\n");
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, $"Executed SQL: An error occurred: {ex.Message}\n\n");
            }
        }

        // Method to generate ALTER TABLE statements for VARCHAR2 BYTE columns
        public static void GenerateAlterStatements(string connectionString, string schemaName, string filePath)
        {
            File.WriteAllText(filePath, $"");
            try
            {
                // Establish a connection to the Oracle database
                using (var connection = new OracleConnection(connectionString))
                {
                    Console.WriteLine("Connecting to Oracle database...");
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    // Query to fetch all columns of type VARCHAR2 or CHAR and CHAR_USED = 'B' in the given schema
                    string query = $"SELECT t.owner, t.table_name, c.column_name, c.data_type, c.data_length, c.nullable FROM dba_tables t JOIN dba_tab_columns c ON t.owner = c.owner AND t.table_name = c.table_name WHERE c.data_type IN ('VARCHAR2', 'CHAR') AND c.char_used = 'B' AND t.owner = '{schemaName.ToUpper()}' ORDER BY t.owner, t.table_name, c.column_name";

                    using (var command = new OracleCommand(query, connection))
                    {
                        // Add the schema name parameter
                        command.Parameters.Add(new OracleParameter(":schemaName", schemaName));
                        Console.WriteLine($"Executing query for schema: {schemaName}");

                        using (var reader = command.ExecuteReader())
                        {
                            // Check if any rows are returned
                            if (reader.HasRows)
                            {
                                Console.WriteLine("Rows found, processing...");

                                // Prepare to group columns by table name
                                var tables = new Dictionary<string, List<string>>();

                                while (reader.Read())
                                {
                                    // Safely get column values
                                    string tableName = reader["table_name"].ToString();
                                    string columnName = reader["column_name"].ToString();
                                    string dataType = reader["data_type"].ToString();
                                    int columnSize = Convert.ToInt32(reader["data_length"]);

                                    // Debugging output
                                    Console.WriteLine($"Table: {tableName}, Column: {columnName}, Type: {dataType}, Size: {columnSize}");

                                    // Add the column to the corresponding table in the dictionary
                                    if (!tables.ContainsKey(tableName))
                                    {
                                        tables[tableName] = new List<string>();
                                    }

                                    // Add column definition for the 'ALTER TABLE' statement
                                    tables[tableName].Add($"{columnName} {dataType}({columnSize} CHAR)");
                                }

                                // Generate the ALTER TABLE statements
                                StringBuilder alterStatements = new StringBuilder();

                                foreach (var table in tables)
                                {
                                    string tableName = table.Key;
                                    List<string> columns = table.Value;

                                    // For each table, start the ALTER TABLE statement
                                    alterStatements.Append($"ALTER TABLE {schemaName}.{tableName} MODIFY (");

                                    int countFields = 0;
                                    foreach (var columnDefinition in columns)
                                    {
                                        alterStatements.Append(columnDefinition);

                                        countFields++;

                                        if (countFields != columns.Count)
                                        {
                                            alterStatements.Append(", ");
                                        }
                                    }

                                    // Close the last ALTER TABLE statement
                                    alterStatements.Append(");\n");
                                }

                                // Output the generated SQL statements to the file
                                File.AppendAllText(filePath, alterStatements.ToString());
                                Console.WriteLine("SQL statements appended to file.");
                            }
                            else
                            {
                                Console.WriteLine("No rows found for the specified schema.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions and log the error
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string connectionString = txtConnectionORCL.Text;// "User Id=system;Password=Abcd_123;Data Source=ORCL";
            GenerateAlterStatements(connectionString, txtSchema.Text, txtOracle.Text);
            Process.Start(new ProcessStartInfo(txtOracle.Text) { UseShellExecute = true });
        }

        private void txtSchema_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTestORCL_Click(object sender, EventArgs e)
        {
            string connectionString = txtConnectionORCL.Text;// "User Id=system;Password=Abcd_123;Data Source=ORCL";
            ExecuteOracleSqlStatements(txtOracle.Text, txtOracleLog.Text, connectionString);
        }

        static void ExecuteOracleSqlStatements(string sqlFilePath, string logFilePath, string connectionString)
        {
            // Ensure log file is empty before starting
            File.WriteAllText(logFilePath, $"Starting execution for: {connectionString}\n\n");

            try
            {
                // Read all SQL statements from the file, splitting by semicolon
                string[] sqlStatements = File.ReadAllText(sqlFilePath)
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                using (var connection = new OracleConnection(connectionString))
                {
                    Console.WriteLine("Connecting to Oracle database...");
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    foreach (string statement in sqlStatements)
                    {
                        string trimmedStatement = statement.Trim();
                        if (string.IsNullOrWhiteSpace(trimmedStatement)) continue;

                        try
                        {
                            using (var command = new OracleCommand(trimmedStatement, connection))
                            {
                                command.ExecuteNonQuery();
                                // Log success
                                File.AppendAllText(logFilePath, $"Passed: {trimmedStatement}\n\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log failure and error message
                            File.AppendAllText(logFilePath, $"=====================================================================================================\nFailed: {trimmedStatement}\nError: {ex.Message}\n=====================================================================================================\n\n");
                        }
                    }

                    File.AppendAllText(logFilePath, "Execution completed. Check the log file for details.\n\n");
                }
            }
            catch (Exception ex)
            {
                // Log overall connection or processing failure
                File.AppendAllText(logFilePath, $"Execution failed with an error: {ex.Message}\n\n");
                Console.WriteLine($"Execution failed: {ex.Message}");
            }
        }
    }
}

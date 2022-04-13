An example of using two different data providers in one method.

```csharp
/// <summary>
/// Perform a manual transfer of Oracle data to SQL-Server
/// 
/// * Normally when working with one data provider the connection object name is cn and the command cmd while
///   working with two different data providers we need clarity thus cn for Oracle becomes oracleConnection
///   and SQL-Server becomes sqlServerConnection, same goes for commands e.g.
///   oracleCommand and sqlServerCommand
/// </summary>
public static void HelpMessageFromOracleToSqlServer()
{
    bool success = true;
    
    using var oracleConnection = new OracleConnection() {ConnectionString = ApplicationSettings.Instance.OracleDevelopmentConnectionString};
    
    using var oracleCommand = new OracleCommand
    {
        Connection = oracleConnection, 
        CommandText = ApplicationSettings.Instance.OracleHelpMessageTableSelectStatement
    };

    oracleConnection.Open();

    _helpMessageTableRowCount = 0;
    var oracleDataReader = oracleCommand.ExecuteReader();
    while (oracleDataReader.Read())
    {
        _helpMessageTableRowCount++;
    }
    
    oracleDataReader.Close();

    var reader = oracleCommand.ExecuteReader();

    using var sqlServerConnection = new SqlConnection(ApplicationSettings.Instance.SqlServerOcsConnectionString);
    using var sqlServerCommand = new SqlCommand
    {
        Connection = sqlServerConnection, 
        CommandText = ApplicationSettings.Instance.SqlServerTruncateHelpMessagesTable
    };
    
    sqlServerConnection.Open();

    sqlServerCommand.ExecuteNonQuery();
    sqlServerCommand.CommandText = ApplicationSettings.Instance.SqlServerInsertIntoHelpMessages;

    sqlServerCommand.Parameters.Add("@DescriptionEnglish", SqlDbType.NVarChar);
    sqlServerCommand.Parameters.Add("@BodyEnglish", SqlDbType.NVarChar);
    sqlServerCommand.Parameters.Add("@LanguageCode", SqlDbType.NVarChar);
    sqlServerCommand.Parameters.Add("@DescriptionSpanish", SqlDbType.NVarChar);
    sqlServerCommand.Parameters.Add("@BodySpanish", SqlDbType.NVarChar);


    int currentIndex = 0;
    
    while (reader.Read())
    {
        sqlServerCommand.Parameters["@DescriptionEnglish"].Value = reader.GetString(1);
        sqlServerCommand.Parameters["@BodyEnglish"].Value = reader.GetString(2);
        sqlServerCommand.Parameters["@LanguageCode"].Value = reader.GetString(3);
        sqlServerCommand.Parameters["@DescriptionSpanish"].Value = reader.SafeGetString(4);
        sqlServerCommand.Parameters["@BodySpanish"].Value = reader.SafeGetString(5);

        try
        {
            var primaryKey = (int) sqlServerCommand.ExecuteScalar();


            Progress?.Invoke(currentIndex.PercentageOf(_helpMessageTableRowCount));

            currentIndex++;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            success = false;
        }

    }

    if (success)
    {
        Progress?.Invoke(100);
    }
    
    
}

```
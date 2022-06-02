using System;
using System.Data;
using System.Data.OleDb;

namespace JCCoreLib3.DB
{
  public class OledbClient
  {
    private OledbClient()
    {
    }

    public static string GetConnStringOleMSAccess(
      string DataSource,
      string UserId = "admin",
      string Password = "",
      string Provider = "Microsoft.Jet.OLEDB.4.0")
    {
      return string.Format("Provider={0};Data Source={1};User Id={2};Password={3}", (object) Provider, (object) DataSource, (object) UserId, (object) Password);
    }

    public static string GetConnStringOleSQLExpress(
      string Database,
      string Server = "(local)\\SQLEXPRESS",
      string IntegratedSecurity = "SSPI",
      string Provider = "sqloledb")
    {
      return string.Format("Provider={0};server={1};database={2};Integrated Security={3}", (object) Provider, (object) Server, (object) Database, (object) IntegratedSecurity);
    }

    public static OleDbDataReader ExecuteReader(
      string commandText,
      OleDbConnection connection,
      CommandType commandType,
      params OleDbParameter[] parameters)
    {
      if (connection == null)
        throw new Exception("No Connection. Please establish connection before running query.");
      using (OleDbCommand oleDbCommand = OledbClient.BuildCommand(commandText, connection, commandType, parameters))
      {
        if (connection.State == ConnectionState.Closed)
          connection.Open();
        return oleDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
      }
    }

    public static void ExecuteNonQuery(string strSQL, string ConnString)
    {
      using (OleDbConnection connection = new OleDbConnection(ConnString))
      {
        using (OleDbCommand oleDbCommand = new OleDbCommand(strSQL, connection))
        {
          oleDbCommand.CommandText = strSQL;
          try
          {
            connection.Open();
            oleDbCommand.ExecuteNonQuery();
          }
          catch (Exception ex)
          {
            Console.WriteLine("************** ERROR *********");
            Console.WriteLine(ex.Message);
            throw;
          }
          finally
          {
            if (connection.State != ConnectionState.Closed)
              connection.Close();
          }
        }
      }
    }

    public static DataTable ExecuteDataTable(string commandText, string strConn)
    {
      DataTable dataTable = new DataTable();
      using (OleDbConnection connection = new OleDbConnection(strConn))
      {
        using (OleDbCommand oleDbCommand = new OleDbCommand(commandText, connection))
        {
          oleDbCommand.CommandText = commandText;
          try
          {
            connection.Open();
            dataTable.Load((IDataReader) oleDbCommand.ExecuteReader(CommandBehavior.CloseConnection));
          }
          catch (Exception ex)
          {
            Console.WriteLine("************** ERROR *********");
            Console.WriteLine(ex.Message);
          }
          finally
          {
            if (connection.State != ConnectionState.Closed)
              connection.Close();
          }
        }
      }
      return dataTable;
    }

    public static void PrintDataTable(DataTable table)
    {
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        foreach (DataColumn column in (InternalDataCollectionBase) table.Columns)
          Console.Write("{0}\t", row[column]);
        Console.WriteLine();
      }
    }

    private static OleDbCommand BuildCommand(
      string commandText,
      OleDbConnection connection,
      CommandType commandType,
      params OleDbParameter[] parameters)
    {
      OleDbCommand oleDbCommand = new OleDbCommand(commandText, connection);
      oleDbCommand.CommandType = commandType;
      if (parameters != null)
      {
        foreach (OleDbParameter parameter in parameters)
          oleDbCommand.Parameters.Add(parameter);
      }
      return oleDbCommand;
    }
  }
}

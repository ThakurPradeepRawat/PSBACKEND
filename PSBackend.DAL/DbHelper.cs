using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
  public class SqlDataClient
  {
    private readonly string _connectionString;

    public class MultiResult<T1, T2>
    {
      public T1 Table1 { get; set; }

      public List<T2> Table2 { get; set; }
    }

    public SqlDataClient(IConfiguration configuration)
    {
      _connectionString =
          configuration.GetConnectionString("DefaultConnection");
    }

    // =========================================
    // SINGLE TABLE
    // =========================================
    public TOutput SingleModel<TInput, TOutput>(
        string spName,
        TInput inputModel)
    {
      using var connection =
          new SqlConnection(_connectionString);

      return connection.QueryFirstOrDefault<TOutput>(
          spName,
          inputModel,
          commandType: CommandType.StoredProcedure
      );
    }

    // =========================================
    // LIST TABLE
    // =========================================
    public List<TOutput> ListModel<TInput, TOutput>(
        string spName,
        TInput inputModel)
    {
      using var connection =
          new SqlConnection(_connectionString);

      var result = connection.Query<TOutput>(
          spName,
          inputModel,
          commandType: CommandType.StoredProcedure
      );

      return result.AsList();
    }

    // =========================================
    // MULTIPLE TABLES
    // =========================================
    public MultiResult<T1, T2>
    MultiModel<TInput, T1, T2>(
        string spName,
        TInput inputModel)
    {
      using var connection =
          new SqlConnection(_connectionString);

      using var multi =
          connection.QueryMultiple(
              spName,
              inputModel,
              commandType: CommandType.StoredProcedure
          );

      var result = new MultiResult<T1, T2>
      {
        Table1 =
              multi.ReadFirstOrDefault<T1>(),

        Table2 =
              multi.Read<T2>().AsList()
      };

      return result;
    }
  }
}

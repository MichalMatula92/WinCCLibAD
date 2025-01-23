using System.Collections.Generic;

namespace SqlServer
{
    public interface ISqlDataAccess
    {
        int Execute(string sql, object parameters = null);
        T ExecuteScalar<T>(string sql, object parameters = null);
        List<T> Query<T>(string sql, object parameters = null);
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRepository
{
    public interface IDBManager
    {
        DataSet ExecuteCommand(string query, string connString);

        void ExecuteQuery(string query, string connString, List<KeyValuePair<string, KeyValuePair<SqlDbType, object>>> inOutparams);

        string ExecuteStoredProcedureQuery(string storedProcedureName, string connString, List<KeyValuePair<string, KeyValuePair<SqlDbType, object>>> inOutparams);
        int ExecuteScalar(string query, string connString);
    }
}

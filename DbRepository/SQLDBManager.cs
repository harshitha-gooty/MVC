using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRepository
{
    public class SQLDBManager : IDBManager
    {
        public DataSet ExecuteCommand(string query, string connString)
        {
            DataSet ds = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                SqlDataAdapter ada = new SqlDataAdapter(query, conn);
                ds = new DataSet();
                ada.Fill(ds);
            }
            return ds;
        }
        public void ExecuteQuery(string query, string connString, List<KeyValuePair<string, KeyValuePair<SqlDbType, object>>> inOutparams)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connString;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (inOutparams.Count() > 0)
                    {
                        foreach (var param in inOutparams)
                        {
                            cmd.Parameters.Add("@" + param.Key, param.Value.Key).Value = param.Value.Value;
                        }
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public string ExecuteStoredProcedureQuery(string storedProcedureName, string connString, List<KeyValuePair<string, KeyValuePair<SqlDbType, object>>> inOutparams)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connString;
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (inOutparams.Count() > 0)
                    {
                        foreach (var param in inOutparams)
                        {
                            cmd.Parameters.Add("@" + param.Key, param.Value.Key).Value = param.Value.Value;
                        }
                    }
                    cmd.Parameters.Add("@Output", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@Output"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    var result = cmd.Parameters["@Output"].Value.ToString();
                    return result;
                }
            }

        }

        public int ExecuteScalar(string query, string connString)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connString;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;

        }
    }
}

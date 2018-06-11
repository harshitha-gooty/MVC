using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRepository
{
    public class DbFamilyConstants
    {
        public const string MSSQL = "MSSQL";
        public const string MongoDB = "MongoDB";
    }
    public class DBFamily : IDBFamily
    {
        public IDBManager GetQueryBuilder(string dbFamily)
        {
            IDBManager queryBuilder = null;
            switch (dbFamily)
            {
                case DbFamilyConstants.MSSQL:
                    queryBuilder = new SQLDBManager();
                    break;
            }
            return queryBuilder;
        }
    }
}

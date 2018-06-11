using DbRepository;
using Models.CustomAccount;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class CustomAccountController : ApiController
    {
        [HttpPost]
        [Route("api/CustomAccount/Register/")]
        public void Register([FromBody] Register details)
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLConString"].ToString();
            IDBFamily family = new DBFamily();
            var dbFamilyResult = family.GetQueryBuilder(DbFamilyConstants.MSSQL);
            List<KeyValuePair<string, KeyValuePair<SqlDbType, object>>> inOutParams = new List<KeyValuePair<string, KeyValuePair<SqlDbType, object>>>();
            inOutParams.Add(new KeyValuePair<string, KeyValuePair<SqlDbType, object>>("UserName", new KeyValuePair<SqlDbType, object>(SqlDbType.NVarChar, details.FirstName + "." + details.LastName)));
            inOutParams.Add(new KeyValuePair<string, KeyValuePair<SqlDbType, object>>("Password", new KeyValuePair<SqlDbType, object>(SqlDbType.NVarChar, details.Password)));
            inOutParams.Add(new KeyValuePair<string, KeyValuePair<SqlDbType, object>>("EmailId", new KeyValuePair<SqlDbType, object>(SqlDbType.NVarChar, details.EmailId)));
            inOutParams.Add(new KeyValuePair<string, KeyValuePair<SqlDbType, object>>("FirstName", new KeyValuePair<SqlDbType, object>(SqlDbType.NVarChar, details.FirstName)));
            inOutParams.Add(new KeyValuePair<string, KeyValuePair<SqlDbType, object>>("LastName", new KeyValuePair<SqlDbType, object>(SqlDbType.NVarChar, details.LastName)));
            inOutParams.Add(new KeyValuePair<string, KeyValuePair<SqlDbType, object>>("IsActive", new KeyValuePair<SqlDbType, object>(SqlDbType.Bit, 1)));
            dbFamilyResult.ExecuteQuery("insert into NewUsers values(@UserName,@Password,@EmailId,@FirstName,@LastName,@IsActive)", connString, inOutParams);

        }

        [HttpPost]
        [Route("api/CustomAccount/Login")]
        public string Login([FromBody] Login details)
        {
            string returnValue = "";
            string connString = ConfigurationManager.ConnectionStrings["SQLConString"].ToString();
            IDBFamily family = new DBFamily();
            var dbFamilyResult = family.GetQueryBuilder(DbFamilyConstants.MSSQL);
            var checkIfUsersExistsQuery = string.Format("select count(1) from NewUsers where UserName='{0}'", details.UserName);
            int count = dbFamilyResult.ExecuteScalar(checkIfUsersExistsQuery, connString);
            if (count > 0)
            {
                var query = string.Format("select count(1) from NewUsers where UserName='{0}' and Password='{1}'", details.UserName, details.Password);
                int result = dbFamilyResult.ExecuteScalar(query, connString);
                if (result > 0)
                    returnValue = "Success";
                else
                    returnValue = "Invalid password";
            }
            else
                returnValue = "User doesnt exists";
            return returnValue;
        }
    }
}

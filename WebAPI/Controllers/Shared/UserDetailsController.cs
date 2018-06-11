using DbRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers.Shared
{
    public class UserDetailsController : ApiController
    {
        [HttpGet]
        [Route("api/UserDetails/GetUserDetails/")]
        public List<UserDetails> GetUserDetails()
        {
            string conn = ConfigurationManager.ConnectionStrings["SQLConString"].ConnectionString;
            List<UserDetails> result = new List<UserDetails>();
            IDBFamily dbFamily = new DBFamily();
            var dbFamilyResult = dbFamily.GetQueryBuilder(DbFamilyConstants.MSSQL);
            var dataSet = dbFamilyResult.ExecuteCommand("select * from UserDetails", conn);
            if (dataSet != null && dataSet.Tables != null)
            {
                var dt = dataSet.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    result = dt.DataTableToList<UserDetails>();
                }
            }
            return result;
        }
    }
}

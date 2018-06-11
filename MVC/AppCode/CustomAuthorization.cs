using DbRepository;
using Models.CustomAccount;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Data;
using System.Reflection;

namespace MVC.AppCode
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;
        IDBFamily dbFamily;
        IDBManager dbFamilyResult;
        string connString;
        public CustomAuthorization(params string[] roles)
        {
            connString = ConfigurationManager.ConnectionStrings["SQLConString"].ToString();
            dbFamily = new DBFamily();
            dbFamilyResult = dbFamily.GetQueryBuilder(DbFamilyConstants.MSSQL);
            this.allowedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorize = false;
            List<Register> listOfUsers = new List<Register>();
            List<Roles> listOfRoles = new List<Roles>();
            //get current user
            string currentUser = System.Web.HttpContext.Current.Session["CurrentUser"].ToString();
            var users = dbFamilyResult.ExecuteCommand(string.Format("select * from NewUsers where UserName='{0}'", currentUser), connString);
            if (users.Tables.Count > 0)
            {
                var dt = users.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    listOfUsers = dt.DataTableToList<Register>();
                    var userRoles = dbFamilyResult.ExecuteCommand(string.Format("select RoleId from UserRoleMapping where UserID={0}", listOfUsers.FirstOrDefault().UserId), connString);
                    if (userRoles.Tables.Count > 0)
                    {
                        var dtUserRoles = userRoles.Tables[0];
                        if (dtUserRoles.Rows.Count > 0)
                        {
                            var listOfUserRoles = dtUserRoles.DataTableToList<UserRoleMapping>();
                            var roles = dbFamilyResult.ExecuteCommand(string.Format("select RoleName from Roles where RoleId={0}", listOfUserRoles.FirstOrDefault().RoleId), connString);
                            if (roles.Tables.Count > 0)
                            {
                                var dtRoles = roles.Tables[0];
                                if (dtRoles.Rows.Count > 0)
                                {
                                    listOfRoles = dtRoles.DataTableToList<Roles>();
                                    System.Web.HttpContext.Current.Session["currentRole"] = listOfRoles.First().RoleName;
                                    if (allowedRoles.Any(m => m == listOfRoles.First().RoleName))
                                    {
                                        isAuthorize = true;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return isAuthorize;

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();

        }

    }

    public static class ExtensionMethods
    {
        public static List<T> DataTableToList<T>(this System.Data.DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {

                    T obj = new T();
                    PropertyInfo[] PropertyInfocolleciton = obj.GetType().GetProperties();
                    foreach (var prop in PropertyInfocolleciton)
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (row.Table.Columns.Contains(prop.Name))
                            {
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
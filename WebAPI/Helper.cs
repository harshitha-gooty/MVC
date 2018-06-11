using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace WebAPI
{
    public class Helper
    {
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
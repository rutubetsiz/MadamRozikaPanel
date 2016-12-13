using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;


public enum DatabaseType { DBType1 }
public class Execute
{
    private DatabaseType DatabaseType;
    private List<SqlParameter> _Parameters = new List<SqlParameter>();
    public List<SqlParameter> Parameters { get { return _Parameters; } set { _Parameters = value; } }

    public Execute(DatabaseType DatabaseType)
    {
        this.DatabaseType = DatabaseType;
    }
    /// <summary>
    /// Gönderilen Sql Sorgusunu çalıştırıp istenilen türde (T) geri döndürür.
    /// </summary>
    /// <typeparam name="T">Geri Dönüş Tipi; NewsList, List NewsList>, DbNull, int, string, boolen, DateTime, Byte, Char, Array(int[], string[], boolen[],DataTime[]...)...</typeparam>
    /// <param name="SQLQuery">Çalıştırılacak Sql Sorgusu</param>
    /// <param name="CacheInSecond">Saniye cinsinden Cache Süresi, 0 gönderilirse Cache'e atmaz, Default değeri 0</param>
    /// <param name="cmdType">CommandType enum</param>
    /// <returns>T Türünü döndürür</returns>
    public T ExecuteQuery<T>(string SQLQuery, int CacheInSecond = 0, CommandType cmdType = CommandType.Text)
    {
        string CacheName = MemoryCache.GetMemoryCacheName(SQLQuery + "; " + cmdType + "; " + typeof(T), Parameters);
        if (CacheInSecond > 0 && MemoryCache.IsInMemoryCache(CacheName))
            return ((T)MemoryCache.GetMemoryCache(CacheName));
        else
        {
            if (HttpContext.Current.Request.QueryString["DatacacheControl"] != null && HttpContext.Current.Request.QueryString["DatacacheControl"].ToString() == "1")
                HttpContext.Current.Response.Write(SQLQuery);//CACHE OLMAYAN YER VAR MI KONTROLÜ
            SqlCommand cmd = new SqlCommand();
            SqlConnection dbConn = new SqlConnection(GetSqlConnectionString(DatabaseType));
            cmd.Connection = dbConn;
            cmd.CommandType = cmdType;
            cmd.CommandText = SQLQuery;
            if (Parameters != null)
                cmd.Parameters.AddRange(Parameters.ToArray());
            T ReturnObject = default(T);

            if (dbConn.State != ConnectionState.Open) dbConn.Open();
            #region FillReturnObject
            #region T>DBNULL
            if (typeof(T) == typeof(DBNull))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch 
                {
                    string dsds = "ds";
                    
                }
            }
            #endregion T>NULL
            #region T>NameSpace: "System"
            else if (typeof(T).Namespace == "System")
            {
                if (typeof(T).IsArray)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    ReturnObject = dr.Ext_MappingToArray<T>();
                }
                else
                {
                    object ObjectReturn = cmd.ExecuteScalar();
                    if (ObjectReturn != null)
                        ReturnObject = (T)Convert.ChangeType(ObjectReturn, typeof(T));
                }
            }
            #endregion T>NameSpace: "System"
            #region T>Type:DataSet
            else if (typeof(T) == typeof(DataSet))
            {
                using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                {
                    DataSet DS = new DataSet();
                    DA.Fill(DS);
                    ReturnObject = (T)Convert.ChangeType(DS, typeof(T));
                }
            }
            #endregion T>Type:DataSet
            #region T>Type:DataTable
            else if (typeof(T) == typeof(DataTable))
            {
                using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                {
                    DataTable DT = new DataTable();
                    DA.Fill(DT);
                    ReturnObject = (T)Convert.ChangeType(DT, typeof(T));
                }
            }
            #endregion T>Type:DataTable
            #region T>Type:DataRow
            else if (typeof(T) == typeof(DataRow))
            {
                using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                {
                    DataTable DT = new DataTable();
                    DA.Fill(DT);
                    if (DT.Rows.Count > 0)
                        ReturnObject = (T)Convert.ChangeType(DT.Rows[0], typeof(T));
                }
            }
            #endregion T>Type:DataRow
            #region T>Type:DataColumn
            else if (typeof(T) == typeof(DataColumn))
            {
                using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                {
                    DataTable DT = new DataTable();
                    DA.Fill(DT);
                    if (DT.Rows.Count > 0)
                        ReturnObject = (T)Convert.ChangeType(DT.Rows[0][0], typeof(T));
                }
            }
            #endregion T>Type:DataColumn
            #region T>Type:EntityObject
            else if (typeof(T).IsClass)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                ReturnObject = dr.Ext_MappingToEntityObject<T>();
            }
            #endregion T>Type:EntityObject
            #endregion FillReturnObject
            dbConn.Close();
            cmd.Parameters.Clear();
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            cmd.Dispose();
            dbConn.Dispose();
            if (CacheInSecond > 0)
                MemoryCache.AddMemoryCache(ReturnObject, CacheName, CacheInSecond);
            return ReturnObject;

        }
    }
    /// <summary>
    /// Gönderilen Sql Sorgusunu çalıştır, geri herhangi bir değer döndürmez.
    /// </summary>
    /// <param name="SQLQuery">Çalıştırılacak Sql Sorgusu</param>
    /// <param name="CacheInSecond">Saniye cinsinden Cache Süresi, 0 gönderilirse Cache'e atmaz, Default değeri 0</param>
    /// <param name="cmdType">CommandType enum</param>
    public void ExecuteQuery(string SQLQuery, int CacheInSecond = 0, CommandType cmdType = CommandType.Text)
    {
        ExecuteQuery<DBNull>(SQLQuery, CacheInSecond, cmdType);
    }
    private static string GetSqlConnectionString(DatabaseType DatabaseType)
    {
        return ConfigurationManager.ConnectionStrings[DatabaseType.ToSafeString()].ToSafeString();
        //switch (DatabaseType)
        //{

        //    case DatabaseType.MedyaDb: return "SERVER=medyadb.haberler.com;DATABASE=medya;UID=medya;PWD=medya723745324;";
        //    case DatabaseType.SecimDb: return "";
        //    case DatabaseType.HastaneDb: return "";
        //    case DatabaseType.ErrorDb: return "SERVER=188.132.198.237;DATABASE=Web_Errors;UID=web_errors;PWD=errors;";
        //    case DatabaseType.DestekDb: return "SERVER=188.132.198.237;DATABASE=Web_Destek;UID=webdestek;PWD=wd32409867632699;";
        //    default: return "";
        //}
    }
}
public static class ExecuteExtensionMethods
{
    public static Execute AddParameter(this Execute Ex, string ParameterName, object ParameterValue, ParameterDirection Direction = ParameterDirection.Input)
    {
        SqlParameter P = new SqlParameter(ParameterName, ParameterValue);
        P.Direction = Direction;
        Ex.Parameters.Add(P);
        return Ex;
    }
    public static Execute AddParameter(this Execute Ex, List<SqlParameter> ParameterList)
    {
        foreach (SqlParameter item in ParameterList)
        {
            Ex.Parameters.Add(item);
        }
        return Ex;
    }
    public static Execute AddParameter(this Execute Ex, SqlParameter Parameter)
    {
        Ex.Parameters.Add(Parameter);
        return Ex;
    }

    public static void RemoveParameters(this Execute Ex)
    {
        Ex.Parameters = new List<SqlParameter>();
    }
    public static T GetParameterValue<T>(this Execute Ex, string ParameterName)
    {
        foreach (var item in Ex.Parameters)
        {
            if (item.ParameterName == ParameterName)
                return (T)Convert.ChangeType(item.Value, typeof(T));
        }
        return default(T);
    }
    #region DataReader Extensions
    /// <summary>
    /// DataReader içeriğini istenilen tipteki objeye doldurmak için kullanılır
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dr"></param>
    /// <returns></returns>
    public static T Ext_MappingToEntityObject<T>(this IDataReader dr)
    {
        T ReturObject = Activator.CreateInstance<T>();
        if (ReturObject.GetType().IsGenericType && ReturObject is IEnumerable)//Generic List Item
        {
            Type ListItemType = ReturObject.GetType().GetProperty("Item").PropertyType;
            var ReturObjectList = (IList)Activator.CreateInstance<T>();
            while (dr.Read())
            {
                var ObjectItem = Activator.CreateInstance(ListItemType);
                FillEntityObjectItem(ObjectItem, ref dr);
                ReturObjectList.Add(ObjectItem);
            }
            ReturObject = (T)Convert.ChangeType(ReturObjectList, typeof(T));
        }
        else
        {
            if (dr.Read())
            {
                FillEntityObjectItem(ReturObject, ref dr);
            }
        }

        return ReturObject;
    }
    /// <summary>
    /// DataReader içeriğini istenilen tipte doldurmak için kullanılır
    /// </summary>
    /// <param name="ReturObject"></param>
    /// <param name="dr"></param>
    private static void FillEntityObjectItem(Object ReturObject, ref IDataReader dr)
    {
        foreach (PropertyInfo prop in ReturObject.GetType().GetProperties())
        {
            foreach (DatabaseField Field in prop.GetCustomAttributes(typeof(DatabaseField)))
            {
                if (Field != null)
                {
                    if (dr.Ext_HasColumn(Field.Name))// If DataReader Has This Column Name 
                        if (!object.Equals(dr[Field.Name], DBNull.Value))// Column Is Not Null
                            if (prop.PropertyType.IsArray)//Object Property Is Array
                            {
                                ArrayList ReturnArrayObj = new ArrayList();
                                if (!string.IsNullOrWhiteSpace(dr[Field.Name].ToString()))
                                {
                                    foreach (string ArrayItem in dr[Field.Name].ToString().Split(','))
                                    {
                                        ReturnArrayObj.Add(Convert.ChangeType(ArrayItem, prop.PropertyType.GetElementType()));
                                    }
                                }
                                prop.SetValue(ReturObject, ReturnArrayObj.ToArray(prop.PropertyType.GetElementType()), null);

                            }
                            else //Object Property Is Not Array
                            {
                                if (prop.PropertyType.IsEnum)//if proppert is an enum try to cast
                                {
                                    var EnumValue = (Enum.Parse(prop.PropertyType, dr[Field.Name].ToString(), true));
                                    prop.SetValue(ReturObject, EnumValue, null);
                                }
                                else
                                {
                                    prop.SetValue(ReturObject, Convert.ChangeType(dr[Field.Name], prop.PropertyType), null);
                                }
                            }
                }
                ////
            }
        }
    }
    /// <summary>
    /// DataReader içeriğini istenilen tipteki Array'e doldurmak için kullanılır
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dr"></param>
    /// <returns></returns>
    public static T Ext_MappingToArray<T>(this IDataReader dr)
    {
        Type ReturnArrayType = typeof(T).GetElementType();
        ArrayList ReturnObj = new ArrayList();
        while (dr.Read())
        {
            ReturnObj.Add(Convert.ChangeType(dr[0], ReturnArrayType));
        }
        return (T)Convert.ChangeType(ReturnObj.ToArray(ReturnArrayType), typeof(T));
    }
    /// <summary>
    /// DataReader içerisinde ilgili alan var mı?
    /// </summary>
    /// <param name="dr"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static bool Ext_HasColumn(this IDataRecord dr, string columnName)
    {
        for (int i = 0; i < dr.FieldCount; i++)
        {
            if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                return true;
        }
        return false;
    }
    #endregion DataReader Extensions
}


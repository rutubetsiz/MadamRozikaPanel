using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


    public class MemoryCache
    {
        public static string GetMemoryCacheName(string Sql, List<SqlParameter> Parameters)
        {
            string CacheNameExtra = string.Empty;
            if (Parameters != null)
                foreach (SqlParameter Parameter in Parameters)
                {
                    CacheNameExtra += "; " + Parameter.ParameterName + "=" + Parameter.Value + ">" + Parameter.Direction;
                }
            return Sql + CacheNameExtra;
        }
        public static bool IsInMemoryCache(string MemoryCacheName)
        {
            return HttpContext.Current.Cache[MemoryCacheName] != null ? true : false;
        }
        public static void AddMemoryCache(object CachingObject, string MemoryCacheName, int CacheInSecond)
        {
            HttpContext.Current.Cache.Add(MemoryCacheName, CachingObject, null, DateTime.Now.AddSeconds(CacheInSecond), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        }
        public static object GetMemoryCache(string MemoryCacheName)
        {
            return HttpContext.Current.Cache[MemoryCacheName];
        }
    }


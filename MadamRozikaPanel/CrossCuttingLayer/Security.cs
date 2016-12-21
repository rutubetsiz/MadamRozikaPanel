using System;
using System.Web;

namespace MadamRozikaPanel.CrossCuttingLayer
{
    public class Security
    {
        public static string GetClientIP()
        {
            string clientIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                clientIp = clientIp + " " + HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]))
                    clientIp += " " + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
                //if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.ServerVariables["REMOTE_PORT"]))
                //    clientIp += "-" + HttpContext.Current.Request.ServerVariables["REMOTE_PORT"].ToString();
            }
            return clientIp.Substring(0, Math.Min(clientIp.Length, 90));
        }
    }
}


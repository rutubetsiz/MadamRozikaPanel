using System;
using System.Collections.Generic;
using System.Web;

namespace MadamRozikaPanel.CrossCuttingLayer
{
    public static class ExtensionMethods
    {

        public static string Ext_SubJoin<T>(this T[] List, string Seperator, int Index)
        {
            if (List != null)
            {
                List<string> Str = new List<string>();
                foreach (var item in List)
                {
                    Str.Add(item.ToSafeString().Split('|')[Index]);
                }
                return string.Join(Seperator, Str);
            }
            else return "";
        }
        public static string Ext_Join<T>(this T[] List, string Seperator)
        {
            if (List != null)
                return string.Join(Seperator, List);
            else
                return "";
        }
        #region List<T> Extensions
        public static List<T> Ext_GetRange<T>(this List<T> list, int CurrentPage, int PageSize)
        {
            int TotalPage = (int)Math.Ceiling(((double)list.Count / (double)PageSize));
            CurrentPage = TotalPage < CurrentPage ? TotalPage : CurrentPage;
            int Start = Math.Max(0, Math.Min(list.Count - 1, (CurrentPage - 1) * PageSize));
            return list.GetRange(Start, Math.Min(PageSize, list.Count - 1 - Start));
        }
        #endregion List<T> Extensions
        #region HttpRequest Extensions
        public static void QueryString(this HttpRequest HttpRequest, string QueryStringName, ref string Result)
        {
            if (HttpRequest.QueryString[QueryStringName] != null)
                Result = HttpRequest.QueryString[QueryStringName].ToString();
        }
        public static void QueryString(this HttpRequest HttpRequest, string QueryStringName, ref int Result)
        {
            if (HttpRequest.QueryString[QueryStringName] != null)
                Result = Convert.ToInt32(HttpRequest.QueryString[QueryStringName]);
        }
        #endregion HttpRequest Extensions
        #region Object Extensions
        public static string ToSafeString(this object obj)
        {
            return (obj ?? string.Empty).ToString();
        }
        #endregion Object Extensions
        #region DateTime Extensions
        public static string Ext_ToTimeAgoString(this object _DateTime)
        {
            DateTime _DT = new DateTime();
            bool IsDate = DateTime.TryParse(_DateTime.ToString(), out _DT);
            if (IsDate)
            {
                TimeSpan span = DateTime.Now - _DT;
                if (span.Days > 365)
                {
                    int years = (span.Days / 365);
                    if (span.Days % 365 != 0)
                        years += 1;
                    return String.Format("{0} yıl önce", years);
                }
                if (span.Days > 30)
                {
                    int months = (span.Days / 30);
                    if (span.Days % 31 != 0)
                        months += 1;
                    return String.Format("{0} ay önce", months);
                }
                if (span.Days > 0)
                    return String.Format("{0} gün önce", span.Days);
                if (span.Hours > 0)
                    return String.Format("{0} saat önce", span.Hours);
                if (span.Minutes > 0)
                    return String.Format("{0} dakika önce", span.Minutes);
                if (span.Seconds > 5)
                    return String.Format("{0} saniye önce", span.Seconds);
                if (span.Seconds <= 5) return "az önce";
                return "";
            }
            else
                return "";
        }
        public static string Ext_ToTimeString(this object _DateTime)
        {
            DateTime _DT = new DateTime();
            bool IsDate = DateTime.TryParse(_DateTime.ToString(), out _DT);
            if (IsDate)
                return _DT.ToString("HH:mm");
            else
                return "";
        }
        public static string Ext_ToDateString(this object _DateTime, string Format = "dddd.MM.yyyy")
        {
            DateTime _DT = new DateTime();
            bool IsDate = DateTime.TryParse(_DateTime.ToString(), out _DT);
            if (IsDate)
                return _DT.ToString(Format);
            else
                return "";
        }
        public static string Ext_ToISO8601DateTimeString(this object _DateTime)
        {
            DateTime _DT = new DateTime();
            bool IsDate = DateTime.TryParse(_DateTime.ToString(), out _DT);
            if (IsDate)
                return string.Concat(_DT.ToString("s"), "+02:00"); // String.Format("{0:s}", _DT);
            else
                return "";
        }
        #endregion DateTime Extensions
        #region String Extensions
        public static string Ext_TextSmash(this string Value)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                string Result = Value;
                Result = Result
                    .Trim()
                    .Replace(",", "")
                    .Replace("'", "")
                    .Replace("\"", " ")
                    .Replace(":", "")
                    .Replace("--", "-");
                Result = Result.Replace(" ", ", ");
                return Result;
            }
            else
                return "";
        }
        
        /// <summary>
        /// String ifadenin uzunluğunu MaxLength kadar yapar.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="MaxLength"></param>
        /// <returns></returns>
        public static string Ext_Truncate(this string Value, int MaxLength)
        {
            string _kesilmismetin = string.Empty;
            if (Value.Length > MaxLength)
            {
                if (Value.Substring(MaxLength, 1) == " ")
                {
                    _kesilmismetin = Value.Substring(0, MaxLength) + "..";
                }
                else
                {
                    for (int i = MaxLength; i >= 0; i--)
                    {
                        if (Value.Substring(i, 1) == " ")
                        {
                            _kesilmismetin = Value.Substring(0, i) + ".."; break;
                        }
                        else
                            _kesilmismetin = " ";
                    }
                }
            }
            else
                _kesilmismetin = Value;
            return _kesilmismetin;
        }
        public static string Ext_Right(this string Value, int Length)
        {
            if (String.IsNullOrEmpty(Value)) return string.Empty;

            return Value.Length <= Length ? Value : Value.Substring(Value.Length - Length);
        }
        //#region Objects Property Extensions
        //public static string Ext_RenderNewsURL(this string OriginalURL) {
        //    switch (Configuration.SelectedSite)
        //    {
        //        case Configuration.Sites.HaberlerCom:
        //            return OriginalURL + "-HaberlerCom";
        //        case Configuration.Sites.SondakikaCom:
        //            return OriginalURL + "-SondakikaCom";
        //        default:
        //            return OriginalURL;
        //    }
        //}
        //public static string Ext_RenderNewsText(this string OriginalText)
        //{
        //    switch (Configuration.SelectedSite)
        //    {
        //        case Configuration.Sites.HaberlerCom:
        //            return OriginalText;
        //        case Configuration.Sites.SondakikaCom:
        //            return OriginalText;
        //        default:
        //            return OriginalText;
        //    }
        //}
        //public static string Ext_RenderNewsVideo(this string OriginalVideo)
        //{
        //    switch (Configuration.SelectedSite)
        //    {
        //        case Configuration.Sites.HaberlerCom:
        //            return OriginalVideo;
        //        case Configuration.Sites.SondakikaCom:
        //            return OriginalVideo;
        //        default:
        //            return OriginalVideo;
        //    }
        //}
        //#endregion Objects Property Extensions
        
        #endregion String Extensions
    }
}


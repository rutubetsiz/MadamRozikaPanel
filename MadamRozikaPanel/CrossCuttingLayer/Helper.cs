using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MadamRozikaPanel.CrossCuttingLayer
{
    public class Helper
    {
        public static string CategoryEnglish(int id)
        {
            switch (id)
            {
                case 1000:
                    return "Politics";
                case 1007:
                    return "Politics";
                case 1010:
                    return "Politics";
                case 1012:
                    return "Politics";
                case 1016:
                    return "Politics";
                case 1017:
                    return "Politics";
                case 1024:
                    return "Politics";
                case 1025:
                    return "Politics";
                case 1027:
                    return "Politics";
                case 1029:
                    return "Politics";
                case 1006:
                    return "Lifestyle";
                case 1023:
                    return "Technology";
                case 1030:
                    return "Companies";
                case 1031:
                    return "Education";
                case 1001:
                    return "Economy";
                case 1008:
                    return "Economy";
                case 1019:
                    return "Economy";
                case 1026:
                    return "Economy";
                case 1011:
                    return "Business";
                case 1015:
                    return "Business";
                case 1002:
                    return "World";
                case 1018:
                    return "World";
                case 1003:
                    return "Sports";
                case 1020:
                    return "Sports";
                case 1021:
                    return "Sports";
                case 1005:
                    return "Culture Arts";
                case 1009:
                    return "Health";
                case 1013:
                    return "Magazine";
                default:
                    return "News";
            }
        }

        public static void SetPageCache(int PageCacheInSecond)
        {
            HttpContext.Current.Response.Cache.SetMaxAge(new TimeSpan(0, 0, PageCacheInSecond));
            HttpContext.Current.Response.CacheControl = "Public";
            HttpContext.Current.Response.Cache.VaryByParams.IgnoreParams = true;
            HttpContext.Current.Response.Cache.SetSlidingExpiration(true);
        }

        public static string GetIp()
        {
            string ipAddress = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            else
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (ipAddress.Contains(","))
                ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(","));
            if (ipAddress.Contains(":"))
                ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(":"));

            return ipAddress;
        }

        public static double IPAddressToNumber(string IPaddress)
        {
            string[] arrDec;
            double num = 0;
            if (IPaddress == "")
            {
                return 0;
            }
            else
            {
                arrDec = IPaddress.Split('.');
                for (int i = arrDec.Length - 1; i >= 0; i = i - 1)
                {
                    num += ((int.Parse(arrDec[i])%256)*Math.Pow(256, (3 - i)));
                }
                return num;
            }
        }

        public static string CreateCaptchaImage(string Kod, int H, int W, string fonts, int Punto, int X, int Y,
            string BackgroundImage)
        {
            Bitmap bmp = new Bitmap(H, W);
            Graphics g = Graphics.FromImage(bmp);
            Font font = new Font(fonts, Punto, FontStyle.Bold);

            Random r = new Random();
            System.Drawing.Image img = System.Drawing.Image.FromFile(BackgroundImage);
                //Arka plan resmi captcha kodunun kolayca okunmasını önlemek için. Ne kadar karışık bir resim olursa o kadar iyidir.

            g.DrawImage(img, 0, 0);

            g.DrawString(Kod, font, Brushes.DarkOrange, X, Y);

            g.CompositingQuality = CompositingQuality.HighQuality;

            //return  bmp;
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Gif);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            return "data:image/gif;base64," + base64Data;
            //imgCtrl.Src = "data:image/gif;base64," + base64Data;
        }

        public static string CreateRandomPassword(int Length)
        {
            string Result = "";
            Random Rnd = new Random();
            for (int i = 0; i < Length; i++)
            {
                Result = Result + Rnd.Next((i == 0 ? 1 : 0), 9);
            }
            return Result;
                //Rnd.Next(Convert.ToInt32((Math.Pow(10, Length - 1))), Convert.ToInt32((Math.Pow(10, Length))) - 1).ToString();
        }

        #region mail

        public static string SendEmail(string SiteName, string From, string To, string Subject, string Body)
        {
            try
            {
                MailMessage email = new MailMessage(From.ToLower(), To.ToLower(), Subject, Body);
                email.From = new MailAddress(From, char.ToUpper(SiteName[0]) + SiteName.Substring(1));
                email.IsBodyHtml = true;
                email.BodyEncoding = Encoding.GetEncoding(1254);
                email.SubjectEncoding = Encoding.GetEncoding(1254);
                email.ReplyTo = new MailAddress("support@poemhunter.com");

                //SmtpClient smtp = new SmtpClient("GENEL-S1", 25);
                //smtp.Credentials = new NetworkCredential("alarm", "gd_47!ser.x");
                SmtpClient smtp = new SmtpClient("yenimedya.smtp.com", 25);
                smtp.Credentials = new NetworkCredential("ekrem.teymur@gmail.com", "yenismtp");
                smtp.Send(email);
                return "success";
            }
            catch
            {
                return "error";
            }
        }

        #endregion mail

        /// <summary>
        /// string olarak verilen querystring',i istediğin türde geri çağırma
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetQueryStringValue<T>(string key)
        {
            string value = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[key]))
            {
                value = HttpContext.Current.Request.QueryString[key];
            }
            return ConvertType<T>(value);
        }

        public static T ConvertType<T>(object value)
        {
            try
            {
                return (T) System.Convert.ChangeType(value, typeof (T), CultureInfo.CurrentCulture);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Kaydedilecek dosya için AY/YIL/GÜN/ olarak path oluşturur
        /// </summary>
        /// <returns>Örnek: 2015/01/30/</returns>
        public static string GetDirectory()
        {
            string year = DateTime.Today.Year.ToString();
            string month = (DateTime.Today.Month).ToString();
            string day = DateTime.Today.Day.ToString();
            return string.Format("/{0}/{1}/{2}/", year, month, day);
        }

        public static string GetDirectoryForInline()
        {
            string year = DateTime.Today.Year.ToString();
            string month = (DateTime.Today.Month).ToString();
            string day = DateTime.Today.Day.ToString();
            string hour = DateTime.Now.Hour.ToString();
            return string.Format("{0}_{1}_{2}_{3}_", year, month, day, hour);
        }

        /// <summary>
        /// Verilen title ı url formatına çevirir
        /// </summary>
        /// <param name="title">url formatına çevrilecek string değer</param>
        /// <returns>string url</returns>
        public static string GetUrl(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            string temp = title.ToLower().Trim();
            temp = temp.Replace(" ", "-");
            temp = temp.Replace(".", "-");
            temp = temp.Replace("_", "-");
            temp = temp.Replace(" ", "-");
            temp = temp.Replace("ç", "c");
            temp = temp.Replace("ğ", "g");
            temp = temp.Replace("ı", "i");
            temp = temp.Replace("ö", "o");
            temp = temp.Replace("ş", "s");
            temp = temp.Replace("ü", "u");
            temp = temp.Replace("'", "");
            temp = temp.Replace("!", "");
            temp = temp.Replace("?", "");
            temp = temp.Replace("*", "");
            temp = temp.Replace(":", "-");
            temp = temp.Replace(";", "");
            temp = temp.Replace("~", "");
            temp = temp.Replace(",", "");
            temp = temp.Replace("&", "");
            temp = temp.Replace("%", "");
            temp = temp.Replace("(", "-");
            temp = temp.Replace(")", "-");
            temp = temp.Replace("[", "-");
            temp = temp.Replace("]", "-");
            temp = temp.Replace("=", "-");
            temp = temp.Replace("<", "-");
            temp = temp.Replace(">", "-");
            temp = temp.Replace("^", "");
            temp = temp.Replace("+", "-");
            temp = temp.Replace("{", "-");
            temp = temp.Replace("}", "-");
            temp = temp.Replace("$", "");
            temp = temp.Replace("#", "");
            temp = temp.Replace("/", "-");
            temp = temp.Replace("|", "-");
            temp = temp.Replace("\"", "-");
            temp = temp.Replace("‘", "");
            temp = temp.Replace("’", "");
            temp = temp.Replace("“", "");
            temp = temp.Replace("”", "");
            temp = temp.Replace("á", "a");
            temp = temp.Replace("ê", "e");
            temp = temp.Replace("â", "a");
            temp = temp.Replace("^", "");
            temp = temp.Replace("â", "a");
            temp = temp.Replace("î", "i");
            temp = temp.Replace("î", "i");
            temp = temp.Replace("ê", "e");
            temp = temp.Replace("û", "u");
            temp = temp.Replace("ô", "o");
            temp = temp.Replace("haberi", "-");
            temp = temp.Replace("-------", "-");
            temp = temp.Replace("------", "-");
            temp = temp.Replace("-----", "-");
            temp = temp.Replace("----", "-");
            temp = temp.Replace("---", "-");
            temp = temp.Replace("--", "-");
            temp = temp.Replace("--", "-");
            temp = temp.Replace("--", "-");
            temp = temp.Replace("--", "-");
            temp = temp.Replace(".....", "");
            temp = temp.Replace("....", "");
            temp = temp.Replace("...", "");
            temp = temp.Replace("..", "");
            return temp;
        }

        public static string SifreleMd5(string metin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] byt = Encoding.UTF8.GetBytes(metin);
            byt = md5.ComputeHash(byt);
            StringBuilder sb = new StringBuilder();
            foreach (byte bt in byt)
            {
                sb.Append(bt.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        public static string RemoveHtml(string html)
        {
            return Regex.Replace(html, "<.*?>", " ").Replace("&", " ").Replace("%", " ");
        }

        public static string ClearString(string Value)
        {
            if (string.IsNullOrEmpty(Value))
                return "";
            string _Value = Value.ToString();
            _Value = _Value.Replace(") ;", "):");
            _Value = _Value.Replace(");", "):");
            _Value = _Value.Replace("--", "-");
            _Value = _Value.Replace("update ", "updat ");
            _Value = _Value.Replace("delete ", "delet ");
            _Value = _Value.Replace("drop ", "dropp ");
            _Value = _Value.Replace("insert ", "inser ");
            _Value = _Value.Replace("'", "’");
            _Value = _Value.Replace("\"", "“");
            _Value = _Value.Replace("!", "");
            _Value = _Value.Replace("%", "");
            _Value = _Value.Replace("&", "");
            _Value = _Value.Replace("|", "");
            _Value = _Value.Replace(")", "");
            _Value = _Value.Replace("(", "");
            _Value = _Value.Replace("]", "");
            _Value = _Value.Replace("[", "");
            _Value = _Value.Replace("=", "");
            _Value = _Value.Replace("?", "");
            _Value = _Value.Replace("$", "");
            _Value = _Value.Replace("+", "");
            _Value = _Value.Replace("{", "");
            _Value = _Value.Replace("}", "");
            _Value = _Value.Replace("\\", "");
            _Value = _Value.Replace("/", "");
            _Value = _Value.Replace("é", "");
            _Value = _Value.Replace(";", "");
            _Value = _Value.Replace("`", "");
            _Value = _Value.Replace(">", "");
            _Value = _Value.Replace("<", "");
            _Value = _Value.Replace("|", "");
            _Value = _Value.Replace("^", "");
            _Value = _Value.Trim();
            return _Value;
        }

        public static string ToEnglishWord(string Value)
        {
            Value = Value.ToString().Replace("Ç", "C");
            Value = Value.ToString().Replace("Ü", "U");
            Value = Value.ToString().Replace("Ö", "O");
            Value = Value.ToString().Replace("Ş", "S");
            Value = Value.ToString().Replace("Ğ", "G");
            Value = Value.ToString().Replace("İ", "I");
            Value = Value.ToString().Replace("ç", "c");
            Value = Value.ToString().Replace("ü", "u");
            Value = Value.ToString().Replace("ö", "o");
            Value = Value.ToString().Replace("ş", "s");
            Value = Value.ToString().Replace("ğ", "g");
            Value = Value.ToString().Replace("ı", "i");
            return Value;
        }

        public static string ToUpperFirstLetter(string Value)
        {
            string ResultString = "";
            if (Value != null)
            {
                string[] kelimeler = Value.Split(' ');
                foreach (string item in kelimeler)
                {
                    if (item.Length > 0)
                        ResultString += item.Substring(0, 1).ToUpper() +
                                        (item.Length > 1 ? item.Substring(1).ToLower() : "") + " ";
                }
            }
            return ResultString.Trim();
        }
    }
}

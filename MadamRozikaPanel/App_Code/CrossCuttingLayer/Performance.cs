using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class Performance
    {
        public static string MinifyHtml(this string HtmlText)
        {
            HtmlText = System.Text.RegularExpressions.Regex.Replace(HtmlText, @"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}", string.Empty);
            HtmlText = System.Text.RegularExpressions.Regex.Replace(HtmlText, @"[ \f\r\t\v]?([\n\xFE\xFF/{}[\];,<>*%&|^!~?:=])[\f\r\t\v]?", "$1");
            HtmlText = System.Text.RegularExpressions.Regex.Replace(HtmlText, "<!\\-\\-.*?\\-\\->", string.Empty);
            HtmlText = HtmlText.Replace(";\n", ";");
            return HtmlText.Trim();

        }
    }


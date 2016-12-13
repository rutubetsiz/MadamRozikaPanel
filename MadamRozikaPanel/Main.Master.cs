using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel
{
    public partial class Main : System.Web.UI.MasterPage
    {
        private string url;
        protected string clsAnasayfa;
        protected string clsHaberler;
        protected string clsGaleriler;
        protected string clsYazarYazi;
        protected string clsModerasyon;
        protected string clsHeadlines;
        protected string clsRaporlama;
        protected string clsYetkilendirme;
        private string PageTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            url = Request.Url.ToString();

            if (url.Contains("/News/"))
            {
                clsHaberler = "active";
                PageTitle = "Haberler";
            }
            else if (url.Contains("/Gallery/"))
            {
                clsGaleriler = "active";
                PageTitle = "Galeriler";
            }
            else if (url.Contains("/Authors/"))
            {
                clsYazarYazi = "active";
                PageTitle = "Yazarlar - Yazılar";
            }
            else if (url.Contains("/Moderation/"))
            {
                clsModerasyon = "active";
                PageTitle = "Moderasyon";
            }
            else if (url.Contains("/Headlines/"))
            {
                clsHeadlines = "active";
                PageTitle = "Manşet Yönetimi";
            }
            else if (url.Contains("/Reporting/"))
            {
                clsRaporlama = "active";
                PageTitle = "Raporlama";
            }
            else if (url.Contains("/Authorization/"))
            {
                clsYetkilendirme = "active";
                PageTitle = "Yetkilendirme";
            }
            else
            {
                clsAnasayfa = "active";
                PageTitle = "Anasayfa";
            }

        }
    }
}

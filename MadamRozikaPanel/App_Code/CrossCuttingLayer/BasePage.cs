using System;
using System.Web.UI;

namespace MadamRozikaPanel.CrossCuttingLayer
{
    public class BasePage : Page
    {
        private int _PageCache = 600;

        public int PageCache
        {
            get { return _PageCache; }
            set { _PageCache = value; }
        }

        public Seo PageSeo;

        public BasePage()
        {
            PageSeo = GetDefaultSiteSeoSettings(Page);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Response.Cache.SetMaxAge(new TimeSpan(0, 0, PageCache));
            Response.CacheControl = "Public";
            Response.Cache.VaryByParams.IgnoreParams = true;
            Response.Cache.SetSlidingExpiration(true);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            PageSeo.RenderSeo();
            using (HtmlTextWriter HtmlWriter = new HtmlTextWriter(new System.IO.StringWriter()))
            {
                base.Render(HtmlWriter);
                if (Request.QueryString["MinifyHtml"] == null) //Test For Html
                {
                    writer.Write(HtmlWriter.InnerWriter.ToString().MinifyHtml());
                }
                else
                {
                    writer.Write(HtmlWriter.InnerWriter);
                }
            }
        }


        protected virtual Seo GetDefaultSiteSeoSettings(Page page)
        {
            Seo PageSeo = new Seo(page);
            PageSeo.Title = "Haber";
            return PageSeo;
        }
    }
}
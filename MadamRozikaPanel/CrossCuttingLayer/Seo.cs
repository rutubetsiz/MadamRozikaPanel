using MadamRozikaPanel.CrossCuttingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MadamRozikaPanel.CrossCuttingLayer
{
    public class Seo
    {
        private Page Page;

        public Seo(Page p)
        {
            Page = p;
        }

        private string _Favicon = "";

        public string Favicon
        {
            get { return _Favicon; }
            set { _Favicon = value; }
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Viewport = "";

        public string Viewport
        {
            get { return _Viewport; }
            set { _Viewport = value; }
        }

        private string _AppleMobileWebAppCapable = "yes";

        public string AppleMobileWebAppCapable
        {
            get { return _AppleMobileWebAppCapable; }
            set { _AppleMobileWebAppCapable = value; }
        }

        private string _Identifier;

        public string Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Keywords;

        public string Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }

        private string _Section;

        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
        }

        private DateTime _DateModified;

        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }

        private DateTime _DatePublished;

        public DateTime DatePublished
        {
            get { return _DatePublished; }
            set { _DatePublished = value; }
        }

        private string _Canonical;

        public string Canonical
        {
            get { return _Canonical; }
            set { _Canonical = value; }
        }

        private string _NewsKeywords;

        public string NewsKeywords
        {
            get { return _NewsKeywords; }
            set { _NewsKeywords = value; }
        }

        private string _Image;

        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private string _Genre;

        public string Genre
        {
            get { return _Genre; }
            set { _Genre = value; }
        }

        private string _PST;

        public string PST
        {
            get { return _PST; }
            set { _PST = value; }
        }

        private string _Publisher;

        public string Publisher
        {
            get { return _Publisher; }
            set { _Publisher = value; }
        }

        private string _CRE;

        public string CRE
        {
            get { return _CRE; }
            set { _CRE = value; }
        }

        private string _DiscussionURL;

        public string DiscussionURL
        {
            get { return _DiscussionURL; }
            set { _DiscussionURL = value; }
        }

        private string _ThumbnailUrl;

        public string ThumbnailUrl
        {
            get { return _ThumbnailUrl; }
            set { _ThumbnailUrl = value; }
        }

        private string _ArticleTag;

        public string ArticleTag
        {
            get { return _ArticleTag; }
            set { _ArticleTag = value; }
        }

        private string _FBAppID;

        public string FBAppID
        {
            get { return _FBAppID; }
            set { _FBAppID = value; }
        }

        private string _OGTitle;

        public string OGTitle
        {
            get { return _OGTitle; }
            set { _OGTitle = value; }
        }

        private string _TwitterSite;

        public string TwitterSite
        {
            get { return _TwitterSite; }
            set { _TwitterSite = value; }
        }

        private string _VideoURL;

        public string VideoURL
        {
            get { return _VideoURL; }
            set { _VideoURL = value; }
        }

        private string _VideoHeight;

        public string VideoHeight
        {
            get { return _VideoHeight; }
            set { _VideoHeight = value; }
        }

        private string _VideoWidth;

        public string VideoWidth
        {
            get { return _VideoWidth; }
            set { _VideoWidth = value; }
        }

        private string _VideoType;

        public string VideoType
        {
            get { return _VideoType; }
            set { _VideoType = value; }
        }

        private string _CopyrightYear = "tr-TR";

        public string CopyrightYear
        {
            get { return _CopyrightYear; }
            set { _CopyrightYear = value; }
        }

        public string GoogleSiteVerification { get; set; }
        private string StringMetaTag = string.Empty;
        private string _GoogleAnaltics = @"";

        public string GoogleAnaltics
        {
            get { return _GoogleAnaltics; }
            set { _GoogleAnaltics = value; }
        }

        public void AddStringMetaTag(string Html)
        {
            StringMetaTag += Html + "\n\t";
        }

        public string Charset { get; set; }
        public string Pragma { get; set; }
        public string Expires { get; set; }
        public string CacheControl { get; set; }
        public string ContentType { get; set; }
        public string ContentLanguage { get; set; }
        public string StartURL { get; set; }
        public string ThumbnailHeight { get; set; }
        public string ThumbnailWeight { get; set; }
        public string Rating { get; set; }
        public string PT { get; set; }
        public string Robots { get; set; }
        public string Medium { get; set; }
        public string Refresh { get; set; }
        public string AlternateLink { get; set; }

        public void RenderSeo()
        {
            DateModified = DateTime.MinValue != DateModified ? DateModified : DatePublished;

            AddMeta(new LiteralControl(StringMetaTag));
            AddMeta(new LiteralControl(GoogleAnaltics));
            HtmlLink hlinkCanonical = new HtmlLink();
            hlinkCanonical.Attributes.Add("rel", "canonical");
            hlinkCanonical.Href = Canonical;
            AddMeta(hlinkCanonical);
            AddMeta(new LiteralControl("<link rel=\"shortcut icon\" href=\"" + Favicon + "\" type=\"image/x-icon\" />"));
            if (!string.IsNullOrWhiteSpace(ThumbnailUrl))
                AddMeta(
                    new LiteralControl("<link rel=\"thumbnail\" type=\"image/jpeg\" href=\"" + ThumbnailUrl + "\" />"));
            if (!string.IsNullOrWhiteSpace(Image))
                AddMeta(new LiteralControl("<link rel=\"image_src\" type=\"image/jpeg\" href=\"" + Image + "\" />"));
            if (!string.IsNullOrWhiteSpace(AlternateLink))
                AddMeta(
                    new LiteralControl("<link rel=\"alternate\" media='only screen and (max-width: 640px) ' href=\"" +
                                       AlternateLink + "\" />"));

            AddProperty("twitter:image", Image);
            AddProperty("twitter:description", Helper.ClearString(Description));
            AddProperty("twitter:title", Helper.ClearString(Title));
            AddProperty("twitter:url", Canonical);
            AddMeta("twitter:site", TwitterSite);
            AddMeta("twitter:card", "summary");



            AddProperty("og:video", VideoURL);
            AddProperty("og:video:height", VideoHeight);
            AddProperty("og:video:width", VideoWidth);
            AddProperty("og:video:type", VideoType);
            AddProperty("fb:app_id", FBAppID);
            AddProperty("og:article:section", Section);
            if (DatePublished != DateTime.MinValue)
                AddProperty("og:article:published_time", DatePublished.Ext_ToISO8601DateTimeString());
            AddProperty("og:article:author", Publisher);
            AddProperty("og:image", Image);
            AddProperty("og:locale", "tr");
            AddProperty("og:url", Canonical);
            AddProperty("og:site_name", CRE);
            AddProperty("og:type", PT);
            AddProperty("og:description", Helper.ClearString(Description));
            AddProperty("og:title",
                string.IsNullOrWhiteSpace(OGTitle) ? Helper.ClearString(Title) : Helper.ClearString(OGTitle));
            AddProperty("article:modified", DateModified.Ext_ToISO8601DateTimeString());
            AddProperty("article:tag", ArticleTag);

            AddMeta(new LiteralControl("\n\t"));

            AddItemProp("articleSection", Section);
            if (DatePublished != DateTime.MinValue)
                AddItemProp("datePublished", DatePublished.Ext_ToISO8601DateTimeString());
            if (DateModified != DateTime.MinValue)
                AddItemProp("dateModified", DateModified.Ext_ToISO8601DateTimeString());
            AddItemProp("copyrightYear", CopyrightYear);
            AddItemProp("inLanguage", ContentLanguage);
            AddItemProp("author", Publisher);
            AddItemProp("sourceOrganization", Publisher);
            AddItemProp("creator", Publisher);
            AddItemProp("copyrightHolder", Publisher);
            AddItemProp("discussionUrl", DiscussionURL);
            AddItemProp("provider", Publisher);
            AddItemProp("publisher", Publisher);
            AddItemProp("genre", Genre);
            AddItemProp("url", Canonical);
            AddItemProp("thumbnailUrl", ThumbnailUrl);
            AddItemProp("image", Image);
            AddItemProp("description", Helper.ClearString(Description));
            AddItemProp("name", Helper.ClearString(Title));
            AddItemProp("keywords", string.IsNullOrWhiteSpace(NewsKeywords) ? Keywords : NewsKeywords);
            AddItemProp("alternativeHeadline", Helper.ClearString(Title));
            AddMeta(new LiteralControl("\n\t"));

            if (DateModified != DateTime.MinValue)
                AddhHttpEquiv("last-modified", DateModified.Ext_ToISO8601DateTimeString());
            AddhHttpEquiv("charset", Charset);
            AddhHttpEquiv("pragma", Pragma);
            AddhHttpEquiv("expires", Expires);
            AddhHttpEquiv("cache-control", CacheControl);
            AddhHttpEquiv("content-type", ContentType);
            AddhHttpEquiv("content-language", ContentLanguage);
            AddhHttpEquiv("refresh", Refresh);

            AddMeta("msapplication-starturl", StartURL);
            AddMeta("thumbnail_height", ThumbnailHeight);
            AddMeta("thumbnail_width", ThumbnailWeight);
            AddMeta("google-site-verification", GoogleSiteVerification);
            AddMeta("Rating", Rating);
            AddMeta("lp", Helper.ClearString(Description));
            if (DatePublished != DateTime.MinValue)
                AddMeta("dat", DatePublished.ToString("dd MMMM yyyy dddd", CultureInfo.CurrentCulture));
            if (DatePublished != DateTime.MinValue)
                AddMeta("DISPLAYDATE", DatePublished.ToString("dd MMMM yyyy dddd", CultureInfo.CurrentCulture));
            if (DatePublished != DateTime.MinValue) AddMeta("ptime", DatePublished.ToString("yyyyMMddhhmmss"));
            if (DateModified != DateTime.MinValue) AddMeta("utime", DateModified.ToString("yyyyMMddhhmmss"));
            if (DatePublished != DateTime.MinValue) AddMeta("pdate", DatePublished.ToString("yyyyMMdd"));
            if (DatePublished != DateTime.MinValue)
                AddMeta("DC.date.issued", DatePublished.Ext_ToISO8601DateTimeString());
            AddMeta("cre", CRE);
            AddMeta("PT", PT);
            AddMeta("PST", PST);
            AddMeta("hdl", Helper.ClearString(Title));
            AddMeta("robots", Robots);
            AddMeta("medium", Medium);
            AddMeta(new LiteralControl("<meta charset=\"UTF-8\"/>"));
            AddMeta("apple-mobile-web-app-capable", AppleMobileWebAppCapable);
            AddMeta("viewport", Viewport);
            AddMeta("news_keywords", NewsKeywords);
            AddMeta("keywords", Keywords);
            AddMeta("description", Helper.ClearString(Description));
            AddMeta(new LiteralControl("<title>" + Helper.ClearString(Title) + "</title>"));
        }

        private void AddMeta(string Name, string Content)
        {
            if (Content != null)
            {
                Page.Header.Controls.AddAt(0,
                    new LiteralControl("<meta name=\"" + Name + "\" content=\"" + Content + "\" />"));
                Page.Header.Controls.AddAt(0, new LiteralControl("\n\t"));
            }
        }

        private void AddMeta(Control c)
        {
            if (c != null)
            {
                Page.Header.Controls.AddAt(0, c);
                Page.Header.Controls.AddAt(0, new LiteralControl("\n\t"));
            }
        }

        private void AddItemProp(string Itemprop, string Content)
        {
            if (Content != null)
            {
                Page.Header.Controls.AddAt(0,
                    new LiteralControl("<meta itemprop=\"" + Itemprop + "\" content=\"" + Content + "\" />"));
                Page.Header.Controls.AddAt(0, new LiteralControl("\n\t"));
            }
        }

        private void AddhHttpEquiv(string Name, string Content)
        {
            if (Content != null)
            {
                Page.Header.Controls.AddAt(0,
                    new LiteralControl("<meta http-equiv=\"" + Name + "\" content=\"" + Content + "\" />"));
                Page.Header.Controls.AddAt(0, new LiteralControl("\n\t"));
            }
        }

        private void AddProperty(string prop, string Content)
        {
            if (Content != null)
            {
                Page.Header.Controls.AddAt(0,
                    new LiteralControl("<meta property=\"" + prop + "\" content=\"" + Content + "\" />"));
                Page.Header.Controls.AddAt(0, new LiteralControl("\n\t"));
            }
        }
    }

}
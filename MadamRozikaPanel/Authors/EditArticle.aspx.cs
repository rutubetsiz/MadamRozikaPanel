using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.Authors
{
    public partial class EditArticle : System.Web.UI.Page
    {
        public string Baslik;
        public int ArticleId;
        public string Link;
        public string AltBaslik;
        O_Article ArticleOprt = new O_Article();
        O_Cross CrossOprt = new O_Cross();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ArticleId"]))
            {
                ArticleId = 0;
            }
            else
            {
                ArticleId = Convert.ToInt32(Request.QueryString["ArticleId"]);
            }
            if (ArticleId == 0)
            {
                Baslik = "Yazı Ekle";
                Link = "/Authors/EditArticle.aspx";
                AltBaslik = "Yeni Yazı Ekle";
            }
            else
            {
                Baslik = "Yazı Düzenle";
                Link = "/Authors/EditArticle.aspx?ArticleId=" + ArticleId;
                AltBaslik = ArticleId + " ID'li Yazıyı Düzenle";
                if (!IsPostBack)
                {
                    M_Articles A = ArticleOprt.GetArticleDetail(ArticleId);
                    txtBaslik.Text = A.Title;
                    cbDurum.Checked = Convert.ToBoolean(A.Status);
                    cbYorum.Checked = Convert.ToBoolean(A.CommentActive);
                    ddListYazarlar.SelectedValue = A.AuthorId.ToString();
                    ckEditor.Value = A.ArticleText;
                }
            }

            if (!IsPostBack)
            {
                FillFilter();
            }
        }
        public void FillFilter()
        {
            ddListYazarlar.DataSource = ArticleOprt.GetAllAuthors();
            ddListYazarlar.DataTextField = "Name";
            ddListYazarlar.DataValueField = "AuthorId";
            ddListYazarlar.DataBind();
            ddListYazarlar.Items.Insert(0, new ListItem("Tüm Yazarlar", "-1"));
            if (ArticleId == 0)
            {
                ddListYazarlar.SelectedValue = "-1";
            }
        }
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (ArticleId == 0)
            {
                ArticleOprt.InsertArticle(txtBaslik.Text, ddListYazarlar.SelectedValue, Convert.ToInt32(cbDurum.Checked), Convert.ToInt32(cbYorum.Checked), ckEditor.Value);
            }
            else
            {
                ArticleOprt.UpdateArticle(txtBaslik.Text, ddListYazarlar.SelectedValue, Convert.ToInt32(cbDurum.Checked), Convert.ToInt32(cbYorum.Checked), ckEditor.Value, ArticleId.ToString());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MadamRozikaPanel.CrossCuttingLayer;

namespace MadamRozikaPanel.Authors
{
    public partial class EditAuthor : System.Web.UI.Page
    {
        public string Baslik;
        public int AuthorId;
        public string Link;
        public string AltBaslik;
        public int Rank;
        O_Author AuthorOprt = new O_Author();
        O_Cross CrossOprt = new O_Cross();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["AuthorId"]))
            {
                AuthorId = 0;
            }
            else
            {
                AuthorId = Convert.ToInt32(Request.QueryString["AuthorId"]);
            }
            if (!IsPostBack)
            {
                //cblSite = CrossOprt.FillSites(ref cblSite);
                ddListKategori.DataSource = CrossOprt.KategoriDoldur();
                ddListKategori.DataTextField = "Name";
                ddListKategori.DataValueField = "CategoryId";
                ddListKategori.DataBind();
                ddListKategori.Items.Insert(0, new ListItem("Tüm Kategoriler", "-1"));

                ddListSira.DataSource = AuthorOprt.GetRowNumber();
                ddListSira.DataTextField = "RowNumber";
                ddListSira.DataValueField = "RowNumber";
                ddListSira.DataBind();
            }
            if (AuthorId == 0)
            {
                Baslik = "Yazar Ekle";
                Link = "/Authors/EditAuthor.aspx";
                AltBaslik = "Yeni Yazar Ekle";
            }
            else
            {
                Baslik = "Yazar Düzenle";
                Link = "/Authors/EditAuthor.aspx?AuthorId=" + AuthorId;
                AltBaslik = AuthorId + " ID'li Yazarı Düzenle";
                if (!IsPostBack)
                {
                    M_Authors A = AuthorOprt.GetAllAuthorDetail(AuthorId);
                    txtAd.Text = A.Name;
                    txtMail.Text = A.Mail;
                    txtTwitter.Text = A.TwitterUrl;
                    txtFacebook.Text = A.FacebookUrl;
                    txtLinkedin.Text = A.LinkedinUrl;
                    txtEmbed.Text = A.Embed;
                    cbDurum.Checked = Convert.ToBoolean(A.Status);
                    cbAnaSayfa.Checked = Convert.ToBoolean(A.MainPageStatus);
                    //cblSite = CrossOprt.SelectSites(ref cblSite, A.Sites.Item(0));
                    ddListKategori.SelectedValue = A.CategoryId.ToString();
                    ddListSira.SelectedValue = A.Rank.ToString();
                    Rank = A.Rank;
                }
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string NameUrl = Helper.GetUrl(txtAd.Text);

            //string Sites = CrossOprt.ReturnSites(cblSite);
            string folder = ConfigurationManager.AppSettings["AuthorImagePath"] + @"\";

            if (AuthorId == 0)
            {
                if (FuAuthor.HasFile)
                {
                    string AId = AuthorOprt.InsertAuthor(txtAd.Text, txtMail.Text, Convert.ToInt32(cbDurum.Checked), Convert.ToInt32(cbAnaSayfa.Checked), NameUrl, folder + NameUrl + Path.GetExtension(FuAuthor.FileName), txtTwitter.Text, txtFacebook.Text, txtLinkedin.Text, txtEmbed.Text, Convert.ToInt32(ddListKategori.SelectedValue), Convert.ToInt32(ddListSira.SelectedValue));
                    if (!string.IsNullOrEmpty(AId))
                    {
                        #region Yazar için seçilen görselin orjinali ve varyasyonları kaydediliyor.
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        byte[] imageByteArray = FuAuthor.FileBytes;
                        MemoryStream stream = new MemoryStream();
                        stream.Write(imageByteArray, 0, imageByteArray.Length);
                        Bitmap imageBitMap = new Bitmap(stream);
                        System.Drawing.Image yeniImg = System.Drawing.Image.FromStream(stream);
                        int height = (yeniImg.Height * 640) / yeniImg.Width;
                        UploadImage uploadGaleriResmi = new UploadImage();
                        uploadGaleriResmi.SaveImageSingle(imageBitMap, folder, NameUrl + Path.GetExtension(FuAuthor.FileName), 200, 200);
                        #endregion
                    }
                }
            }
            else
            {
                if (FuAuthor.HasFile)
                {
                    #region Yazar için seçilen görselin orjinali ve varyasyonları kaydediliyor.
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    byte[] imageByteArray = FuAuthor.FileBytes;
                    MemoryStream stream = new MemoryStream();
                    stream.Write(imageByteArray, 0, imageByteArray.Length);
                    Bitmap imageBitMap = new Bitmap(stream);
                    System.Drawing.Image yeniImg = System.Drawing.Image.FromStream(stream);
                    int height = (yeniImg.Height * 640) / yeniImg.Width;
                    UploadImage uploadGaleriResmi = new UploadImage();
                    uploadGaleriResmi.SaveImageSingle(imageBitMap, folder, NameUrl + Path.GetExtension(FuAuthor.FileName), 200, 200);
                    #endregion

                    AuthorOprt.UpdateAuthor(txtAd.Text, txtMail.Text, Convert.ToInt32(cbDurum.Checked), Convert.ToInt32(cbAnaSayfa.Checked), NameUrl, folder + NameUrl + Path.GetExtension(FuAuthor.FileName), txtTwitter.Text, txtFacebook.Text, txtLinkedin.Text, txtEmbed.Text, Convert.ToInt32(ddListKategori.SelectedValue), Convert.ToInt32(ddListSira.SelectedValue), Rank, AuthorId);
                }
                else
                {
                    AuthorOprt.UpdateAuthor(txtAd.Text, txtMail.Text, Convert.ToInt32(cbDurum.Checked), Convert.ToInt32(cbAnaSayfa.Checked), NameUrl, txtTwitter.Text, txtFacebook.Text, txtLinkedin.Text, txtEmbed.Text, Convert.ToInt32(ddListKategori.SelectedValue), Convert.ToInt32(ddListSira.SelectedValue), Rank, AuthorId);
                }
            }
        }
    }
}
using MadamRozikaPanel.CrossCuttingLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.Headlines
{
    public partial class EditHeadline : System.Web.UI.Page
    {
        public string Baslik;
        public int HeadlineId;
        public string Link;
        public string AltBaslik;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["HeadlineId"]))
            {
                HeadlineId = 0;
            }
            else
            {
                HeadlineId = Convert.ToInt32(Request.QueryString["HeadlineId"]);
            }
            if (HeadlineId == 0)
            {
                Baslik = "Manşet Ekle";
                Link = "/Headlines/EditHeadline.aspx";
                AltBaslik = "Yeni Manşet Ekle";
            }
            else
            {
                Baslik = "Manşet Düzenle";
                Link = "/Headlines/EditHeadline.aspx?AuthorId=" + HeadlineId;
                AltBaslik = HeadlineId + " ID'li Manşeti Düzenle";
                if (!IsPostBack)
                {
                    //Headlines H = AuthorOprt.GetAllAuthorDetail(HeadlineId);
                    //txtAd.Text = A.Name;
                    //txtMail.Text = A.Mail;
                    //cbDurum.Checked = Convert.ToBoolean(A.Status);
                    //cbAnaSayfa.Checked = Convert.ToBoolean(A.MainPageStatus);

                    //XmlNode xn = A.Sites.Item(0);
                    //string bugun = xn["bugun"].InnerText;
                    //string millet = xn["millet"].InnerText;
                    //if (bugun == "1")
                    //{
                    //    cblSite.Items.FindByValue("bugun").Selected = true;
                    //}
                    //if (millet == "1")
                    //{
                    //    cblSite.Items.FindByValue("millet").Selected = true;
                    //}
                }
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string title = txtBaslik.Text;
            string description = txtSpot.Text;
            string url = txtUrl.Text;
            int status = Convert.ToInt32(cbDurum.Checked);

            string folder = ConfigurationManager.AppSettings["HeadlineImagePath"] + @"\" + Helper.GetDirectory();

            if (HeadlineId == 0)
            {
                if (FuManset.HasFile)
                {
                    //string HId = HeadlineOprt.InsertHeadline(title, description, url, Convert.ToInt32(cbDurum.Checked), folder, "Manşet");
                    if (true) //if (!string.IsNullOrEmpty(HId))
                    {
                        #region Manşet için seçilen görsel yükleniyor

                        Bitmap imageBitMap = new Bitmap(FuManset.PostedFile.InputStream);
                        UploadImage uploadHeadline = new UploadImage();
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        //uploadHeadline.SaveImageSingle(imageBitMap, folder + "/", HId + "_640_360" + Path.GetExtension(FuManset.FileName), 640, 360);
                        #endregion
                    }
                }
            }
            //else
            //{
            //    if (FuManset.HasFile)
            //    {
            //        #region Yazar için seçilen görselin orjinali ve varyasyonları kaydediliyor.
            //        if (!Directory.Exists(folder))
            //            Directory.CreateDirectory(folder);
            //        byte[] imageByteArray = FuManset.FileBytes;
            //        MemoryStream stream = new MemoryStream();
            //        stream.Write(imageByteArray, 0, imageByteArray.Length);
            //        Bitmap imageBitMap = new Bitmap(stream);
            //        System.Drawing.Image yeniImg = System.Drawing.Image.FromStream(stream);
            //        int height = (yeniImg.Height * 640) / yeniImg.Width;
            //        UploadImage uploadGaleriResmi = new UploadImage();
            //        uploadGaleriResmi.SaveImageSingle(imageBitMap, folder, Helper.GetUrl(txtBaslik.Text) + Path.GetExtension(FuManset.FileName), 200, 200);
            //        #endregion

            //        AuthorOprt.UpdateAuthor(txtBaslik.Text, txtSpot.Text, Convert.ToInt32(cbDurum.Checked), Convert.ToInt32(cbAnaSayfa.Checked), HeadlineId, url, folder + Helper.GetUrl(txtBaslik.Text) + Path.GetExtension(FuAuthor.FileName), Sites);
            //    }
            //    else
            //    {
            //        AuthorOprt.UpdateAuthor(txtBaslik.Text, txtSpot.Text, Convert.ToInt32(cbDurum.Checked), Convert.ToInt32(cbAnaSayfa.Checked), HeadlineId, url, Sites);
            //    }
            //}
        }
    }
}
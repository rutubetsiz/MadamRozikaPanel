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

namespace MadamRozikaPanel.Gallery
{
    public partial class EditGallery : System.Web.UI.Page
    {
        public string Baslik;
        public int GalleryId;
        public string Link;
        public string AltBaslik;
        O_Gallery GalleryOprt = new O_Gallery();
        M_Galleries G = new M_Galleries();
        O_Cross CrossOprt = new O_Cross();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["GalleryId"]))
            {
                GalleryId = 0;
            }
            else
            {
                GalleryId = Convert.ToInt32(Request.QueryString["GalleryId"]);
            }
            if (GalleryId == 0)
            {
                Baslik = "Galeri Ekle";
                Link = "/Gallery/EditGallery.aspx";
                AltBaslik = "Yeni Galeri Ekle";
            }
            else
            {
                Baslik = "Galeri Düzenle";
                Link = "/Gallery/EditGallery.aspx?GalleryId=" + GalleryId;
                AltBaslik = GalleryId + " ID'li Galeriyi Düzenle";
                if (!IsPostBack)
                {
                    G = GalleryOprt.GalleryDetail(GalleryId);
                    txtBaslik.Text = G.Title;
                    ddListKategori.SelectedValue = G.CategoryId.ToString();
                    ddListTur.SelectedValue = G.Type.ToString();
                    ddListAktifmi.SelectedValue = G.Status.ToString();
                }
            }


            if (!IsPostBack)
            {
                FillFilter();
            }
        }
        public void FillFilter()
        {
            ddListKategori.DataSource = GalleryOprt.FillDropDownList();
            ddListKategori.DataTextField = "Name";
            ddListKategori.DataValueField = "CategoryId";
            ddListKategori.DataBind();
            ddListKategori.Items.Insert(0, new ListItem("Tüm Kategoriler", "-1"));
        }
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string url = Helper.GetUrl(txtBaslik.Text);
            if (GalleryId == 0)
            {
                string folder = ConfigurationManager.AppSettings["GalleryImagePath"] + @"\" + Helper.GetDirectory();
                string GId = GalleryOprt.InsertGallery(Convert.ToInt32(ddListKategori.SelectedValue.ToString()), txtBaslik.Text, Convert.ToInt32(ddListTur.SelectedValue.ToString()), Convert.ToInt32(ddListAktifmi.SelectedValue.ToString()), url);
                if (!string.IsNullOrEmpty(GId))
                {
                    if (FuGaleriGorseli.HasFile)
                    {
                        #region Galeri için seçilen görselin orjinali ve varyasyonları kaydediliyor.
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        byte[] imageByteArray = FuGaleriGorseli.FileBytes;
                        MemoryStream stream = new MemoryStream();
                        stream.Write(imageByteArray, 0, imageByteArray.Length);
                        Bitmap imageBitMap = new Bitmap(stream);
                        System.Drawing.Image yeniImg = System.Drawing.Image.FromStream(stream);
                        int height = (yeniImg.Height * 640) / yeniImg.Width;
                        UploadImage uploadGaleriResmi = new UploadImage();
                        uploadGaleriResmi.SaveImageAllSize(imageBitMap, folder, GId, txtBaslik.Text);
                        #endregion

                        if (FuGaleriitems.HasFile)
                        {
                            string ItemsSql = "";
                            #region Oluşturulan galeri fotogaleri ise galeri görselleri ekleniyor.
                            string[] itemSummary = txtItemSummary.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                            int itemSummaryCount = itemSummary.Length;
                            for (int i = 0; i < FuGaleriitems.PostedFiles.Count; i++)
                            {
                                Bitmap imageBitMapitems = new Bitmap(FuGaleriitems.PostedFiles[i].InputStream);
                                System.Drawing.Image yeniImgitem = System.Drawing.Image.FromStream(FuGaleriitems.PostedFiles[i].InputStream);
                                int heightitem = (yeniImgitem.Height * 640) / yeniImgitem.Width;
                                string imageName = url + "_" + GId + "_" + (i + 1) + Path.GetExtension(FuGaleriitems.PostedFiles[i].FileName);
                                UploadImage uploaditems = new UploadImage();
                                uploaditems.SaveImageSingle(imageBitMapitems, folder, imageName, 640, heightitem);

                                if (i < itemSummaryCount)
                                {
                                    ItemsSql += "INSERT INTO GalleryItem (GalleryId, Summary, Type, FilePath, ItemOrder) VALUES (" + GId + ", '" + itemSummary[i].ToString() + "', 1, '" + folder + imageName + "', " + (i + 1) + "); ";
                                }
                                else
                                {
                                    ItemsSql += "INSERT INTO GalleryItem (GalleryId, Summary, Type, FilePath, ItemOrder) VALUES (" + GId + ", '', 1, '" + folder + imageName + "', " + (i + 1) + "); ";
                                }
                            }
                            O_Gallery.InsertGalleryItems(ItemsSql);
                            #endregion
                        }
                        else if (FuVideo.HasFile)
                        {
                            #region Oluşturulan galeri video ise galeri videosu ekleniyor.
                            string VideoFolder = ConfigurationManager.AppSettings["GalleryVideoPath"] + @"\" + Helper.GetDirectory();
                            if (!Directory.Exists(VideoFolder))
                                Directory.CreateDirectory(VideoFolder);
                            string file = VideoFolder + url + "_" + GId + Path.GetExtension(FuVideo.PostedFile.FileName);
                            FuVideo.SaveAs(file);
                            O_Gallery.InsertGalleryItems("INSERT INTO GalleryItem (GalleryId, Summary, Type, FilePath, ItemOrder) VALUES (" + GId + ", '" + txtVideoSummary.Text + "', 0, '" + file + "', 1)");
                            #endregion
                        }
                    }
                }
            }
            else
            {
                GalleryOprt.UpdateGallery(Convert.ToInt32(ddListKategori.SelectedValue.ToString()), txtBaslik.Text, Convert.ToInt32(ddListTur.SelectedValue.ToString()), Convert.ToInt32(ddListAktifmi.SelectedValue.ToString()), url, GalleryId.ToString());
                G = GalleryOprt.GalleryDetail(GalleryId);
                string folder = "";//G.FilePath;
                string GId = GalleryId.ToString();
                if (!string.IsNullOrEmpty(GId))
                {
                    if (FuGaleriGorseli.HasFile)
                    {
                        #region Galeri için seçilen görselin orjinali ve varyasyonları kaydediliyor.
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        byte[] imageByteArray = FuGaleriGorseli.FileBytes;
                        MemoryStream stream = new MemoryStream();
                        stream.Write(imageByteArray, 0, imageByteArray.Length);
                        Bitmap imageBitMap = new Bitmap(stream);
                        System.Drawing.Image yeniImg = System.Drawing.Image.FromStream(stream);
                        int height = (yeniImg.Height * 640) / yeniImg.Width;
                        UploadImage uploadGaleriResmi = new UploadImage();
                        uploadGaleriResmi.SaveImageAllSize(imageBitMap, folder, GId, txtBaslik.Text);
                        #endregion

                        if (FuGaleriitems.HasFile)
                        {
                            string ItemsSql = "";
                            #region Oluşturulan galeri fotogaleri ise galeri görselleri ekleniyor.
                            string[] itemSummary = txtItemSummary.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                            int itemSummaryCount = itemSummary.Length;
                            for (int i = 0; i < FuGaleriitems.PostedFiles.Count; i++)
                            {
                                Bitmap imageBitMapitems = new Bitmap(FuGaleriitems.PostedFiles[i].InputStream);
                                System.Drawing.Image yeniImgitem = System.Drawing.Image.FromStream(FuGaleriitems.PostedFiles[i].InputStream);
                                int heightitem = (yeniImgitem.Height * 640) / yeniImgitem.Width;
                                string imageName = url + "_" + GalleryId + "_" + (i + 1) + Path.GetExtension(FuGaleriitems.PostedFiles[i].FileName);
                                UploadImage uploaditems = new UploadImage();
                                uploaditems.SaveImageSingle(imageBitMapitems, folder, imageName, 640, heightitem);

                                if (i < itemSummaryCount)
                                {
                                    ItemsSql += "INSERT INTO GalleryItem (GalleryId, Summary, Type, FilePath, ItemOrder) VALUES (" + GalleryId + ", '" + itemSummary[i].ToString() + "', 1, '" + folder + imageName + "', " + (i + 1) + "); ";
                                }
                                else
                                {
                                    ItemsSql += "INSERT INTO GalleryItem (GalleryId, Summary, Type, FilePath, ItemOrder) VALUES (" + GalleryId + ", '', 1, '" + folder + imageName + "', " + (i + 1) + "); ";
                                }
                            }
                            O_Gallery.InsertGalleryItems(ItemsSql);
                            #endregion
                        }
                        else if (FuVideo.HasFile)
                        {
                            #region Oluşturulan galeri video ise galeri videosu ekleniyor.
                            string VideoFolder = ConfigurationManager.AppSettings["GalleryVideoPath"] + @"\" + Helper.GetDirectory();
                            if (!Directory.Exists(VideoFolder))
                                Directory.CreateDirectory(VideoFolder);
                            string file = VideoFolder + url + "_" + GalleryId + Path.GetExtension(FuVideo.PostedFile.FileName);
                            FuVideo.SaveAs(file);
                            O_Gallery.InsertGalleryItems("INSERT INTO GalleryItem (GalleryId, Summary, Type, FilePath, ItemOrder) VALUES (" + GalleryId + ", '" + txtVideoSummary.Text + "', 0, '" + file + "', 1)");
                            #endregion
                        }
                    }
                }
            }
        }
    }
}
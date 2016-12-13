using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.News
{
    public partial class EditNews : System.Web.UI.Page
    {
        public string Baslik;
        public int NewsId;
        public string Link;
        public string AltBaslik;
        private O_News NewsOprt = new O_News();
        private O_Cross CrossOprt = new O_Cross();
        private O_Video VideoOprt = new O_Video();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["NewsId"]))
            {
                NewsId = 0;
            }
            else
            {
                NewsId = Convert.ToInt32(Request.QueryString["NewsId"]);
            }

            if (!IsPostBack)
            {
                FillFilter();

                if (NewsId == 0)
                {
                    Baslik = "Haber Ekle";
                    Link = "/News/EditNews.aspx";
                    AltBaslik = "Yeni Haber Ekle";
                    galeriEkleButon.Visible = false;

                }
                else
                {
                    galeriEkleButon.Visible = false;
                    Baslik = "Haber Düzenle";
                    Link = "/News/EditNews.aspx?NewsId=" + NewsId;
                    AltBaslik = NewsId + " ID'li Haberi Düzenle";
                    M_News N = NewsOprt.NewsDetail(NewsId);
                    txtMansetBaslik.Text = N.Title;
                    txtOzet.Text = N.Summary;
                    txtEtiketler.Text = N.NewsTags;
                    ddListKategori.SelectedValue = N.CategoryId.ToString();
                    ddListAktifmi.SelectedValue = N.Status.ToString();
                    cbYorum.Checked = N.CommentActive;
                    ckEditor.Value = N.NewsText;
                    cblDigerKategoriler = CrossOprt.ReturnDigerKategoriler(ref cblDigerKategoriler, NewsId);
                }
            }
        }

        public void FillFilter()
        {
            ddListKategori.DataSource = NewsOprt.KategoriDoldur();
            ddListKategori.DataTextField = "Name";
            ddListKategori.DataValueField = "CategoryId";
            ddListKategori.DataBind();
            ddListKategori.Items.Insert(0, new ListItem("Tüm Kategoriler", "-1"));

            cblDigerKategoriler.DataSource = NewsOprt.TumKategorileriDoldur();
            cblDigerKategoriler.DataTextField = "Name";
            cblDigerKategoriler.DataValueField = "CategoryId";
            cblDigerKategoriler.DataBind();
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            int _newsid = 0;
            string _baslik = txtMansetBaslik.Text;
            string _baslikUrl = Helper.GetUrl(txtMansetBaslik.Text);
            string _ozet = txtOzet.Text;
            string _haberMetni = ckEditor.Value;
            string _etiket = txtEtiketler.Text;
            int _anaKategori = Convert.ToInt32(ddListKategori.SelectedValue);
            int _newsType = Convert.ToInt32(ddListTip.SelectedValue);
            int _durum = Convert.ToInt32(ddListAktifmi.SelectedValue);
            int _yorum = Convert.ToInt32(cbYorum.Checked);
            string _image = "";

            //yeni haber ekleniyor
            if (NewsId == 0)
            {
                _newsid = NewsOprt.Insert(_baslik, _baslikUrl, _ozet, _haberMetni, _durum, _yorum, _etiket, _anaKategori,
                    _newsType);

                if (_newsid > 0)
                {
                    if (fuHaberGorseli.HasFile)
                    {
                        #region Haber için seçilen görselin orjinali ve varyasyonları kaydediliyor.

                        string folder = ConfigurationManager.AppSettings["NewsImagePath"] + Helper.GetDirectory();
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);

                        byte[] imageByteArray = fuHaberGorseli.FileBytes;
                        MemoryStream stream = new MemoryStream();
                        stream.Write(imageByteArray, 0, imageByteArray.Length);
                        Bitmap imageBitMap = new Bitmap(stream);
                        UploadImage uploadHaberGorseli = new UploadImage();
                        uploadHaberGorseli.SaveImageAllSize(imageBitMap, folder, _newsid.ToString(),
                            txtMansetBaslik.Text);
                        _image = folder + Helper.GetUrl(txtMansetBaslik.Text) + "_" + _newsid + ".jpg";

                        #endregion

                        NewsOprt.Update(_image, _newsid);
                    }
                }
            }
            else //haber düzenleniyor
            {

                _newsid = NewsId;
                NewsOprt.Update(_baslik, _newsid, _ozet, _haberMetni, _durum, _yorum, _etiket, _anaKategori);
                M_News N = NewsOprt.NewsDetail(_newsid);
                if (fuHaberGorseli.HasFile)
                {
                    #region Haber için seçilen görselin orjinali ve varyasyonları kaydediliyor.

                    string folder = ConfigurationManager.AppSettings["NewsImagePath"] + @"\" +
                                    N.PublishDate.ToString("yyyy/MM/dd").Replace(".", "/") + "/";
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    byte[] imageByteArray = fuHaberGorseli.FileBytes;
                    MemoryStream stream = new MemoryStream();
                    stream.Write(imageByteArray, 0, imageByteArray.Length);
                    Bitmap imageBitMap = new Bitmap(stream);
                    UploadImage uploadHaberGorseli = new UploadImage();
                    uploadHaberGorseli.SaveImageAllSize(imageBitMap, folder, _newsid.ToString(), txtMansetBaslik.Text);
                    _image = folder + Helper.GetUrl(txtMansetBaslik.Text) + "_" + _newsid + ".jpg";

                    #endregion

                    NewsOprt.Update(_image, _newsid);
                }

            }

            if (Session["VideoId"] != null)
            {
                VideoOprt.Insert(_newsid, Convert.ToInt32(Session["VideoId"]), "haber");
                NewsOprt.Update(Convert.ToInt32(Session["VideoId"]), _newsid);
            }
            CrossOprt.InsertDigerKategoriler(ref cblDigerKategoriler, _newsid);
        }

        protected void fuResim_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string dir = ConfigurationManager.AppSettings["NewsImagePath"] + @"\" + Helper.GetDirectory();
            string rnd = Helper.GetDirectoryForInline();
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            byte[] imageByteArray = e.GetContents();
            MemoryStream stream = new MemoryStream();
            stream.Write(imageByteArray, 0, imageByteArray.Length);
            Bitmap imageBitMap = new Bitmap(stream);
            System.Drawing.Image yeniImg = System.Drawing.Image.FromStream(stream);
            int height = (yeniImg.Height*640)/yeniImg.Width;
            UploadImage uploadHabericiResim = new UploadImage();
            uploadHabericiResim.SaveImageSingle(imageBitMap, dir, rnd + e.FileName, 640, height);
        }

        protected void fuVideo_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string dir = ConfigurationManager.AppSettings["NewsVideoPath"] + @"\" + Helper.GetDirectory();
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string fileName = Helper.ToEnglishWord(e.FileName);
            string filePath = dir + Helper.GetDirectoryForInline() + fileName;

            fuVideo.SaveAs(filePath);

            int videoId = VideoOprt.Insert(txtMansetBaslik.Text, txtOzet.Text, Helper.GetUrl(txtMansetBaslik.Text),
                filePath, txtEtiketler.Text, 1212);
            Session["VideoId"] = videoId;
        }

        protected void btnGaleriKaydet_Click(object sender, EventArgs e)
        {
            //if (fuGaleri.HasFile)
            //{
            //    int GalleryId = 0;
            //    using (CommonData commonData = new CommonData())
            //    {
            //        GalleryId = commonData.GetMaxId();
            //    }

            //    string folder = Utils.IDtoPath(GalleryId);
            //    using (GalleryData data = new GalleryData())
            //    {
            //        int itemId = 0;
            //        try
            //        {
            //            string[] itemSummary = txtItemSummary.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //            int itemSummaryCount = itemSummary.Length;
            //            for (int i = 0; i < FileUpload1.PostedFiles.Count; i++)
            //            {
            //                using (GalleryItem gi = new GalleryItem())
            //                {
            //                    gi.GalleryId = GalleryId;
            //                    gi.Type = 1;
            //                    gi.ItemOrder = data.MaxOrderId(GalleryId);
            //                    itemId = data.AddItem(gi);

            //                    if (i < itemSummaryCount)
            //                    {
            //                        gi.Summary = itemSummary[i].ToString();
            //                    }

            //                    if (itemId > 0)
            //                    {
            //                        gi.ItemId = itemId;

            //                        if (!Directory.Exists(Server.MapPath(folder)))
            //                        {
            //                            Directory.CreateDirectory(Server.MapPath(folder));
            //                        }
            //                        string file = folder + "/" + GalleryId + "_" + itemId + Path.GetExtension(FileUpload1.PostedFiles[i].FileName);
            //                        fuGaleri.PostedFiles[i].SaveAs(Server.MapPath(file));
            //                        gi.FilePath = file;
            //                        itemId = data.UpdateItem(gi);
            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            //AlertBox.Show(Verbs.AlertType.Error, ex.Message);
            //        }
            //    }


            //    Execute Exec = new Execute(DatabaseType.DBType1);
            //    Exec.ExecuteQuery("UPDATE News SET GalleryId = " + GalleryId + " WHERE NewsId = " + NewsId);

            //    #region habere eklenen galeri item ları için galeri oluşturuluyor
            //    int res = -1;
            //    TagList tagList = new TagList();
            //    tagList.AddRange(txtTags.Text.Trim().Split(',').Select(s => new Tag(s)));
            //    if (GalleryId > 0)
            //    {
            //        Gallery g = new Gallery
            //        {
            //            GalleryId = GalleryId,
            //            Category = new Category(int.Parse(drpCategory.SelectedValue)),
            //            Title = txtTitle.Text.Trim(),
            //            ShortTitle = txtShortTitle.Text,
            //            Weight = Convert.ToInt32(0),
            //            MainPageStatus = chkMainPage.Checked,
            //            Status = Convert.ToByte(drpActive.SelectedValue),
            //            Type = Convert.ToByte(1),
            //            SortingDate = DateTime.Today,
            //            PublishDate = DateTime.Now,
            //            Tags = tagList
            //        };
            //        int nGalId = 0;
            //        if (int.TryParse("0", out nGalId))
            //        {
            //            g.NextGalleryId = nGalId;
            //        }

            //        using (GalleryData data = new GalleryData())
            //        {
            //            res = data.Add(g);
            //        }
            //        UpdateMainPages(res);
            //    }
            //    #endregion

            //    News n = GetViewState<News>("News");

            //    //WebClient client=new WebClient();
            //    //Stream stream = client.OpenRead(n.GetImageUrl());

            //    Bitmap bmpNewsImage1 = new Bitmap(new WebClient().OpenRead(n.GetImageUrl()));
            //    Bitmap bmpNewsImage2 = new Bitmap(new WebClient().OpenRead(n.GetImageUrl("_720_400")));


            //    #region oluşturulan galeri için haber resmi galeri kapağı olarak kaydediliyor.
            //    string haberFolder = Utils.IDtoPath(NewsId);
            //    string haberGorseli = haberFolder + "/" + NewsId + ".jpg";
            //    string haberGorseliBuyuk = haberFolder + "/" + NewsId + "_720_400.jpg";

            //    string galeriFolder = folder;
            //    string galeriGorseli = galeriFolder + "/" + GalleryId + ".jpg";
            //    string galeriGorseliBuyuk = galeriFolder + "/" + GalleryId + "_720_400.jpg";

            //    bmpNewsImage1.Save(Server.MapPath(galeriGorseli));
            //    bmpNewsImage2.Save(Server.MapPath(galeriGorseliBuyuk));

            //    #endregion
            //    Response.Redirect("/CMS/News/EditNews.aspx?newsId=" + NewsId);
            //}
        }
    }
}
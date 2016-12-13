using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace MadamRozikaPanel.CrossCuttingLayer
{
    /// <summary>
    /// Summary description for UploadImage
    /// </summary>
    public class UploadImage
    {
        public void SaveImage(string ImageUrl, string ImageSavePath, string ImageName, int NewsId)
        {
            ImageResize resize =
                new ImageResize(new MetaData
                {
                    Title = ImageName.Replace("-", " "),
                    CopyRight = "www.MadamRozika.com",
                    Description = ImageName.Replace("-", " "),
                    Rating = 4
                });
            Bitmap bmp = new Bitmap(ImageUrl);

            resize.Resize(bmp, ImageSavePath, ImageName + "_k.jpg", 100, 100, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_95.jpg", 95, 95, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_126.jpg", 126, 126, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_131.jpg", 131, 131, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_146.jpg", 146, 146, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_190.jpg", 190, 190, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_120.jpg", 120, 67, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_170.jpg", 170, 100, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_300.jpg", 292, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_400.jpg", 367, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_450.jpg", 450, 350, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_b.jpg", 500, ImageResize.Quality.High, "Haberler.com");
            resize.Resize(bmp, ImageSavePath, ImageName + "_507.jpg", 507, 267, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_90.jpg", 90, 60, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_300y.jpg", 300, 200, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_266.jpg", 266, 147, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_200.jpg", 200, 111, ImageResize.Quality.High);
            resize.Resize(bmp, ImageSavePath, ImageName + "_ov.jpg", 640, 360, ImageResize.Quality.High,
                @"C:\videoIcon\videoicon.png");
            resize.Resize(bmp, ImageSavePath, ImageName + "_400v.jpg", 400, ImageResize.Quality.High,
                @"C:\videoIcon\videoicon.png", true);


        }

        public void SaveImageSingle(Bitmap bitImage, string ImageSavePath, string ImageName, int width, int height)
        {
            ImageResize resize = new ImageResize();
            resize.Resize(bitImage, ImageSavePath, ImageName, width, height, ImageResize.Quality.High);
            bitImage.Dispose();
        }

        public void SaveImageAllSize(Bitmap bitImage, string ImageSavePath, string id, string Title)
        {
            ImageResize resize = new ImageResize();
            resize.Resize(bitImage, ImageSavePath, Helper.GetUrl(Title) + "_" + id + "_1250_550.jpg", 1250, 550,
                ImageResize.Quality.High);
            resize.Resize(bitImage, ImageSavePath, Helper.GetUrl(Title) + "_" + id + "_330_242.jpg", 330, 242,
                ImageResize.Quality.High);
            resize.Resize(bitImage, ImageSavePath, Helper.GetUrl(Title) + "_" + id + "_510_187.jpg", 510, 187,
                ImageResize.Quality.High);
            resize.Resize(bitImage, ImageSavePath, Helper.GetUrl(Title) + "_" + id + "_100_100.jpg", 100, 100,
                ImageResize.Quality.High);
            bitImage.Save(ImageSavePath + Helper.GetUrl(Title) + "_" + id + ".jpg");
            bitImage.Dispose();
        }

        public string GetZImage(string OriginalImagePath, string Baslik, string UstBaslik)
        {
            Log.Yaz("Z RESİMLER BAŞLADI: BAŞLIK: " + Baslik + " ÜST BAŞLIK : " + UstBaslik);
            ZImage zImage = new ZImage();
            return zImage.GetImages(OriginalImagePath, Baslik, UstBaslik);
        }
    }

    public class ImageResize
    {
        private double imageOran;
        private int newWidth = 0;
        private int newHeight = 0;
        private Bitmap newBitmap;
        private MetaData _MetaData;

        public ImageResize()
        {
            //Initialize();
        }

        public ImageResize(MetaData metaData)
        {
            _MetaData = metaData;
        }

        /// <summary>
        /// Gönderilen width değerine göre genişlik ayarlanır ve yükseklik uygun boyuta getirilir.
        /// </summary>
        /// <param name="orijinalBitmap"></param>
        /// <param name="Width"></param>
        /// <param name="ImageName"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public Size Resize(Bitmap orijinalBitmap, string SavePath, string ImageName, int Width, Quality quality)
        {
            try
            {
                Size size;
                if (orijinalBitmap.Width != Width)
                {
                    imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                    newWidth = Width;
                    newHeight = (int)(orijinalBitmap.Height / imageOran);
                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);
                    newBitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    //MagickImage(SavePath + ImageName);
                    size = newBitmap.Size;
                    newBitmap.Dispose();
                    if (_MetaData != null)
                    {
                        //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                        //j.add(_MetaData);
                    }
                    return size;
                }
                else
                {
                    orijinalBitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg),
                        GetEncoderParameters(quality));
                    size = orijinalBitmap.Size;
                    if (_MetaData != null)
                    {
                        //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                        //j.add(_MetaData);
                    }
                    return size;
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ImageName + " -- SavePath : " + SavePath + " HATA DETAYI : " + ex.ToString());
                return new Size();
            }
        }

        public Size Resize(Bitmap orijinalBitmap, string SavePath, string ImageName, int Width, Quality quality,
            string pngPath, bool video)
        {
            try
            {
                Size size;
                Bitmap png = new Bitmap(pngPath);
                if (orijinalBitmap.Width != Width)
                {
                    imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                    newWidth = Width;
                    newHeight = (int)(orijinalBitmap.Height / imageOran);
                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);
                    Graphics g = Graphics.FromImage(newBitmap);
                    g.DrawImage(png,
                        new Rectangle((newBitmap.Width / 2) - (png.Width / 2), (newBitmap.Height / 2) - (png.Height / 2),
                            png.Width, png.Height));
                    newBitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    size = newBitmap.Size;
                    newBitmap.Dispose();
                    if (_MetaData != null)
                    {
                        try
                        {
                            //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                            //j.add(_MetaData);
                        }
                        catch
                        {
                        }
                    }
                    return size;
                }
                else
                {
                    Graphics g = Graphics.FromImage(orijinalBitmap);
                    g.DrawImage(png,
                        new Rectangle((orijinalBitmap.Width / 2) - (png.Width / 2),
                            (orijinalBitmap.Height / 2) - (png.Height / 2), png.Width, png.Height));
                    orijinalBitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg),
                        GetEncoderParameters(quality));
                    size = orijinalBitmap.Size;
                    try
                    {
                        //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                        //j.add(_MetaData);
                    }
                    catch
                    {
                    }
                    return size;
                }
                //png.Dispose();
            }
            catch (Exception ex)
            {
                Log.Yaz(ImageName + " -- SavePath : " + SavePath + " HATA DETAYI : " + ex.ToString());
                return new Size();
            }
        }

        /// <summary>
        /// Gönderilen height değerine göre yükseklik ayarlanır ve genişlik uygun boyuta getirilir.
        /// </summary>
        /// <param name="orijinalBitmap"></param>
        /// <param name="Width"></param>
        /// <param name="ImageName"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public Size Resize(Bitmap orijinalBitmap, string SavePath, string ImageName, int Height, Quality quality,
            string WriteLogo = "")
        {
            try
            {
                Size size;
                if (orijinalBitmap.Height != Height)
                {
                    imageOran = Math.Round(orijinalBitmap.Height / (double)Height, 2);
                    newWidth = (int)(orijinalBitmap.Width / imageOran);
                    newHeight = Height;
                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);
                    if (WriteLogo != "")
                    {
                        Graphics g = Graphics.FromImage(newBitmap);
                        g.DrawString(WriteLogo, new Font("Arial", 12, FontStyle.Bold), Brushes.White,
                            new PointF(10, newBitmap.Height - 20));
                    }
                    GetGraphic(ref newBitmap, ImageResize.Quality.High);
                    newBitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    size = newBitmap.Size;
                    newBitmap.Dispose();
                    if (_MetaData != null)
                    {
                        try
                        {
                            //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                            //j.add(_MetaData);
                        }
                        catch
                        {
                        }
                    }
                    return size;
                }
                else
                {
                    orijinalBitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg),
                        GetEncoderParameters(quality));
                    if (_MetaData != null)
                    {
                        try
                        {
                            //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                            //j.add(_MetaData);
                        }
                        catch
                        {
                        }
                    }
                    size = orijinalBitmap.Size;
                    return size;
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ImageName + " -- SavePath : " + SavePath + " HATA DETAYI : " + ex.ToString());
                return new Size();
            }
        }

        /// <summary>
        /// Gönderilen width ve height değerlerine göre boyutlandırılır.Gerekirse resim kırpılır.
        /// </summary>
        /// <param name="orijinalBitmap"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="ImageName"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public Size Resize(Bitmap orijinalBitmap, string SavePath, string ImageName, int Width, int Height,
            Quality quality)
        {
            try
            {
                if (orijinalBitmap.Width > orijinalBitmap.Height || (Width < 640))
                {
                    imageOran = Math.Round(orijinalBitmap.Height / (double)Height, 2);
                    int x, y = 0;
                    if ((orijinalBitmap.Width / imageOran) >= Width)
                    {
                        newWidth = (int)(Math.Round(orijinalBitmap.Width / imageOran));
                        newHeight = Height;
                        x = (newWidth - Width) / 2;
                        y = 0;
                    }
                    else
                    {
                        imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                        newWidth = Width;
                        newHeight = (int)(Math.Round(orijinalBitmap.Height / imageOran));
                        x = 0;
                        y = ((newHeight - Height) / 100) * 15;
                    }

                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);
                    Bitmap bitmap = new Bitmap(Width, Height);
                    if (Height > newHeight)
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, Width, newHeight), newBitmap.PixelFormat);
                    }
                    else if (Width > newWidth)
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, newWidth, Height), newBitmap.PixelFormat);
                    }
                    else
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, Width, Height), newBitmap.PixelFormat);
                    }

                    bitmap = new Bitmap(bitmap, Width, Height);
                    //GetGraphic(ref bitmap, ImageResize.Quality.High);
                    bitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    //MagickImage(SavePath + ImageName, bitmap);

                    Size size = bitmap.Size;
                    bitmap.Dispose();
                    try
                    {
                        //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                        //j.add(_MetaData);
                    }
                    catch
                    {
                    }
                    return size;
                }
                else
                {
                    imageOran = Math.Round(orijinalBitmap.Height / (double)Height, 2);
                    newHeight = Height;
                    newWidth = (int)(Math.Round(orijinalBitmap.Width / imageOran));
                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);

                    Bitmap bitmap2 = new Bitmap(Width, Height);
                    Graphics g = Graphics.FromImage((Image)bitmap2);
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //g.Clear(Color.White);
                    g.DrawImage((Image)newBitmap, (Width - newWidth) / 2, 0, newWidth, newHeight);
                    bitmap2.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    Size size = bitmap2.Size;
                    bitmap2.Dispose();
                    try
                    {
                        //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                        //j.add(_MetaData);
                    }
                    catch
                    {
                    }
                    return size;

                    /*imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                newWidth = Width;
                newHeight = (int)(Math.Round(orijinalBitmap.Height / imageOran));
                x = 0;
                //y = (newHeight - Height) / 2;
                //y = 0;
                //if (newHeight > Height)
                y = ((newHeight - Height) / 100) * 15;*/
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ImageName + " -- SavePath : " + SavePath + " HATA DETAYI : " + ex.ToString());
                return new Size();
            }
        }

        public Size Resize(Bitmap orijinalBitmap, string SavePath, string ImageName, int Width, int Height,
            Quality quality, string PngPath)
        {
            try
            {
                Bitmap png = new Bitmap(PngPath);
                if (orijinalBitmap.Width > orijinalBitmap.Height || (Width < 640))
                {
                    imageOran = Math.Round(orijinalBitmap.Height / (double)Height, 2);
                    int x, y = 0;
                    if ((orijinalBitmap.Width / imageOran) >= Width)
                    {
                        newWidth = (int)(Math.Round(orijinalBitmap.Width / imageOran));
                        newHeight = Height;
                        x = (newWidth - Width) / 2;
                        y = 0;
                    }
                    else
                    {
                        imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                        newWidth = Width;
                        newHeight = (int)(Math.Round(orijinalBitmap.Height / imageOran));
                        x = 0;
                        y = ((newHeight - Height) / 100) * 15;
                    }

                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);
                    Bitmap bitmap = new Bitmap(Width, Height);
                    if (Height > newHeight)
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, Width, newHeight), newBitmap.PixelFormat);
                    }
                    else if (Width > newWidth)
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, newWidth, Height), newBitmap.PixelFormat);
                    }
                    else
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, Width, Height), newBitmap.PixelFormat);
                    }

                    bitmap = new Bitmap(bitmap, Width, Height);
                    GetGraphic(ref bitmap, ImageResize.Quality.High);
                    Graphics gg = Graphics.FromImage(bitmap);

                    gg.DrawImage(png,
                        new Rectangle((bitmap.Width / 2) - (png.Width / 2), (bitmap.Height / 2) - (png.Height / 2), png.Width,
                            png.Height));
                    bitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    Size size = bitmap.Size;
                    bitmap.Dispose();
                    if (_MetaData != null)
                    {
                        try
                        {
                            //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                            //j.add(_MetaData);
                        }
                        catch
                        {
                        }
                    }
                    return size;
                }
                else
                {
                    imageOran = Math.Round(orijinalBitmap.Height / (double)Height, 2);
                    newHeight = Height;
                    newWidth = (int)(Math.Round(orijinalBitmap.Width / imageOran));
                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);

                    Bitmap bitmap2 = new Bitmap(Width, Height);
                    Graphics g = Graphics.FromImage((Image)bitmap2);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.Clear(Color.White);
                    g.DrawImage((Image)newBitmap, (Width - newWidth) / 2, 0, newWidth, newHeight);
                    g.DrawImage(png,
                        new Rectangle((bitmap2.Width / 2) - (png.Width / 2), (bitmap2.Height / 2) - (png.Height / 2), png.Width,
                            png.Height));
                    bitmap2.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    Size size = bitmap2.Size;
                    bitmap2.Dispose();
                    if (_MetaData != null)
                    {
                        try
                        {
                            //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                            //j.add(_MetaData);
                        }
                        catch
                        {
                        }
                    }
                    return size;

                    /*imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                newWidth = Width;
                newHeight = (int)(Math.Round(orijinalBitmap.Height / imageOran));
                x = 0;
                //y = (newHeight - Height) / 2;
                //y = 0;
                //if (newHeight > Height)
                y = ((newHeight - Height) / 100) * 15;*/
                }
                png.Dispose();
            }
            catch (Exception ex)
            {
                Log.Yaz(ImageName + " -- SavePath : " + SavePath + " HATA DETAYI : " + ex.ToString());
                return new Size();
            }
        }

        public Bitmap GetBitmap(Bitmap orijinalBitmap, string SavePath, string ImageName, int Width, int Height,
            Quality quality)
        {
            try
            {
                if (orijinalBitmap.Width > orijinalBitmap.Height || (Width < 640))
                {
                    imageOran = Math.Round(orijinalBitmap.Height / (double)Height, 2);
                    int x, y = 0;
                    if ((orijinalBitmap.Width / imageOran) >= Width)
                    {
                        newWidth = (int)(Math.Round(orijinalBitmap.Width / imageOran));
                        newHeight = Height;
                        x = (newWidth - Width) / 2;
                        y = 0;
                    }
                    else
                    {
                        imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                        newWidth = Width;
                        newHeight = (int)(Math.Round(orijinalBitmap.Height / imageOran));
                        x = 0;
                        //y = (newHeight - Height) / 2;
                        //y = 0;
                        //if (newHeight > Height)
                        y = ((newHeight - Height) / 100) * 15;
                    }

                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);
                    Bitmap bitmap = new Bitmap(Width, Height);
                    if (Height > newHeight)
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, Width, newHeight), newBitmap.PixelFormat);
                    }
                    else if (Width > newWidth)
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, newWidth, Height), newBitmap.PixelFormat);
                    }
                    else
                    {
                        bitmap = newBitmap.Clone(new Rectangle(x, y, Width, Height), newBitmap.PixelFormat);
                    }
                    bitmap = new Bitmap(bitmap, Width, Height);
                    GetGraphic(ref bitmap, ImageResize.Quality.High);
                    //bitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    return bitmap;
                }
                else
                {
                    imageOran = Math.Round(orijinalBitmap.Height / (double)Height, 2);
                    newHeight = Height;
                    newWidth = (int)(Math.Round(orijinalBitmap.Width / imageOran));
                    newBitmap = new Bitmap(orijinalBitmap, newWidth, newHeight);

                    Bitmap bitmap2 = new Bitmap(Width, Height);
                    Graphics g = Graphics.FromImage((Image)bitmap2);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.Clear(Color.White);
                    g.DrawImage((Image)newBitmap, (Width - newWidth) / 2, 0, newWidth, newHeight);
                    //bitmap2.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                    return bitmap2;

                    /*imageOran = Math.Round(orijinalBitmap.Width / (double)Width, 2);
                newWidth = Width;
                newHeight = (int)(Math.Round(orijinalBitmap.Height / imageOran));
                x = 0;
                //y = (newHeight - Height) / 2;
                //y = 0;
                //if (newHeight > Height)
                y = ((newHeight - Height) / 100) * 15;*/

                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ImageName + " -- SavePath : " + SavePath + " HATA DETAYI : " + ex.ToString());
                return orijinalBitmap;
            }
        }

        /// <summary>
        /// Verilen parametrelere göre resim büzüşse de verilen boyutlara zorlanır
        /// </summary>
        /// <param name="orijinalBitmap">Orijinal Bitmap Dosyası</param>
        /// <param name="SavePath">Kayıt Yeri ör:C:\test\</param>
        /// <param name="ImageName">test.jpg</param>
        /// <param name="Width">Genişlik</param>
        /// <param name="Height">YÜkseklik</param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public Size ResizeKontrolsuz(Bitmap orijinalBitmap, string SavePath, string ImageName, int Width, int Height,
            Quality quality)
        {
            try
            {
                Bitmap bitmap = new Bitmap(orijinalBitmap, Width, Height);
                bitmap.Save(SavePath + ImageName, GetEncoder(ImageFormat.Jpeg), GetEncoderParameters(quality));
                Size size = bitmap.Size;
                orijinalBitmap.Dispose();
                bitmap.Dispose();
                if (_MetaData != null)
                {
                    try
                    {
                        //JpegMetadataAdapter j = new JpegMetadataAdapter(SavePath + ImageName);
                        //j.add(_MetaData);
                    }
                    catch
                    {
                    }
                }
                return size;
            }
            catch (Exception ex)
            {
                Log.Yaz(ImageName + " -- SavePath : " + SavePath + " HATA DETAYI : " + ex.ToString());
                return new Size();
            }
        }

        public EncoderParameters GetEncoderParameters(Quality quality)
        {
            EncoderParameters eParams;
            if (quality == Quality.High)
            {
                eParams = new EncoderParameters(4);
                eParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                eParams.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ScanMethod, 7);
                eParams.Param[2] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, 11);
                eParams.Param[3] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, 20);
            }
            else if (quality == Quality.Medium)
            {
                eParams = new EncoderParameters(5);
                eParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);
                eParams.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ScanMethod, 7);
                eParams.Param[2] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, 11);
                eParams.Param[3] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, 20);
                eParams.Param[4] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100L);
            }
            else
            {
                eParams = new EncoderParameters(5);
                eParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 68L);
                eParams.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 150L);
                eParams.Param[2] = new EncoderParameter(System.Drawing.Imaging.Encoder.ScanMethod, 7);
                eParams.Param[3] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, 11);
                eParams.Param[4] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, 20);
            }
            return eParams;
        }

        public ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public Bitmap GetGraphic(ref Bitmap bitmap, Quality quality)
        {
            Graphics graphic = Graphics.FromImage((bitmap));
            if (quality == Quality.High)
            {
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            }
            else
            {
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            }
            return bitmap;
        }

        public enum Quality
        {
            High,
            Medium,
            Low
        }
    }

    public class ZImage
    {
        //public static WebClient client = new WebClient();
        //public ZImage()
        //{
        //    NetworkCredential c = new NetworkCredential("resim", "ym_rrr8080.");
        //    client.Credentials = c;
        //}

        public string GetImages(string OriginalImagePath, string Baslik, string UstBaslik = "")
        {
            //Log.Yaz("Z GetImages BAŞLADI: BAŞLIK: " + Baslik + " ÜST BAŞLIK : " + UstBaslik);
            Bitmap bmp = new Bitmap(OriginalImagePath);
            ImageResize image =
                new ImageResize(new MetaData
                {
                    CopyRight = "Haberler.com",
                    Description = Baslik,
                    Title = Baslik,
                    Rating = 5
                });
            image.Resize(bmp, OriginalImagePath.Replace(".jpg", "_316.jpg"), "", 316, 343, ImageResize.Quality.High);
            //UstSolDar(new Bitmap(client.OpenRead(OriginalImagePath)), OriginalImagePath, Baslik, UstBaslik);
            //AltYayik(new Bitmap(client.OpenRead(OriginalImagePath)), OriginalImagePath, Baslik, UstBaslik);
            //AltSol(new Bitmap(client.OpenRead(OriginalImagePath)), OriginalImagePath, Baslik, UstBaslik);
            //AltSag(new Bitmap(client.OpenRead(OriginalImagePath)), OriginalImagePath, Baslik, UstBaslik);
            //UstSol(new Bitmap(client.OpenRead(OriginalImagePath)), OriginalImagePath, Baslik, UstBaslik);
            //Ustsag(new Bitmap(client.OpenRead(OriginalImagePath)), OriginalImagePath, Baslik, UstBaslik);
            //UstSagDar(new Bitmap(client.OpenRead(OriginalImagePath)), OriginalImagePath, Baslik, UstBaslik);
            UstSolDar(new Bitmap(OriginalImagePath), OriginalImagePath, Baslik, UstBaslik);
            AltYayik(new Bitmap(OriginalImagePath), OriginalImagePath, Baslik, UstBaslik);
            AltSol(new Bitmap(OriginalImagePath), OriginalImagePath, Baslik, UstBaslik);
            AltSag(new Bitmap(OriginalImagePath), OriginalImagePath, Baslik, UstBaslik);
            UstSol(new Bitmap(OriginalImagePath), OriginalImagePath, Baslik, UstBaslik);
            Ustsag(new Bitmap(OriginalImagePath), OriginalImagePath, Baslik, UstBaslik);
            UstSagDar(new Bitmap(OriginalImagePath), OriginalImagePath, Baslik, UstBaslik);
            return OriginalImagePath.Replace(".jpg", "_Z1.jpg");
        }

        private void Rectangle(Bitmap bmp, string savePath, List<string> List, Rectangle Altrec, string UstBaslik = "",
            int AltBaslikFont = 18, string Cizgi = "Alt", int alpha = 245)
        {
            try
            {
                foreach (var item in List)
                {
                    Log.Yaz("Z Rectangle BAŞLADI: BAŞLIK: " + item + " ÜST BAŞLIK : " + UstBaslik);
                }

                Brush brush = new SolidBrush(Color.FromArgb(alpha, Color.Black));
                Graphics g = Graphics.FromImage(bmp);

                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                if (!string.IsNullOrEmpty(UstBaslik))
                {
                    int width = UstBaslik.Width(new Font("Arial", 12, FontStyle.Bold));
                    g.FillRectangle(new SolidBrush(Color.Red), Altrec.X, Altrec.Y - 35 - 3, width + 10, 35);
                    g.DrawString(UstBaslik, new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.White),
                        Altrec.X + 5, Altrec.Y - 35 + 5);
                }

                g.FillRectangle(brush, Altrec);

                if (Cizgi == "Alt")
                    g.FillRectangle(new SolidBrush(Color.Red), Altrec.X, Altrec.Y + Altrec.Height, Altrec.Width, 4);
                else if (Cizgi == "Ust")
                    g.FillRectangle(new SolidBrush(Color.White), Altrec.X, Altrec.Y - 3, Altrec.Width, 3);

                int sayac = 0;
                foreach (var item in List)
                {
                    g.DrawString(item, new Font("Arial", AltBaslikFont, FontStyle.Bold), new SolidBrush(Color.White),
                        Altrec.X + 5, Altrec.Y + 10 + sayac);
                    sayac += 30;
                }

                bmp.Save(savePath, GetEncoder(ImageFormat.Jpeg), this.GetEncoderParameters());
                bmp.Dispose();

                try
                {
                    //JpegMetadataAdapter j = new JpegMetadataAdapter(savePath);
                    //j.add(new MetaData { CopyRight = "Haberler.com", Description = List.ListToString(), Title = List.ListToString(), Rating = 5 });
                }
                catch
                {
                }

            }
            catch (Exception ex)
            {
                //Log.Yaz(ex.ToString());
            }
        }

        public EncoderParameters GetEncoderParameters()
        {
            EncoderParameters eParams;
            eParams = new EncoderParameters(5);
            eParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eParams.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ScanMethod, 7);
            eParams.Param[2] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, 11);
            eParams.Param[3] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, 20);
            eParams.Param[4] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100L);
            return eParams;
        }

        public ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public List<string> GetMetin(string Metin, Font font, int Width)
        {
            string _metin = Metin;
            List<string> MetinList = new List<string>();
            if (_metin.Width(font) > Width)
            {
                string[] _metinSplit = _metin.Split(' ');
                string metinBirlestir = "";
                for (int i = 0; i < _metinSplit.Length; i++)
                {
                    if (string.IsNullOrEmpty(_metinSplit[i]))
                        continue;

                    string metinGecici = metinBirlestir;
                    metinBirlestir = metinBirlestir + " " + _metinSplit[i];
                    if (metinBirlestir.Width(font) < Width)
                    {
                        if (_metinSplit.Length == i + 1)
                        {
                            MetinList.Add(metinBirlestir.Trim());
                        }
                    }
                    else
                    {
                        MetinList.Add(metinGecici.Trim());
                        if (_metinSplit.Length == i + 1)
                        {
                            MetinList.Add(_metinSplit[i]);
                            // i--;
                            metinBirlestir = "";
                            if (i < 0)
                            {
                                return new List<string>() { Metin };
                            }
                        }
                        else
                        {
                            metinBirlestir = _metinSplit[i];
                        }
                    }
                }
            }
            else
            {
                MetinList = new List<string>() { Metin };
            }
            List<string> newList = new List<string>();
            foreach (var item in MetinList)
            {
                if (!string.IsNullOrEmpty(item))
                    newList.Add(item);
            }

            return newList;
        }

        public void AltYayik(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), bmp.Width - 10).Cut(3);
                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z1.jpg"), list,
                        new Rectangle(10, bmp.Height - 63, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        10,
                        bmp.Height - (40 * list.Count) - 13,
                        bmp.Width - 20,
                        40 * list.Count);
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z1.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
            finally
            {
                bmp.Dispose();
            }
        }

        public void AltSol(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), 370).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z2.jpg"), list,
                        new Rectangle(10, 280, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        10,
                        bmp.Height - (40 * list.Count) - 13,
                        list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold)) + 5,
                        (40 * list.Count));
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z2.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }

        public void AltSag(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), 370).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z3.jpg"), list,
                        new Rectangle(bmp.Width - genislik - 10, 280, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        bmp.Width - (list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold))) - 15,
                        bmp.Height - (40 * list.Count) - 13,
                        list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold)) + 5,
                        40 * list.Count);
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z3.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }

        public void UstSol(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), 370).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z4.jpg"), list,
                        new Rectangle(10, 70, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        10,
                        70,
                        list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold)),
                        40 * list.Count);
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z4.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }

        public void UstSolDar(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                Log.Yaz("Z UstSolDar BAŞLADI: BAŞLIK: " + AltBaslik + " ÜST BAŞLIK : " + UstBaslik);
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), 200).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z5.jpg"), list,
                        new Rectangle(10, 70, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        10,
                        70,
                        list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold)) + 5,
                        40 * list.Count);
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z5.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }

        public void Ustsag(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), 370).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z6.jpg"), list,
                        new Rectangle(bmp.Width - genislik - 10, 70, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        bmp.Width - (list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold))) - 15,
                        70,
                        list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold)) + 5,
                        40 * list.Count);
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z6.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }

        public void UstSagDar(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), 200).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z7.jpg"), list,
                        new Rectangle(bmp.Width - genislik - 15, 70, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        bmp.Width - (list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold))) - 15,
                        70,
                        list.ToArray().GetLongWidth(new Font("Arial", 18, FontStyle.Bold)) + 5,
                        40 * list.Count);
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z7.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }

        public void UstOrta(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), bmp.Width - 10).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z8.jpg"), list,
                        new Rectangle((bmp.Width - genislik) / 2, 43, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        5,
                        43,
                        bmp.Width - 10,
                        40 * list.Count);
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z8.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }

        public void AltOrta(Bitmap bmp, string OriginalImagePath, string AltBaslik, string UstBaslik = "")
        {
            try
            {
                int genislik = AltBaslik.Width(new Font("Arial", 18, FontStyle.Bold));
                List<string> list = GetMetin(AltBaslik, new Font("Arial", 18, FontStyle.Bold), bmp.Width - 10).Cut(6);

                if (list.Count == 1)
                {
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z9.jpg"), list,
                        new Rectangle((bmp.Width - genislik) / 2, 280, genislik + 5, 50), UstBaslik);
                }
                else
                {
                    Rectangle r = new Rectangle(
                        5,
                        bmp.Height - (40 * list.Count) - 13,
                        bmp.Width - 10,
                        (40 * list.Count));
                    Rectangle(bmp, OriginalImagePath.Replace(".jpg", "_Z9.jpg"), list, r, UstBaslik);
                }
            }
            catch (Exception ex)
            {
                Log.Yaz(ex.ToString());
            }
        }
    }


    public static class extendions
    {
        public static int Width(this string Metin, Font font)
        {
            string _metin = Metin;
            Graphics g = Graphics.FromImage(new Bitmap(664, 343));
            return (int)g.MeasureString(Metin, font).Width;
        }

        public static List<string> Cut(this List<string> List, int count)
        {
            if (List.Count <= count)
            {
                return List;
            }
            else
            {
                List<string> List2 = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    List2.Add(List[i]);
                }
                List2[List2.Count - 1] = List2[List2.Count - 1] + "..";
                return List2;
            }
        }

        public static int GetLongWidth(this string[] arr, Font font)
        {
            string temp = "";
            for (int write = 0; write < arr.Length; write++)
            {
                for (int sort = 0; sort < arr.Length - 1; sort++)
                {
                    if (arr[sort].Width(font) < arr[sort + 1].Width(font))
                    {
                        temp = arr[sort + 1];
                        arr[sort + 1] = arr[sort];
                        arr[sort] = temp;
                    }
                }
            }
            return arr[0].Width(font);
        }

        public static void ToDeleteFile(this string s)
        {
            try
            {
                File.Delete(s);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// String bir listeyi aralarda boşluk bırakacak şekilde tek satır string'e çevirir
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public static string ListToString(this List<string> List)
        {
            List<string> _List = List;
            string _str = "";
            foreach (var item in _List)
            {
                _str += item + " ";
            }
            return _str;
        }

        /// <summary>
        /// Manşet resminin fiziksel yolunu virtual yola çevirir
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertMansetPathToVirtual(this string s)
        {
            string _s = s;
            string _match = Regex.Match(s, @"\\[0-9]+\\.*?\.jpg").Value.Replace("\\", "/");

            if (string.IsNullOrEmpty(_match))
                return "";

            _s = "http://img.haberler.com/manset" + _match;
            return _s;
        }

        /// <summary>
        /// Haber resiminin fiziksel yolunu virtual yola çevirir
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertHaberImagePathToVirtual(this string s)
        {
            string _s = s;
            string _match = Regex.Match(s, @"\\[0-9]{3,}\\.*?\.jpg").Value.Replace("\\", "/");

            if (string.IsNullOrEmpty(_match))
                return "";

            _s = "http://img.haberler.com/haber" + _match;
            return _s;
        }
    }

    public class Log
    {
        private static object state = new object();

        public static void Yaz(string Hata)
        {
            try
            {
                FileStream F =
                    new FileStream(@"C:\ImageUploaderLogs\Logs" + DateTime.Now.ToString("ddMMyyy-HH") + ".txt",
                        FileMode.Append);
                StreamWriter sw = new StreamWriter(F);
                lock (state)
                {
                    sw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " --> " + Hata + "\n");
                    sw.WriteLine();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch
            {

            }
        }
    }

    //[ComVisible(true)]
    //public class JpegMetadataAdapter
    //{
    //    private readonly string path;
    //    private BitmapFrame frame;
    //    public readonly BitmapMetadata Metadata;

    //    public JpegMetadataAdapter(string path)
    //    {
    //        try
    //        {
    //            this.path = path;
    //            frame = getBitmapFrame(path);
    //            Metadata = (BitmapMetadata)frame.Metadata.Clone();
    //        }
    //        catch { }
    //    }

    //    public void add(MetaData meta)
    //    {
    //        try
    //        {
    //            Metadata.Title = meta.Title;
    //            Metadata.Copyright = meta.CopyRight;
    //            Metadata.Author = new System.Collections.ObjectModel.ReadOnlyCollection<string>(new List<string> { meta.CopyRight });
    //            Metadata.Comment = meta.Description;
    //            //Metadata.DateTaken = DateTime.Now.ToString();
    //            Metadata.Subject = meta.Description;
    //            Metadata.Rating = meta.Rating;
    //            Metadata.Keywords = new System.Collections.ObjectModel.ReadOnlyCollection<string>(new List<string> { "haber", "haberi", meta.Title.ToLower() + " haberi" });
    //            Save();
    //            //SaveAs(path);
    //        }
    //        catch { }
    //    }
    //    public void Save()
    //    {
    //        SaveAs(path);
    //    }
    //    public void SaveAs(string path)
    //    {
    //        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
    //        encoder.Frames.Add(BitmapFrame.Create(frame, frame.Thumbnail, Metadata, frame.ColorContexts));
    //        using (Stream stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite))
    //        {
    //            encoder.Save(stream);
    //        }
    //    }
    //    private BitmapFrame getBitmapFrame(string path)
    //    {
    //        BitmapDecoder decoder = null;
    //        using (Stream stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
    //        {
    //            decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
    //        }
    //        return decoder.Frames[0];
    //    }
    //}

    public class MetaData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string CopyRight { get; set; }
        public int Rating { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;


public class M_News
{
    public M_News()
    {

    }
    public M_News(DataRow dr)
    {
        NewsId = Convert.ToInt32(dr["NewsId"]);
        Title = dr["Title"].ToString();
        TitleUrl = dr["TitleUrl"].ToString();
        Status = Convert.ToInt32(dr["Status"]);
        Summary = dr["Summary"].ToString();
        NewsText = dr["NewsText"].ToString();
        ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
        PublishDate = Convert.ToDateTime(dr["PublishDate"]);
        CommentActive = Convert.ToBoolean(dr["CommentActive"]);
        Hit = Convert.ToInt32(dr["Hit"]);
        GalleryId = Convert.ToInt32(dr["GalleryId"]);
        VideoId = Convert.ToInt32(dr["VideoId"]);
        ImageUrl = dr["ImageUrl"].ToString();
        NewsTags = dr["NewsTags"] != DBNull.Value ? dr["NewsTags"].ToString() : "";
        CategoryId = Convert.ToInt32(dr["CategoryId"]);
        NewsType = Convert.ToInt32(dr["NewsType"]);
    }
    #region Properties
    public int NewsId { get; set; }
    public string Title { get; set; }
    public string TitleUrl { get; set; }
    public int Status { get; set; }
    public string Summary { get; set; }
    public string NewsText { get; set; }
    public DateTime ModifiedDate { get; set; }
    public DateTime PublishDate { get; set; }
    public bool CommentActive { get; set; }
    public int Hit { get; set; }
    public int GalleryId { get; set; }
    public int VideoId { get; set; }
    public string NewsTags { get; set; }
    public int CategoryId { get; set; }
    public string ImageUrl { get; set; }
    public int NewsType { get; set; }
    #endregion
}
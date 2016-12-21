using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Headlines
/// </summary>
public class M_Headlines
{
    public M_Headlines()
    {
    }
    public M_Headlines(DataRow dr)
    {
        HeadlineId = Convert.ToInt32(dr["HeadlineId"]);
        ObjectId = Convert.ToInt32(dr["ObjectId"]);
        ObjectType = dr["ObjectType"].ToString();
        Url = dr["Url"].ToString();
        Title = dr["Title"].ToString();
        Ip = dr["Ip"].ToString();
        Description = dr["Description"].ToString();
        ImageUrl = dr["ImageUrl"].ToString();
        ImageUrlPrivate = dr["ImageUrlPrivate"].ToString();
        Site = dr["Site"].ToString();
        Status = Convert.ToInt32(dr["Status"]);
        Rank = Convert.ToInt32(dr["Rank"]);
        DateTime = Convert.ToDateTime(dr["DateTime"]);
    }
    #region Properties
    public int HeadlineId { get; set; }
    public int ObjectId { get; set; }
    public string ObjectType { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string Ip { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string ImageUrlPrivate { get; set; }
    public string Site { get; set; }
    public int Status { get; set; }
    public int Rank { get; set; }
    public DateTime DateTime { get; set; }
    #endregion
}
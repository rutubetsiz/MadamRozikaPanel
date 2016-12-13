using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for Comments
/// </summary>
public class M_Comments
{
    public M_Comments()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public M_Comments(DataRow dr)
    {
        CommentId = Convert.ToInt32(dr["CommentId"]);
        ContentId = Convert.ToInt32(dr["ContentId"]);
        UserName = dr["UserName"].ToString();
        Text = dr["Text"].ToString();
        IsActive = Convert.ToInt32(dr["IsActive"]);
        RecordDate = Convert.ToDateTime(dr["RecordDate"]);
        IpAddress = dr["IpAddress"].ToString();
        LikeCount = Convert.ToInt32(dr["LikeCount"]);
        UnlikeCount = Convert.ToInt32(dr["UnlikeCount"]);
        ParentId = Convert.ToInt32(dr["ParentId"]);
        Type = dr["Type"].ToString();
        Device = dr["Device"].ToString();
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(dr["Sites"].ToString());
        Sites = xml.SelectNodes("/sites/site");
    }

    #region Properties
    public int CommentId { get; set; }
    public int ContentId { get; set; }
    public string UserName { get; set; }
    public string Text { get; set; }
    public int IsActive { get; set; }
    public DateTime RecordDate { get; set; }
    public string IpAddress { get; set; }
    public int LikeCount { get; set; }
    public int UnlikeCount { get; set; }
    public int ParentId { get; set; }
    public string Type { get; set; }
    public string Device { get; set; }
    public XmlNodeList Sites { get; set; }
    #endregion
}
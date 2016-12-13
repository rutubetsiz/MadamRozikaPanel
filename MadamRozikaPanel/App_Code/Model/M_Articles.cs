using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;


public class M_Articles
{
    public M_Articles()
    {

    }
    public M_Articles(DataRow dr)
    {
        ArticleId = Convert.ToInt32(dr["ArticleId"]);
        AuthorId = Convert.ToInt32(dr["AuthorId"]);
        Title = dr["Title"].ToString();
        Status = Convert.ToByte(dr["Status"]);
        ArticleText = dr["ArticleText"].ToString();
        PublishDate = Convert.ToDateTime(dr["PublishDate"]);
        SortingDate = Convert.ToDateTime(dr["SortingDate"]);
        Hit = Convert.ToInt32(dr["Hit"]);
        CommentActive = Convert.ToByte(dr["CommentActive"]);
        Copyright = Convert.ToInt32(dr["Copyright"]);
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(dr["Sites"].ToString());
        Sites = xml.SelectNodes("/sites/site");
    }

    #region Properties
    public int ArticleId { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public byte Status { get; set; }
    public string ArticleText { get; set; }
    public DateTime PublishDate { get; set; }
    public DateTime SortingDate { get; set; }
    public int Hit { get; set; }
    public byte CommentActive { get; set; }
    public int Copyright { get; set; }
    public XmlNodeList Sites { get; set; }
    #endregion
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for Authors
/// </summary>
public class M_Authors
{
    public M_Authors()
    {

    }
    public M_Authors(DataRow dr)
    {
        AuthorId = Convert.ToInt32(dr["AuthorId"]);
        Name = dr["Name"].ToString();
        Mail = dr["Mail"].ToString();
        Status = Convert.ToInt32(dr["Status"]);
        MainPageStatus = Convert.ToInt32(dr["MainPageStatus"]);
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(dr["Sites"].ToString());
        Sites = xml.SelectNodes("/sites/site");
        NameUrl = dr["NameUrl"].ToString();
        imageUrl = dr["imageUrl"].ToString();
        TwitterUrl = dr["TwitterUrl"].ToString();
        FacebookUrl = dr["FacebookUrl"].ToString();
        LinkedinUrl = dr["LinkedinUrl"].ToString();
        Embed = dr["Embed"].ToString();
        Rank = Convert.ToInt32(dr["Rank"]);
        CategoryId = Convert.ToInt32(dr["CategoryId"]);
    }

    #region Properties
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }
    public int Status { get; set; }
    public int MainPageStatus { get; set; }
    public XmlNodeList Sites { get; set; }
    public string NameUrl { get; set; }
    public string imageUrl { get; set; }
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string LinkedinUrl { get; set; }
    public string Embed { get; set; }
    public int Rank { get; set; }
    public int CategoryId { get; set; }
    #endregion
}
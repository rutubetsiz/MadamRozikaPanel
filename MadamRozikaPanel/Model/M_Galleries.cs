using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for Galleries
/// </summary>
public class M_Galleries
{
    public M_Galleries()
    {

    }
    public M_Galleries(DataRow dr)
    {
        GalleryId = Convert.ToInt32(dr["GalleryId"]);
        CategoryId = Convert.ToInt32(dr["CategoryId"]);
        Title = dr["Title"].ToString();
        Type = Convert.ToByte(dr["Type"]);
        PublishDate = Convert.ToDateTime(dr["PublishDate"]);
        Status = Convert.ToByte(dr["Status"]);
        Hit = Convert.ToInt32(dr["Hit"]);
        ItemCount = Convert.ToInt32(dr["ItemCount"]);
    }

    #region Properties
    public int GalleryId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public int Type { get; set; }
    public DateTime PublishDate { get; set; }
    public byte Status { get; set; }
    public int Hit { get; set; }
    public int ItemCount { get; set; }
    #endregion
}
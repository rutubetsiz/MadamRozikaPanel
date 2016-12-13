using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GalleryOperations
/// </summary>
public class O_Gallery
{
    public List<M_Galleries> GetAllGalleries(int top)
    {
        List<M_Galleries> lst = new List<M_Galleries>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Gallery ORDER BY GalleryId DESC", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Galleries G = new M_Galleries(dr);
            lst.Add(G);
        }
        return lst;
    }

    public List<M_Galleries> GetAllGalleryListWithCondition(int top, string condition)
    {
        List<M_Galleries> lst = new List<M_Galleries>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Gallery WHERE " + condition + " ORDER BY GalleryId DESC", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Galleries G = new M_Galleries(dr);
            lst.Add(G);
        }
        return lst;
    }

    /// <summary>
    /// Panelde listelenen haberler için Edit sayfasına gitmeden başlık, spot, kategori, durum ve konum gibi bilgileri günceller
    /// </summary>
    /// <param name="title">String türünde data (haber başlığı)</param>
    /// <param name="summary">String türünde data (haber spotu)</param>
    /// <param name="categoryId">int türünde data (haber kategori id'si)</param>
    /// <param name="status">int türünde data (haber statüs'ü)</param>
    /// <param name="newsType">int türünde data (haber konumu) Veritabanında bir tabloda tutulmuyor, kod içerisinde manuel olarak eklenmiştir. Kutu Haber = 0, Manşet = 1, Sürmanşet = 2, Süper Manşet = 3, Üst Süper Manşet = 4, Seo = 5, Manşet Üstü Süper = 6</param>
    /// <param name="newsId">int türünde data (haber id'si)</param>
    public void UpdateGallery(string title, int categoryId, int status, int GalleryId)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery("UPDATE Gallery SET Title='" + title + "', CategoryId=" + categoryId + ", Status=" + status + " WHERE GalleryId=" + GalleryId, 0, CommandType.Text);
    }

    public M_Galleries GalleryDetail(int GalleryId)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        M_Galleries G = new M_Galleries(Exec.ExecuteQuery<DataRow>("SELECT * FROM Gallery WHERE GalleryId = " + GalleryId, 0, CommandType.Text));
        return G;
    }

    public DataTable FillDropDownList()
    {
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT Name, CategoryId FROM Category where ParentId = 0 AND Status = 1  ORDER BY  Name", 3600, CommandType.Text);
        return dt;
    }

    public string InsertGallery(int CategoryId, string Title, int Type, int Status, string GalleryUrl)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        string dsda = "INSERT INTO Gallery (CategoryId, Title, Type, PublishDate, Status, GalleryUrl) VALUES (" + CategoryId + ",'" + Title + "', " + Type + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Status + ", '" + GalleryUrl + "')";
        Exec.ExecuteQuery("INSERT INTO Gallery (CategoryId, Title, Type, PublishDate, Status, GalleryUrl) VALUES (" + CategoryId + ",'" + Title + "', " + Type + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Status + ", '" + GalleryUrl + "')", 0, CommandType.Text);
        DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT MAX(GalleryId) as MaxId FROM Gallery", 0, CommandType.Text);
        return dr["MaxId"].ToString();
    }
    public string UpdateGallery(int CategoryId, string Title, int Type, int Status, string GalleryUrl,string GalleryId)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery("UPDATE Gallery SET CategoryId=" + CategoryId + ", Title='" + Title + "', Type= " + Type + ", Status=" + Status + ", GalleryUrl='" + GalleryUrl + "'  WHERE GalleryId=" + GalleryId, 0, CommandType.Text);
        DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT MAX(GalleryId) as MaxId FROM Gallery", 0, CommandType.Text);
        return dr["MaxId"].ToString();
    }
    public static void InsertGalleryItems(string sql)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery(sql, 0, CommandType.Text);
    }
}
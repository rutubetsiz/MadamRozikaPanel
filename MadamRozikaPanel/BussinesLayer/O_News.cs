using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using MadamRozikaPanel.CrossCuttingLayer;
using MadamRozikaPanelData;

namespace MadamRozikaPanel.BussinesLayer
{
    /// <summary>
    /// Summary description for NewsOperation
    /// </summary>
    public class O_News
    {
        private readonly MadamRozikaEntities _db = new MadamRozikaEntities();

        public MadamRozikaPanelData.News NewsDetail(int newsId)
        {
            return _db.News.Include(x => x.TagNewsRelations).Include(xt => xt.TagNewsRelations.Select(t => t.Tag)).FirstOrDefault(x => x.NewsId == newsId);
        }


        public CheckBoxList ReturnDigerKategoriler(ref CheckBoxList cbl, int newsid)
        {
            var result = _db.News.Include(x => x.CategoryNewsRelations).Include(xt => xt.CategoryNewsRelations.Select(t => t.Category)).Where(n => n.NewsId == newsid);
            foreach (var item in result)
            {
                cbl.Items.FindByValue(item.CategoryId.ToSafeString()).Selected = true;
            }
            return cbl;
        }
        public List<Category> KategoriDoldur()
        {
            return _db.Categories.Where(x => x.ParentId == 0 && x.Status == 1 && x.Url != "anasayfa").OrderBy(x => x.Rank).ToList();
        }

        public List<Category> TumKategorileriDoldur()
        {
            return _db.Categories.Where(x => x.Status == 1 && x.Url != "anasayfa" && x.ParentId != 0).OrderBy(x => x.ParentId).ThenBy(x => x.Rank).ToList();
        }

        //public List<M_News> GetAllNewsList(int top)
        //{

        //    return _db.News.OrderByDescending(x => x.ModifiedDate).Take(top).ToList();
        //    List<M_News> lst = new List<M_News>();
        //    DataTable dt = new DataTable();
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " N.* FROM News AS N ORDER BY  N.NewsId DESC", 0, CommandType.Text);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        M_News N = new M_News(dr);
        //        lst.Add(N);
        //    }
        //    return lst;
        //}

        //public List<M_News> GetAllNewsListWithCondition(int top, string condition)
        //{
        //    List<M_News> lst = new List<M_News>();
        //    DataTable dt = new DataTable();
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " N.* FROM News AS N WHERE " + condition + " ORDER BY  N.NewsId DESC", 0, CommandType.Text);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        M_News N = new M_News(dr);
        //        lst.Add(N);
        //    }
        //    return lst;
        //}

        //public DataTable GetAllNewsForOrder(string newsType, string date)
        //{
        //    DataTable dt = new DataTable();
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    dt = Exec.ExecuteQuery<DataTable>("SELECT TOP 50 * FROM News WHERE  Status = 1 AND NewsType = " + newsType + " ORDER BY Weight ASC, PublishDate DESC", 0, CommandType.Text);
        //    return dt;
        //}

        //public DataTable KategoriDoldur()
        //{
        //    DataTable dt = new DataTable();
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    dt = Exec.ExecuteQuery<DataTable>("SELECT Name, CategoryId FROM Category WHERE ParentId = 0 AND Status = 1 AND Url != 'anasayfa' ORDER BY Weight", 3600, CommandType.Text);
        //    return dt;
        //}
        //public DataTable TumKategorileriDoldur()
        //{
        //    var dt =
        //        new Execute(DatabaseType.DBType1).ExecuteQuery<DataTable>(
        //            "SELECT Name, CategoryId FROM Category WHERE Status=1 AND Url != 'anasayfa' AND ParentId != 0 ORDER BY ParentId, Weight",
        //            3600, CommandType.Text);
        //    return dt;
        //}
        //public void UpdateNews(string title, string summary, int categoryId, int status, int newsType, int newsId)
        //{
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    Exec.ExecuteQuery("UPDATE News SET Title='" + title + "', Summary='" + summary + "', CategoryId=" + categoryId + ", Status=" + status + ", NewsType = " + newsType + " WHERE NewsId=" + newsId, 0, CommandType.Text);
        //}
        //public void UpdateNewsForReOrder(string title, string summary, int newsId)
        //{
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    Exec.ExecuteQuery("UPDATE News SET Title='" + title + "', Summary='" + summary + "' WHERE NewsId=" + newsId, 0, CommandType.Text);
        //}

        //public M_News NewsDetail(int NewsId)
        //{
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    M_News N = new M_News(Exec.ExecuteQuery<DataRow>("SELECT * FROM News WHERE NewsId = " + NewsId, 0, CommandType.Text));
        //    return N;
        //}

        //public int Insert(string Title, string TitleUrl, string Summary, string NewsText, int Status, int CommentActive, string NewsTags, int CategoryId, int NewsType)
        //{
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    Exec.ExecuteQuery("INSERT INTO News (Title, TitleUrl, Summary, NewsText, Status, ModifiedDate, PublishDate, CommentActive, NewsTags, CategoryId, NewsType) VALUES ('" + Title + "', '" + TitleUrl + "', '" + Summary + "', '" + NewsText + "', " + Status + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + CommentActive + ", '" + NewsTags + "', " + CategoryId + ", " + NewsType + ")", 0, CommandType.Text);
        //    DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT MAX(NewsId) as MaxId FROM News", 0, CommandType.Text);
        //    return Convert.ToInt32(dr["MaxId"].ToString());
        //}
        //public void Update(string Title, int NewsId, string Summary, string NewsText, int Status, int CommentActive, string NewsTags, int CategoryId)
        //{
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    Exec.ExecuteQuery("UPDATE News SET Title='" + Title + "', Summary='" + Summary + "', NewsText='" + NewsText + "', Status=" + Status + ", CommentActive=" + CommentActive + ", NewsTags='" + NewsTags + "', CategoryId=" + CategoryId + " WHERE NewsId=" + NewsId, 0, CommandType.Text);
        //}
        //public void Update(string imageurl, int newsId)
        //{
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    Exec.ExecuteQuery("UPDATE News SET ImageUrl='" + imageurl + "' WHERE NewsId=" + newsId, 0, CommandType.Text);
        //}

        //public void Update(int videoid, int newsId)
        //{
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    Exec.ExecuteQuery("UPDATE News SET VideoId=" + videoid + " WHERE NewsId=" + newsId, 0, CommandType.Text);
        //}
    }
}
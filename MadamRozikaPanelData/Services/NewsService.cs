
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MadamRozikaPanelData.Services
{
    class NewsService
    {
        private readonly MadamRozikaEntities _context = new MadamRozikaEntities();

        public List<News> GetAllNewsList(int top)
        {
            return _context.News.OrderByDescending(x => x.NewsId).Take(top).ToList();
        }

        //public List<News> GetAllNewsListWithCondition(int top, string condition)
        //{
        //    List<News> lst = new List<News>();
        //    DataTable dt = new DataTable();
        //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " N.* FROM News AS N WHERE " + condition + " ORDER BY  N.NewsId DESC", 0, CommandType.Text);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        News N = new News(dr);
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

        public List<Category> KategoriDoldur()
        {
            var lst =
                _context.Categories.Where(x => x.ParentId == 0 && x.Status == 1 && x.Url != "anasayfa")
                    .OrderBy(x => x.Rank)
                    .ToList();

            return lst;
        }
        public List<Category> TumKategorileriDoldur()
        {
            var lst =
                _context.Categories.Where(x => x.Status == 1 && x.Url != "anasayfa" && x.ParentId != 0)
                    .OrderBy(x => x.ParentId)
                    .ThenBy(x => x.Rank).ToList();
            return lst;
        }
        public void UpdateNews(string title, string summary, int categoryId, int status, int newsType, int newsId)
        {
            try
            {
                _context.Entry(new News()).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (System.Exception ex)
            {
                throw new Exception("Haber Kaydetme Sırasında hata " + ex.Message);
            }

            //    Execute Exec = new Execute(DatabaseType.DBType1);
        //    Exec.ExecuteQuery("UPDATE News SET Title='" + title + "', Summary='" + summary + "', CategoryId=" + categoryId + ", Status=" + status + ", NewsType = " + newsType + " WHERE NewsId=" + newsId, 0, CommandType.Text);
        }
        public void UpdateNewsForReOrder(string title, string summary, int newsId)
        {
            try
            {
                _context.Entry(new News()).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (System.Exception ex)
            {
                throw new Exception("Haber Kaydetme Sırasında hata " + ex.Message);
            }

        }

        public News NewsDetail(int newsId)
        {
            var news = _context.News.FirstOrDefault(x => x.NewsId == newsId);
            if (news == null)
            {
                throw new Exception(newsId + " li haber bulunamadı");
            }

            return news;
        }

        public int Insert(string Title, string TitleUrl, string Summary, string NewsText, int Status, int CommentActive, string NewsTags, int CategoryId, int NewsType)
        {


            try
            {
                _context.Entry(new News()).State = EntityState.Added;
                _context.SaveChanges();

            }
            catch (System.Exception ex)
            {
                throw new Exception("Haber Kaydetme Sırasında hata " + ex.Message);
            }

            
            //Execute Exec = new Execute(DatabaseType.DBType1);
            //Exec.ExecuteQuery("INSERT INTO News (Title, TitleUrl, Summary, NewsText, Status, ModifiedDate, PublishDate, CommentActive, NewsTags, CategoryId, NewsType) VALUES ('" + Title + "', '" + TitleUrl + "', '" + Summary + "', '" + NewsText + "', " + Status + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + CommentActive + ", '" + NewsTags + "', " + CategoryId + ", " + NewsType + ")", 0, CommandType.Text);
            //DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT MAX(NewsId) as MaxId FROM News", 0, CommandType.Text);
            //return Convert.ToInt32(dr["MaxId"].ToString());
        }


        public void Update(string Title, int NewsId, string Summary, string NewsText, int Status, int CommentActive, string NewsTags, int CategoryId)
        {

            try
            {
                _context.Entry(new News()).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (System.Exception ex)
            {
                throw new Exception("Haber Kaydetme Sırasında hata " + ex.Message);
            }



            //Execute Exec = new Execute(DatabaseType.DBType1);
            //Exec.ExecuteQuery("UPDATE News SET Title='" + Title + "', Summary='" + Summary + "', NewsText='" + NewsText + "', Status=" + Status + ", CommentActive=" + CommentActive + ", NewsTags='" + NewsTags + "', CategoryId=" + CategoryId + " WHERE NewsId=" + NewsId, 0, CommandType.Text);
        }
        public void Update(string imageurl, int newsId)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("UPDATE News SET ImageUrl='" + imageurl + "' WHERE NewsId=" + newsId, 0, CommandType.Text);
        }

        public void Update(int videoid, int newsId)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("UPDATE News SET VideoId=" + videoid + " WHERE NewsId=" + newsId, 0, CommandType.Text);
        }
    }
}

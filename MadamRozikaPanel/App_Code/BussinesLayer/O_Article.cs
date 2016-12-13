using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ArticleOperations
/// </summary>
public class O_Article
{
    public List<M_Articles> GetAllArticles(int top)
    {
        List<M_Articles> lst = new List<M_Articles>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Article ORDER BY ArticleId DESC", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Articles A = new M_Articles(dr);
            lst.Add(A);
        }
        return lst;
    }

    public List<M_Articles> GetAllArticlesWithCondition(int top, string condition)
    {
        List<M_Articles> lst = new List<M_Articles>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Article WHERE " + condition + " ORDER BY ArticleId DESC", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Articles A = new M_Articles(dr);
            lst.Add(A);
        }
        return lst;
    }

    public List<M_Authors> GetAllAuthors()
    {
        List<M_Authors> lst = new List<M_Authors>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM Author WHERE Status=1 ORDER BY Name", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Authors A = new M_Authors(dr);
            lst.Add(A);
        }
        return lst;
    }
    public void UpdateArticle(string title, int status, int ArticleId)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery("UPDATE Article SET Title='" + title + "', Status=" + status + " WHERE ArticleId=" + ArticleId, 0, CommandType.Text);
    }
    public M_Articles GetArticleDetail(int ArticleId)
    {
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM Article WHERE ArticleId = " + ArticleId, 0, CommandType.Text);
        M_Articles A = new M_Articles(dt.Rows[0]);
        return A;
    }
    public void InsertArticle(string title, string authorid, int status, int commentactive, string articletext)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery("INSERT INTO Article (Title, AuthorId, Status, CommentActive, ArticleText, SortingDate, PublishDate) VALUES ('" + title + "'," + authorid + ", " + status + "," + commentactive + ", '" + articletext + "', '" + DateTime.Today.ToString("yyyy-MM-dd 00:00:00") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", 0, CommandType.Text);
    }

    public void UpdateArticle(string title, string authorid, int status, int commentactive, string articletext, string ArticleId)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery("UPDATE Article SET Title='" + title + "', AuthorId=" + authorid + ", Status=" + status + ", CommentActive="+commentactive+", ArticleText='"+articletext+"' WHERE ArticleId=" + ArticleId, 0, CommandType.Text);
    }
}
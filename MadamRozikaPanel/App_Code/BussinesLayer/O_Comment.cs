using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class O_Comment
{
    public List<M_Comments> GetAllComments(int top)
    {
        List<M_Comments> lst = new List<M_Comments>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Comment ORDER BY CommentId DESC", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Comments C = new M_Comments(dr);
            lst.Add(C);
        }
        return lst;
    }

    public List<M_Comments> GetAllCommensWithCondition(int top, string condition)
    {
        List<M_Comments> lst = new List<M_Comments>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Comment WHERE " + condition + " ORDER BY CommentId DESC", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Comments C = new M_Comments(dr);
            lst.Add(C);
        }
        return lst;
    }

    public void UpdateComment(string CommentId, string isactive)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery("UPDATE Comment SET IsActive=" + isactive + " WHERE CommentId=" + CommentId, 0, CommandType.Text);
    }

    public string GetUrl(string contentid, string type)
    {
        string url = "";
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        if (type == "Haber")
        {
            dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM News WHERE NewsId=" + contentid, 0, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                url = "<a href=\"http://www.bugun.com.tr/" + Helper.GetUrl(dt.Rows[0]["Title"].ToString()) + "-12313.html\" target=\"_blank\">" + dt.Rows[0]["Title"].ToString() + "</a>";
            }
        }
        else if (type == "Galeri")
        {
            dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM Gallery WHERE GalleryId=" + contentid, 0, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                url = "<a href=\"http://www.bugun.com.tr/" + Helper.GetUrl(dt.Rows[0]["Title"].ToString()) + "-fotogaleri-12313\" target=\"_blank\">" + dt.Rows[0]["Title"].ToString() + "</a>";
            }
        }
        else if (type == "Yazar")
        {
            //dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM Gallery WHERE GalleryId=" + contentid, 0, CommandType.Text);
            //if (dt.Rows.Count > 0)
            //{
            //    url = "<a href=\"http://www.bugun.com.tr/" + Helper.GetUrl(dt.Rows[0]["Title"].ToString()) + "\" target=\"_blank\">" + dt.Rows[0]["Title"].ToString() + "</a>";
            //}
        }
        else if (type == "Yazı")
        {
            //dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM Gallery WHERE GalleryId=" + contentid, 0, CommandType.Text);
            //if (dt.Rows.Count > 0)
            //{
            //    url = "<a href=\"http://www.bugun.com.tr/" + Helper.GetUrl(dt.Rows[0]["Title"].ToString()) + "\" target=\"_blank\">" + dt.Rows[0]["Title"].ToString() + "</a>";
            //}
        }
        else if (type == "Etiket")
        {
            //dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM Gallery WHERE GalleryId=" + contentid, 0, CommandType.Text);
            //if (dt.Rows.Count > 0)
            //{
            //    url = "<a href=\"http://www.bugun.com.tr/" + Helper.GetUrl(dt.Rows[0]["Title"].ToString()) + "\" target=\"_blank\">" + dt.Rows[0]["Title"].ToString() + "</a>";
            //}
        }
        else if (type == "Canlı Yayın")
        {
            url = "<a href=\"http://www.bugun.com.tr/BugunTv\" target=\"_blank\">Bugün Tv Canlı İzle</a>";
        }


        return url;
    }
}
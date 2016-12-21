using System;
using System.Data;

namespace MadamRozikaPanel.App_Code.BussinesLayer
{
    /// <summary>
    /// Summary description for O_Video
    /// </summary>
    public class O_Video
    {
        public int Insert(string Title, string Summary, string TitleUrl, string VideoUrl, string NewsTags, int CategoryId)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("INSERT INTO Video (Title, Summary, TitleUrl, VideoUrl, Tags, CategoryId) VALUES ('" + Title + "', '" + Summary + "', '" + TitleUrl + "', '" + VideoUrl + "', '" + NewsTags + "', " + CategoryId + ")", 0, CommandType.Text);
            DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT MAX(VideoId) as MaxId FROM Video", 0, CommandType.Text);
            return Convert.ToInt32(dr["MaxId"].ToString());
        }

        public void Insert(int ContentId, int VideoId, string ContentType)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("INSERT INTO VideoRelation (ContentId, VideoId, ContentType) VALUES (" + ContentId + ", " + VideoId + ", '" + ContentType + "')", 0, CommandType.Text);
            DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT MAX(VideoId) as MaxId FROM Video", 0, CommandType.Text);
        }
    }
}
using System.Collections.Generic;
using System.Data;

namespace MadamRozikaPanel.App_Code.BussinesLayer
{
    /// <summary>
    /// Summary description for HeadlineOperations
    /// </summary>
    public class O_Headline
    {
        public List<M_Headlines> GetData(int top)
        {
            List<M_Headlines> lst = new List<M_Headlines>();
            DataTable dt = new DataTable();
            Execute Exec = new Execute(DatabaseType.DBType1);
            dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Headlines AS N ORDER BY Rank", 0, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                M_Headlines H = new M_Headlines(dr);
                lst.Add(H);
            }
            return lst;
        }

        public void UpdateHeadline(string url, string title, string description, string imageurl, int status, string HeadlineId)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("UPDATE Headlines SET Url='" + url + "', Title='" + title + "', Description='" + description + "', ImageUrl='" + imageurl + "', Status=" + status + " WHERE HeadlineId=" + HeadlineId, 0, CommandType.Text);
        }
        public void UpdateHeadline(string url, string title, string description, int status, string HeadlineId)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("UPDATE Headlines SET Url='" + url + "', Title='" + title + "', Description='" + description + "', Status=" + status + " WHERE HeadlineId=" + HeadlineId, 0, CommandType.Text);
        }

        public string InsertHeadline(string title, string description, string url, int status, string imageurl, string objecttype)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            DataRow dr = Exec.ExecuteQuery<DataRow>("INSERT INTO Headlines (Title, Description, Url, Status, Rank, ObjectType) VALUES ('" + title + "', '" + description + "', '" + url + "', " + status + ", (SELECT (MAX(Rank)+1) FROM Headlines), '"+objecttype+"'); DECLARE @HID INT; SELECT @HID = @@IDENTITY; UPDATE Headlines SET ImageUrl = '" + imageurl + "'+CONVERT(varchar(20), @HID)+'_640_360.jpg' WHERE HeadlineId = @HID; SELECT @HID AS HID ", 0, CommandType.Text);
            return dr["HID"].ToString();
        }
    }
}
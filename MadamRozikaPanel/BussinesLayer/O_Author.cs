using System.Collections.Generic;
using System.Data;

namespace MadamRozikaPanel.BussinesLayer
{
    public class O_Author
    {
        public List<M_Authors> GetAllAuthors(int top)
        {
            List<M_Authors> lst = new List<M_Authors>();
            DataTable dt = new DataTable();
            Execute Exec = new Execute(DatabaseType.DBType1);
            dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Author ORDER BY Name", 0, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                M_Authors A = new M_Authors(dr);
                lst.Add(A);
            }
            return lst;
        }

        public List<M_Authors> GetAllAuthorsWithCondition(int top, string condition)
        {
            List<M_Authors> lst = new List<M_Authors>();
            DataTable dt = new DataTable();
            Execute Exec = new Execute(DatabaseType.DBType1);
            dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Author WHERE " + condition + " ORDER BY Name", 0, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                M_Authors A = new M_Authors(dr);
                lst.Add(A);
            }
            return lst;
        }
        public M_Authors GetAllAuthorDetail(int authorid)
        {
            DataTable dt = new DataTable();
            Execute Exec = new Execute(DatabaseType.DBType1);
            dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM Author WHERE AuthorId = " + authorid, 0, CommandType.Text);
            M_Authors A = new M_Authors(dt.Rows[0]);
            return A;
        }
        public void UpdateAuthor(string name, string email, int status, int MainPageStatus, string NameUrl, string imageurl, string TwitterUrl, string FacebookUrl, string LinkedinUrl, string Embed, int CategoryId, int Rank, int OldRank, int AuthorId)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            if (Rank > OldRank)
            {
                Exec.ExecuteQuery("UPDATE Author SET Rank = Rank - 1 WHERE Rank <= " + Rank + " AND Rank > " + OldRank, 0, CommandType.Text);
            }
            else
            {
                Exec.ExecuteQuery("UPDATE Author SET Rank = Rank + 1 WHERE Rank >= " + Rank + " AND Rank < " + OldRank, 0, CommandType.Text);
            }

            Exec.ExecuteQuery("UPDATE Author SET Name='" + name + "', Mail='" + email + "', Status=" + status + ", MainPageStatus=" + MainPageStatus + ", NameUrl='" + NameUrl + "', imageUrl='" + imageurl + "', TwitterUrl='" + TwitterUrl + "', FacebookUrl='" + FacebookUrl + "', LinkedinUrl='" + LinkedinUrl + "', Embed='" + Embed + "', CategoryId=" + CategoryId + ", Rank=" + Rank + " WHERE AuthorId=" + AuthorId, 0, CommandType.Text);
        }
        public void UpdateAuthor(string name, string email, int status, int MainPageStatus, string NameUrl, string TwitterUrl, string FacebookUrl, string LinkedinUrl, string Embed, int CategoryId, int Rank, int OldRank, int AuthorId)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            if (Rank > OldRank)
            {
                Exec.ExecuteQuery("UPDATE Author SET Rank = Rank - 1 WHERE Rank <= " + Rank + " AND Rank > " + OldRank, 0, CommandType.Text);
            }
            else
            {
                Exec.ExecuteQuery("UPDATE Author SET Rank = Rank + 1 WHERE Rank >= " + Rank + " AND Rank < " + OldRank, 0, CommandType.Text);
            }

            Exec.ExecuteQuery("UPDATE Author SET Name='" + name + "', Mail='" + email + "', Status=" + status + ", MainPageStatus=" + MainPageStatus + ", NameUrl='" + NameUrl + "', TwitterUrl='" + TwitterUrl + "', FacebookUrl='" + FacebookUrl + "', LinkedinUrl='" + LinkedinUrl + "', Embed='" + Embed + "', CategoryId=" + CategoryId + ", Rank=" + Rank + " WHERE AuthorId=" + AuthorId, 0, CommandType.Text);
        }
        public void UpdateAuthor(string ad, string mail, int status, int mainpagestatus, int AuthorId, string Url)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("UPDATE Author SET Name='" + ad + "', Mail='" + mail + "', Status=" + status + ", MainPageStatus=" + mainpagestatus + ", Url='" + Url + "' WHERE AuthorId=" + AuthorId, 0, CommandType.Text);
        }
        public string InsertAuthor(string name, string email, int status, int MainPageStatus, string NameUrl, string imageurl, string TwitterUrl, string FacebookUrl, string LinkedinUrl, string Embed, int CategoryId, int Rank)
        {
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery("UPDATE Author SET Rank = Rank + 1 WHERE Rank >= " + Rank, 0, CommandType.Text);

            Exec.ExecuteQuery("INSERT INTO Author (Name, Mail, Status, MainPageStatus, Url, imageUrl, TwitterUrl, FAcebookUrl, LinkedinUrl, Embed, CategoryId) VALUES ('" + name + "','" + email + "', " + status + ",'" + MainPageStatus + "', '" + NameUrl + "', '" + imageurl + "', '" + TwitterUrl + "', '" + FacebookUrl + "', '" + LinkedinUrl + "', '" + Embed + "', " + CategoryId + ")", 0, CommandType.Text);
            DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT MAX(GalleryId) as MaxId FROM Gallery", 0, CommandType.Text);
            return dr["MaxId"].ToString();
        }

        public DataTable GetRowNumber()
        {
            DataTable dt = new DataTable();
            Execute Exec = new Execute(DatabaseType.DBType1);
            dt = Exec.ExecuteQuery<DataTable>("select ROW_NUMBER() OVER(ORDER BY Rank DESC) AS RowNumber from Author where Status = 1", 0, CommandType.Text);
            return dt;
        }
    }
}
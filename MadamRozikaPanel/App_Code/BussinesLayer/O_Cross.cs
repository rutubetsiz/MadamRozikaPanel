using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

/// <summary>
/// Summary description for O_Cross
/// </summary>
public class O_Cross
{
    O_News NewsOprt = new O_News();
    public string ReturnSites(CheckBoxList cbl)
    {
        string Sites = "<sites><site>";
        for (int i = 0; i < cbl.Items.Count; i++)
        {
            Sites += "<" + cbl.Items[i].Value + ">" + Convert.ToInt32(cbl.Items[i].Selected) + "</" + cbl.Items[i].Value + ">";
        }
        Sites += "</site></sites>";
        return Sites;
    }

    //public CheckBoxList FillSites(ref CheckBoxList cbl)
    //{
    //    List<ListItem> lst = new List<ListItem>();
    //    lst.Add(new ListItem("Bugün", "bugun"));
    //    lst.Add(new ListItem("Millet", "millet"));
    //    cbl.DataSource = lst;
    //    cbl.DataTextField = "text";
    //    cbl.DataValueField = "value";
    //    cbl.DataBind();
    //    return cbl;
    //}
    public CheckBoxList SelectSites(ref CheckBoxList cbl, XmlNode xn)
    {
        if (xn.InnerXml.Contains("bugun"))
        {
            if (xn["bugun"].InnerText == "1")
            {
                cbl.Items.FindByValue("bugun").Selected = true;
            }
        }
        if (xn.InnerXml.Contains("millet"))
        {
            if (xn["millet"].InnerText == "1")
            {
                cbl.Items.FindByValue("millet").Selected = true;
            }
        }
        return cbl;
    }

    public CheckBoxList ReturnDigerKategoriler(ref CheckBoxList cbl, int ContentId)
    {
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM CategoryRelation WHERE IsDefault=0 AND ContentId=" + ContentId, 0, CommandType.Text);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cbl.Items.FindByValue(dt.Rows[i]["CategoryId"].ToString()).Selected = true;
        }
        return cbl;
    }

    public void InsertDigerKategoriler(ref CheckBoxList cbl, int ContentId)
    {
        string sql = "";

        foreach (ListItem item in cbl.Items)
        {
            if (item.Selected)
            {
                sql += "INSERT INTO CategoryRelation (ContentId, CategoryId, IsDefault) VALUES (" + ContentId + ", " + item.Value + ", 0); ";
            }
        }
        Execute Exec = new Execute(DatabaseType.DBType1);
        Exec.ExecuteQuery(sql, 0, CommandType.Text);
    }
    public DataTable KategoriDoldur()
    {
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT Name, CategoryId FROM Category where ParentId = 0 AND Status = 1  ORDER BY  Name", 3600, CommandType.Text);
        return dt;
    }

    public string KategoriAdi(int CategoryId)
    {
        Execute Exec = new Execute(DatabaseType.DBType1);
        DataRow dr = Exec.ExecuteQuery<DataRow>("SELECT Name FROM Category WHERE CategoryId=" + CategoryId, 3600, CommandType.Text);
        return dr["Name"].ToString();
    }

}
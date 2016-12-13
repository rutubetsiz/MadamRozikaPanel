using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for O_Agency
/// </summary>
public class O_Agency
{
    public List<M_Agencies> GetData(int top)
    {
        List<M_Agencies> lst = new List<M_Agencies>();
        DataTable dt = new DataTable();
        Execute Exec = new Execute(DatabaseType.DBType1);
        dt = Exec.ExecuteQuery<DataTable>("SELECT TOP " + top + " * FROM Agencies ORDER BY AgencyName", 0, CommandType.Text);
        foreach (DataRow dr in dt.Rows)
        {
            M_Agencies A = new M_Agencies(dr);
            lst.Add(A);
        }
        return lst;
    }
}
using System.Data;

namespace MadamRozikaPanel.BussinesLayer
{
    public class O_User
    {
        public M_Users GetUserDetail(int authorid)
        {
            DataTable dt = new DataTable();
            Execute Exec = new Execute(DatabaseType.DBType1);
            dt = Exec.ExecuteQuery<DataTable>("SELECT * FROM User Where Email = 'dsadas' AND Password = 'dsada'", 0, CommandType.Text);
            M_Users U = new M_Users(dt.Rows[0]);
            return U;
        }
    }
}
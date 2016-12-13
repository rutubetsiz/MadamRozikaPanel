using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.News
{
    public partial class ReOrder : System.Web.UI.Page
    {
        public string NewsType;
        private O_News NewsOprt = new O_News();

        protected void Page_Load(object sender, EventArgs e)
        {
            NewsType = Helper.GetQueryStringValue<string>("NewsType");
            if (string.IsNullOrEmpty(NewsType))
            {
                NewsType = "1";
            }
            if (!IsPostBack)
            {
                GetData(NewsType);
                ddListTurler.SelectedValue = NewsType;
            }
        }

        public void GetData(string newsType)
        {
            //DataTable dt = NewsOprt.GetAllNewsForOrder(NewsType, DateTime.Parse(DateTime.Today.ToString("yyyy-dd-MM")));
            DataTable dt = NewsOprt.GetAllNewsForOrder(NewsType, "2015-09-18");
            rptAllNews.DataSource = dt;
            rptAllNews.DataBind();
        }

        protected void rptAllNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnGuncelle") // check command is cmd_delete
            {
                int NewsId = Convert.ToInt32(e.CommandArgument);
                TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
                TextBox txtSummary = (TextBox)e.Item.FindControl("txtSummary");
                NewsOprt.UpdateNewsForReOrder(txtTitle.Text.Replace("'", "''"), txtSummary.Text.Replace("'", "''"),
                    NewsId);
            }
        }

        [WebMethod]
        public static string SiralamaUpdate(string Data)
        {
            string[] veri = Data.Split(',');

            string updateSql = "";

            for (int i = 0; i < veri.Length; i++)
            {
                updateSql += " UPDATE News SET Weight=" + veri[i].Split('#')[0] + " WHERE NewsId=" +
                             veri[i].Split('#')[1] + " ;";
            }
            updateSql = updateSql.TrimEnd(';');
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery(updateSql);
            return "OK";
        }

        protected void ddListTurler_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("?NewsType=" + ddListTurler.SelectedValue);
            Response.Redirect("/News/ReOrder.aspx" + sb);
        }
    }
}
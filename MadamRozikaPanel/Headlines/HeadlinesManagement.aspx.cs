using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.Headlines
{
    public partial class HeadlinesManagement : System.Web.UI.Page
    {
        O_Headline HeadlinesOprt = new O_Headline();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }

        public void GetData()
        {
            List<M_Headlines> lst = HeadlinesOprt.GetData(18);
            rptAllNews.DataSource = lst;
            rptAllNews.DataBind();
        }

        protected void rptAllNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnGuncelle")
            {
                int HeadlineId = Convert.ToInt32(e.CommandArgument);
                TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
                TextBox txtSummary = (TextBox)e.Item.FindControl("txtSummary");
                TextBox txtUrl = (TextBox)e.Item.FindControl("txtUrl");
                FileUpload fuGorsel = (FileUpload)e.Item.FindControl("fuGorsel");
                CheckBox cbStatus = (CheckBox)e.Item.FindControl("cbStatus");

                if (fuGorsel.HasFile)
                {
                    string folder = ConfigurationManager.AppSettings["HeadlineImagePath"] + @"\" + Helper.GetDirectory();
                    string image = "";
                    Bitmap imageBitMap = new Bitmap(fuGorsel.PostedFile.InputStream);
                    UploadImage uploadHeadline = new UploadImage();
                    //uploadHeadline.SaveImageSingle(imageBitMap, Server.MapPath(folder), HeadlineId + "_" + Path.GetExtension(fuGorsel.FileName), 640, 360);
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    image = folder + HeadlineId + "_640_360" + Path.GetExtension(fuGorsel.FileName);
                    uploadHeadline.SaveImageSingle(imageBitMap, folder + "/", HeadlineId + "_640_360" + Path.GetExtension(fuGorsel.FileName), 640, 360);
                    HeadlinesOprt.UpdateHeadline(txtUrl.Text, txtTitle.Text, txtSummary.Text, image, Convert.ToInt32(cbStatus.Checked), HeadlineId.ToString());
                }
                else
                {
                    HeadlinesOprt.UpdateHeadline(txtUrl.Text, txtTitle.Text, txtSummary.Text, Convert.ToInt32(cbStatus.Checked), HeadlineId.ToString());
                }
            }
            GetData();
        }

        [WebMethod]
        public static string SiralamaUpdate(string Data)
        {
            string[] veri = Data.Split(',');

            string updateSql = "";

            for (int i = 0; i < veri.Length; i++)
            {
                updateSql += " UPDATE Headline SET Rank=" + veri[i].Split('#')[0] + " WHERE HeadlineId=" + veri[i].Split('#')[1] + " ;";
            }
            updateSql = updateSql.TrimEnd(';');
            Execute Exec = new Execute(DatabaseType.DBType1);
            Exec.ExecuteQuery(updateSql);
            return "OK";
        }
    }
}
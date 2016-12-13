using MadamRozikaPanel.CrossCuttingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.News
{
    public partial class News : BasePage
    {
        public int CategoryId;
        public string AgencyName;
        public string NewsType;
        public string Status;
        public string Search;

        private O_News NewsOprt = new O_News();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadFilterQuery();
            if (!IsPostBack)
            {
                GetData();
                FillFilter();
            }
        }

        public void GetData()
        {
            List<M_News> list = new List<M_News>();

            string QueryString = Request.QueryString.ToString();
            //CategoryId=10200&Agency=Cihan&NewsType=3&Status=0&Search=dsadsa
            if (!String.IsNullOrEmpty(QueryString))
            {
                string[] queries;
                StringBuilder sbCondition = new StringBuilder();
                if (!string.IsNullOrEmpty(Request.QueryString["Search"]))
                {
                    queries = QueryString.Split('&');
                }
                else
                {
                    queries = QueryString.Replace("&Search=", "").Split('&');
                }

                for (int i = 0; i < queries.Length; i++)
                {
                    if (queries[i].ToString().Split('=')[0].ToString() == "Search")
                    {
                        sbCondition.Append("freetext(*,'" + Request.QueryString["Search"].ToString() + "') AND ");
                    }
                    else
                    {
                        if (queries[i].ToString().Split('=')[1].ToString() != "-1")
                        {
                            sbCondition.Append(queries[i].ToString().Split('=')[0] + " = '" +
                                               queries[i].ToString().Split('=')[1] + "' AND ");
                        }
                    }
                }
                sbCondition.Append("1=1");
                list = NewsOprt.GetAllNewsListWithCondition(500, sbCondition.ToString());
            }
            else
            {
                list = NewsOprt.GetAllNewsList(500);
            }
            rptAllNews.DataSource = list;
            rptAllNews.DataBind();
        }

        public void FillFilter()
        {
            ddListKategori.DataSource = NewsOprt.KategoriDoldur();
            ddListKategori.DataTextField = "Name";
            ddListKategori.DataValueField = "CategoryId";
            ddListKategori.DataBind();
            ddListKategori.Items.Insert(0, new ListItem("Tüm Kategoriler", "-1"));
            ddListKategori.SelectedValue = CategoryId.ToSafeString();
            ddListTurler.SelectedValue = NewsType;
            ddListDurum.SelectedValue = Status;
            txtAra.Text = Search;
        }

        protected void rptAllNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                M_News N = (M_News)e.Item.DataItem;
                //DropDownList ddListCategory = e.Item.FindControl("ddListCategory") as DropDownList;
                Literal ltrlKategori = e.Item.FindControl("ltrlKategori") as Literal;
                Literal ltrlDurum = e.Item.FindControl("ltrlDurum") as Literal;
                DataTable dtKategori = NewsOprt.KategoriDoldur();
                if (dtKategori.Rows.Count > 0)
                {
                    ltrlKategori.Text =
                        dtKategori.Select("CategoryId = " + N.CategoryId.ToString()).CopyToDataTable().Rows[0]["Name"]
                            .ToString();
                }

                if (N.Status == 1)
                {
                    ltrlDurum.Text = "Aktif";
                }
                else
                {
                    ltrlDurum.Text = "Pasif";
                }
                //ddListCategory.DataSource = dtKategori;
                //ddListCategory.DataTextField = "Name";
                //ddListCategory.DataValueField = "CategoryId";
                //ddListCategory.DataBind();
                //ddListCategory.SelectedValue = N.CategoryId.ToString();
            }
        }

        protected void rptAllNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "btnGuncelle") // check command is cmd_delete
            //{
            //    int NewsId = Convert.ToInt32(e.CommandArgument);
            //    TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
            //    TextBox txtSummary = (TextBox)e.Item.FindControl("txtSummary");
            //    DropDownList ddListCategory = (DropDownList)e.Item.FindControl("ddListCategory");
            //    CheckBox cbStatus = (CheckBox)e.Item.FindControl("cbStatus");
            //    DropDownList ddListKonum = (DropDownList)e.Item.FindControl("ddListKonum");
            //    NewsOprt.UpdateNews(txtTitle.Text.Replace("'", "''"), txtSummary.Text.Replace("'", "''"), Convert.ToInt32(ddListCategory.SelectedValue), Convert.ToInt32(cbStatus.Checked), Convert.ToInt32(ddListKonum.SelectedValue), NewsId);
            //}
            GetData();
        }

        protected void btnFiltrele_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("?CategoryId=" + ddListKategori.SelectedValue);
            sb.Append("&NewsType=" + ddListTurler.SelectedValue);
            sb.Append("&Status=" + ddListDurum.SelectedValue);
            sb.Append("&Search=" + txtAra.Text.Replace("'", "''"));
            Response.Redirect("/News/News.aspx" + sb);
        }

        public void LoadFilterQuery()
        {
            CategoryId = Helper.GetQueryStringValue<int>("CategoryId");
            NewsType = Helper.GetQueryStringValue<string>("NewsType");
            Search = Helper.GetQueryStringValue<string>("Search");
            Status = Helper.GetQueryStringValue<string>("Status");
        }
    }
}
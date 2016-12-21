using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MadamRozikaPanel.CrossCuttingLayer;

namespace MadamRozikaPanel.Gallery
{
    public partial class Galleries : System.Web.UI.Page
    {
        public int CategoryId;
        public string IsAgency;
        public string GalleryType;
        public string Status;
        public string Search;

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
            //List<M_Galleries> list = new List<M_Galleries>();
            string QueryString = Request.QueryString.ToString();
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
                            sbCondition.Append(queries[i].ToString().Split('=')[0] + " = '" + queries[i].ToString().Split('=')[1] + "' AND ");
                        }
                    }
                }
                sbCondition.Append("1=1");
                //list = GalleryOprt.GetAllGalleryListWithCondition(500, sbCondition.ToString());
            }
            else
            {
                //list = GalleryOprt.GetAllGalleries(500);
            }
            //rptAllNews.DataSource = list;
            rptAllNews.DataBind();
        }
        public void FillFilter()
        {
            //ddListKategori.DataSource = GalleryOprt.FillDropDownList();
            ddListKategori.DataTextField = "Name";
            ddListKategori.DataValueField = "CategoryId";
            ddListKategori.DataBind();
            ddListKategori.Items.Insert(0, new ListItem("Tüm Kategoriler", "-1"));
            ddListKategori.SelectedValue = CategoryId.ToSafeString();
            ddListKaynak.SelectedValue = IsAgency;
            ddListTurler.SelectedValue = GalleryType;
            ddListDurum.SelectedValue = Status;
            txtAra.Text = Search;
        }
        protected void rptAllNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //M_Galleries G = (M_Galleries)e.Item.DataItem;
                DropDownList ddListCategory = e.Item.FindControl("ddListCategory") as DropDownList;
                //ddListCategory.DataSource = GalleryOprt.FillDropDownList();
                ddListCategory.DataTextField = "Name";
                ddListCategory.DataValueField = "CategoryId";
                ddListCategory.DataBind();
                //ddListCategory.SelectedValue = G.CategoryId.ToString();
            }
        }
        protected void rptAllNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnGuncelle") // check command is cmd_delete
            {
                int NewsId = Convert.ToInt32(e.CommandArgument);
                TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
                TextBox txtShortTitle = (TextBox)e.Item.FindControl("txtShortTitle");
                DropDownList ddListCategory = (DropDownList)e.Item.FindControl("ddListCategory");
                CheckBox cbStatus = (CheckBox)e.Item.FindControl("cbStatus");
                //GalleryOprt.UpdateGallery(txtTitle.Text.Replace("'", "''"), Convert.ToInt32(ddListCategory.SelectedValue), Convert.ToInt32(cbStatus.Checked), NewsId);
            }
            GetData();
        }
        protected void btnFiltrele_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?CategoryId=" + ddListKategori.SelectedValue);
            sb.Append("&IsAgency=" + ddListKaynak.SelectedValue);
            sb.Append("&Type=" + ddListTurler.SelectedValue);
            sb.Append("&Status=" + ddListDurum.SelectedValue);
            sb.Append("&Search=" + txtAra.Text.Replace("'", "''"));
            Response.Redirect("/Gallery/Galleries.aspx" + sb);
        }

        public void LoadFilterQuery()
        {
            CategoryId = Helper.GetQueryStringValue<int>("CategoryId");
            GalleryType = Helper.GetQueryStringValue<string>("Type");
            Search = Helper.GetQueryStringValue<string>("Search");
            IsAgency = Helper.GetQueryStringValue<string>("IsAgency");
            Status = Helper.GetQueryStringValue<string>("Status");
        }
    }
}
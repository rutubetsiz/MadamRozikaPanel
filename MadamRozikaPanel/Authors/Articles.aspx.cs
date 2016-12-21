using MadamRozikaPanel.CrossCuttingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.Authors
{
    public partial class Articles : System.Web.UI.Page
    {
        public string AuthorId;
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
            //List<M_Articles> list = new List<M_Articles>();
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
                //list = ArticleOprt.GetAllArticlesWithCondition(500, sbCondition.ToString());
            }
            else
            {
                //list = ArticleOprt.GetAllArticles(500);
            }
            //rptAllNews.DataSource = list;
            rptAllNews.DataBind();
        }
        public void FillFilter()
        {
            //ddListYazarlar.DataSource = ArticleOprt.GetAllAuthors();
            ddListYazarlar.DataTextField = "Name";
            ddListYazarlar.DataValueField = "AuthorId";
            ddListYazarlar.DataBind();
            ddListYazarlar.Items.Insert(0, new ListItem("Tüm Yazarlar", "-1"));
            ddListYazarlar.SelectedValue = AuthorId;
            txtAra.Text = Search;
        }

        protected void rptAllNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnGuncelle") // check command is cmd_delete
            {
                int ArticleId = Convert.ToInt32(e.CommandArgument);
                TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
                CheckBox cbStatus = (CheckBox)e.Item.FindControl("cbStatus");
                //ArticleOprt.UpdateArticle(txtTitle.Text.Replace("'", "''"), Convert.ToInt32(cbStatus.Checked), ArticleId);
            }
            GetData();
        }
        protected void btnFiltrele_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("?AuthorId=" + ddListYazarlar.SelectedValue);
            sb.Append("&Search=" + txtAra.Text.Replace("'", "''"));
            Response.Redirect("/Authors/Articles.aspx" + sb);
        }

        public void LoadFilterQuery()
        {
            Search = Helper.GetQueryStringValue<string>("Search");
            AuthorId = Helper.GetQueryStringValue<string>("AuthorId");
        }
    }
}
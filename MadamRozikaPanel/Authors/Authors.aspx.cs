using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MadamRozikaPanel.Authors
{
    public partial class Authors : System.Web.UI.Page
    {
        public string Status;
        public string Search;

        O_Author AuthorOprt = new O_Author();
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
            List<M_Authors> list = new List<M_Authors>();
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
                list = AuthorOprt.GetAllAuthorsWithCondition(500, sbCondition.ToString());
            }
            else
            {
                list = AuthorOprt.GetAllAuthors(500);
            }
            rptAllNews.DataSource = list;
            rptAllNews.DataBind();
        }
        public void FillFilter()
        {
            ddListDurum.SelectedValue = Status;
            txtAra.Text = Search;
        }
        protected void btnFiltrele_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?Status=" + ddListDurum.SelectedValue);
            sb.Append("&Search=" + txtAra.Text.Replace("'", "''"));
            Response.Redirect("/Authors/Authors.aspx" + sb);
        }

        public void LoadFilterQuery()
        {
            Search = Helper.GetQueryStringValue<string>("Search");
            Status = Helper.GetQueryStringValue<string>("Status");
        }
    }
}
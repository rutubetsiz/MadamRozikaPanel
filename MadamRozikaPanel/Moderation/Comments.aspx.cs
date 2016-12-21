using MadamRozikaPanel.CrossCuttingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MadamRozikaPanel.Moderation
{
    public partial class Comments : System.Web.UI.Page
    {
        //public O_Comment CommentOprt = new O_Comment(); 
        public string Search;
        public string IsActive;

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
            //List<M_Comments> list = new List<M_Comments>();
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
                        sbCondition.Append(queries[i].ToString().Split('=')[0] + " = '" + queries[i].ToString().Split('=')[1] + "' AND ");
                    }
                }
                sbCondition.Append("1=1");
                //list = CommentOprt.GetAllCommensWithCondition(500, sbCondition.ToString());
            }
            else
            {
                //list = CommentOprt.GetAllComments(500);
            }
            //rptAllNews.DataSource = list;
            rptAllNews.DataBind();
        }
        public void FillFilter()
        {
            txtAra.Text = Search;
        }

        protected void rptAllNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int CommentId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "btnAktifle")
            {
                //CommentOprt.UpdateComment(CommentId.ToString(), "1");
            }
            else if (e.CommandName == "btnPasifle")
            {
                //CommentOprt.UpdateComment(CommentId.ToString(), "2");
            }
            GetData();
        }
        protected void btnFiltrele_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?IsActive=" + ddListDurum.SelectedValue);
            sb.Append("&Search=" + txtAra.Text.Replace("'", "''"));
            Response.Redirect("/Moderation/Comments.aspx" + sb);
        }

        public void LoadFilterQuery()
        {
            Search = Helper.GetQueryStringValue<string>("Search");
            IsActive = Helper.GetQueryStringValue<string>("IsActive");
            //if (string.IsNullOrEmpty(IsActive))
            //{
            //    IsActive = "0";
            //}
        }
    }
}
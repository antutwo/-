using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.student
{
    public partial class candidateVotesDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
                string sql = "select * from Tx_student as a,Tx_grade as b where a.grade_id=b.grade_id and a.stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
              
                }
                title.Text = '"'+Request.QueryString["position"]+'"' + "获票详情表";
                bind();
            }
            
        }
        public void bind()
        {
            //统计某个学生的的职位获票数，职位，以及投票的人
            string sqlstr = "select max(a.vote_id)as vote_id,max(a.position)as position,max(a.grade_id)as grade_id,a.vote_name,max(b.counts)as counts from Tx_vote as a,(select count(*) " +
                "as counts,vote_name from Tx_vote where position='"+ Request.QueryString["position"] + "' and stu_id='" + Session["stuid"] + "' and caste='1' group by vote_name) " +
                "as b where a.vote_name=b.vote_name and a.position='" + Request.QueryString["position"] + "' and a.stu_id='"+Session["stuid"]+ "' and a.caste='1' group by a.vote_name";

            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "vote_id" };//设置主键
            GridView1.DataBind();
        }

       
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void DaoChu_Click(object sender, EventArgs e)
        {
            DGToExcel(GridView1);
        }
        public void DGToExcel(System.Web.UI.Control ctl)
        {
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
            ctl.Page.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            ctl.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }
    }
}
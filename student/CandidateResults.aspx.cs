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
    public partial class CandidateResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
                string sql1 = "select stu_name from Tx_student where stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql1);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
                }
                title.Text = '"' + Request.QueryString["position"] + '"' + "候选结果表";
                //绑定
                bind();
            }
        }
        public void countUpdateVote()
        {
            //将候选表中的票数进行修改
            Operation.runSql("update Tx_candidate set votes = b.vote from(select count(*) as vote, stu_id from Tx_vote where " +
           "stu_id in (select stu_id from Tx_candidate where position = '" + Request.QueryString["position"] + "' and grade_id = '" + Session["gid"] + "')and position = '" + Request.QueryString["position"] + "'" +
           " and caste = '1' group by stu_id) as b,Tx_candidate as a where a.stu_id = b.stu_id and a.position = '" + Request.QueryString["position"] + "'" +
           "and a.stu_id in(select stu_id from Tx_candidate where position = '" + Request.QueryString["position"] + "'and grade_id = '" + Session["gid"] + "')");

        }
        public void voteResult()
        {
            ///
            //将票数前几位的状态设置为成功候选
            /* Operation.runSql("update Tx_candidate set candidate_state='成功候选' where stu_id in (select top " +
                 "(select convert(int,number) from Tx_Gposition where position_name='" + Request.QueryString["position"] + "') stu_id from Tx_candidate order by votes desc)" +
                 "and position='" + Request.QueryString["position"] + "'");

             Operation.runSql("update Tx_candidate set candidate_state='候选失败' where stu_id not in (select top " +
               "(select convert(int,number) from Tx_Gposition where position_name='" + Request.QueryString["position"] + "') stu_id from Tx_candidate order by votes desc)" +
               "and position='" + Request.QueryString["position"] + "'");*/

            Operation.runSql("update Tx_candidate set candidate_state='成功候选' where stu_id in (select top " +
            "(select convert(int, number) from Tx_Gposition where position_name = '" + Request.QueryString["position"] + "')stu_id from Tx_candidate order by votes desc)" +
            "and position = '" + Request.QueryString["position"] + "'");
            Operation.runSql("update Tx_candidate set candidate_state='候选失败' where stu_id not in (select top(select convert(int,number) from Tx_Gposition where position_name='" + Request.QueryString["position"] + "') " +
                "stu_id from Tx_candidate order by votes desc) and position = '" + Request.QueryString["position"] + "'");

        }
        public void bind()
        {
            countUpdateVote();//统计票数
            voteResult();//修改成功获选学生的候选状态
            ///
            string sqlstr = "select * from Tx_candidate where position='" + Request.QueryString["position"] + "' and grade_id='"+Session["grade_id"]+"' order by candidate_state";
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "candidate_id" };//设置主键
            GridView1.DataBind();
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

       
    }
}